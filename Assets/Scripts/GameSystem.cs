using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;



public class GameSystem : MonoBehaviour
{
    // �e���̎擾�����̕\���p
    [SerializeField] TextMeshPro text;
    [Serializable] private class AnswerEvent : UnityEvent<CancellationToken> { }

    // �J���^�̃e�N�X�`����\��O�̎D�I�u�W�F�N�g
    [SerializeField] KarutaHuda _KarutaHudaPrehub = null;
    [SerializeField] AudioSource audioSource = null;

    // �ԈႦ���D���擾�����ۂ̃C�x���g
    [SerializeField] private UnityEvent MissEvent = new UnityEvent();
    // �����̎D���擾�����ۂ̃C�x���g

    [SerializeField] private AnswerEvent setAnswerEvent = new AnswerEvent();
    CancellationTokenSource cancellationTokenSource;

    // �J���^�̎D���Ǘ����Ă������
    KarutaSystem cardController;

    // �ǂݏグ���݌v����
    public int _count = 0;

    // ���_��
    public float Player1Point = 0;
    public float Player2Point = 0;

    // �擾����
    public int hudaCount1 = 0;
    public int hudaCount2 = 0;

    public bool turu_flag = false;

    // �擾�����D�̃��X�g
    Stack<KarutaHuda> _Player1List = new Stack<KarutaHuda>();
    Stack<KarutaHuda> _Player2List = new Stack<KarutaHuda>();
    GameObject karuta = null;
    KarutaHuda karuta_hudaID = null;
    private bool jin;
    public static GameSystem instanceGameS = null;
    int hudaAmount = 44;
    public int karutaCount;

    [SerializeField] private CPU cpu = default;
    [SerializeField] private ShortVersionEffects effects = default;

    [SerializeField] private int startWaitTime = 3000;
    private int waitTime = 2000;
    private bool isShortVersion = false;

    private void Awake()
    {
        if (instanceGameS == null)
        {
            instanceGameS = this;
        }

        if (SceneManager.GetActiveScene().name == "ShortVersionScene")
        {
            isShortVersion = true;
        }
    }

    async void Start()
    {
        cardController = new KarutaSystem(_KarutaHudaPrehub);

        // �J�[�h���V���b�t�����ĕ��ׂ�
        cardController.Initialize();

        await Task.Delay(startWaitTime);

        SetAnswer();

        if (PhotonNetwork.LocalPlayer.IsMasterClient == true)
        {
            PlayerScript.playerScript.PlayerPut();
        }
    }

    public void SetAnswer()
    {

        List<KarutaHuda> karutaEhuda = cardController.GetKarutaList();
        List<int> shuffleNumber = cardController.GetnumberList();

        if (isShortVersion)
        {
            hudaAmount = 6;
        } 
        

        if (_count == hudaAmount)
        {
            PlayerPoint();
        }

        cancellationTokenSource = new CancellationTokenSource();

        
        
        jin = karutaEhuda[shuffleNumber[_count]].Jin;


        for (int i = 0; i < karutaEhuda.Count; i++)
        {
            karutaEhuda[i].gameObject.GetComponent<BoxCollider>().enabled = karutaEhuda[i].Jin != jin;
        }

        // ���ɓǂ܂��D�I�u�W�F
        karuta = karutaEhuda[shuffleNumber[_count]].gameObject;
        // ���ɓǂ܂��D��ID
        karuta_hudaID = karutaEhuda[shuffleNumber[_count]];

        audioSource.PlayOneShot( karuta.GetComponent<KarutaHuda>().Getsound());

        audioSource.PlayOneShot(karuta.GetComponent<KarutaHuda>().Getsound());

        // ���ɓǂ܂��D�̃R���C�_�[
        BoxCollider boxCollider = karuta.GetComponent<BoxCollider>();
        boxCollider.enabled = true;

        _count++;

        setAnswerEvent.Invoke(cancellationTokenSource.Token);
    }

    public void GetPoint(Collider huda, bool player)
    {
        if (player == true)
        {
            if (IsCorrectCard(huda) == true)
            {
                SoundEffectSystem.instance1.MakeSoundTouch();
                cancellationTokenSource.Cancel();
                cancellationTokenSource.Dispose();
                //Player1Point = Player1Point + PutPoint(huda.gameObject.GetComponent<KarutaHuda>().hudaID);
                hudaCount1++;
                _Player1List.Push(huda.gameObject.GetComponent<KarutaHuda>());
                text.text = _Player1List.Count + "枚";
                cpu.PlayAnime("LosePose");
                EffectCheck();
            }
            else
            {
                MissEvent.Invoke();
                SoundEffectSystem.instance1.MakeSoundNoTouch();
                if (_Player1List.Count > 0)
                {
                    _Player2List.Push(_Player1List.Pop());
                    text.text = _Player1List.Count + "枚";
                }
                return;
            }
        }
        else
        {
            cpu.PlayAnime("WinPose");
            waitTime=2000;

            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
            //Player2Point = Player2Point + PutPoint(huda.gameObject.GetComponent<KarutaHuda>().hudaID);
            hudaCount2++;
            _Player2List.Push(huda.gameObject.GetComponent<KarutaHuda>());
        }
        huda.gameObject.SetActive(false);
        audioSource.Stop();
        
        Wait();
    }

    // �G�t�F�N�g���o�����ǂ����̃`�F�b�N
    private void EffectCheck()
    {
        if (isShortVersion)
        {
            waitTime = 6000;
            effects.ShowEffect(Getkaruta_hudaID());
        }
    }

    // �u�v��2�_���ɒu�������鏈��
    private float PutPoint(int Id)
    {
        if(isShortVersion)
        {
                return 1;
        }
        else
        {
            if (Id == 17)
            {
                return 1.5f;
            }
            else
            {
                 return 1;
            }
        }
    }

    public GameObject Getanswer()
    {
        return cardController.GetKarutaList()[cardController.GetnumberList()[_count - 1]].gameObject;
    }

    // �ŏI�I�ȏ��s����
    private void PlayerPoint()
    {
        karutaCount=_Player1List.Count;
        float MyPoint = GivePoint();
        if(MyPoint == hudaAmount/2)
        {   
            //Debug.Log(_Player1List);
            //foreach(var karuta_now in _Player1List)Debug.Log($"id:{karuta_now.hudaID}");
            /*foreach(var karuta_now in _Player1List){
                
                if(karuta_now.hudaID==0){
                    
                    SceneManager.LoadScene("WinScene");
                    return;
                }
            }*/
            
            
            for(int i=0;i<_Player1List.Count;i++){
                
                var karuta_now = _Player1List.Pop();

                if(karuta_now.HudaID==0){
                    
                    //Debug.Log($"id:{karuta_now.hudaID}");
                    
                    SceneManager.LoadScene("WinScene");
                    return;
                } 
            }
            SceneManager.LoadScene("LoseScene");

        }
        else if (MyPoint > hudaAmount/2)
        {
            SceneManager.LoadScene("WinScene");
        }
        else
        {
            SceneManager.LoadScene("LoseScene");
        }
    }

    // �u�v��2�_���Ƃ����ŏI���_�i�v���C���[���j��Ԃ�
    public float GivePoint()
    {
        //List<KarutaHuda> _Player1ListList = new List<KarutaHuda>();
        //for (int i=1;i <= _Player1List.Count; i++)
        //{
        //  _Player1ListList.Add(_Player1List.Pop());

        //}
        for (int i = 0; i < _Player1List.Count; i++)
        {
            Player1Point = Player1Point + PutPoint(_Player1List.ToArray()[i].hudaID);
        }
        return Player1Point;
    }

    // �����A�s�����̌��ʂ�Ԃ�
    public bool IsCorrectCard(Collider huda)
    {
        return huda.gameObject.GetComponent<KarutaHuda>().Jin == jin;
    }

    public void DisableAllColliders()//�S����collider��off�ɂ���
    {
        for (int i = 0; i < cardController.GetKarutaList().Count; i++)
        {
            cardController.GetKarutaList()[i].enabled = false;
        }
    }

    public void Miss(bool isMyself)
    {
        if (isMyself == true)
        {
            MissEvent.Invoke();
            SoundEffectSystem.instance1.MakeSoundNoTouch();
        }
    }

    public int Getkaruta_hudaID()
    {
        Debug.Log($"hudaID:{karuta_hudaID.HudaID}");
        return karuta_hudaID.HudaID;
    }

    private async void Wait()
    {      
        if(_count==42)
        {
           cardController.LastCardChange(cardController.GetKarutaList()[cardController.GetnumberList()[_count]].gameObject,cardController.GetKarutaList()[cardController.GetnumberList()[_count+1]].gameObject);
          
        }
        await Task.Delay(waitTime);
        //await Task.Delay(2000);
        SetAnswer();
    }
    
}

