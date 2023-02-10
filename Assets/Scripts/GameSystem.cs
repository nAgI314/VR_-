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
    public int Player1Point = 0;
    public int Player2Point = 0;
    // �擾����
    public int hudaCount1 = 0;
    public int hudaCount2 = 0;
    // �擾�����D�̃��X�g
    Stack<KarutaHuda> _Player1List = new Stack<KarutaHuda>();
    Stack<KarutaHuda> _Player2List = new Stack<KarutaHuda>();
    GameObject karuta = null;
    KarutaHuda karuta_hudaID = null;
    private bool jin;
    public static GameSystem instanceGameS = null;

    [SerializeField] private Text debugText = default;

    [SerializeField] private CPU cpu = default;

    [SerializeField] private Effects effects = default;

    private int waitTime = 0;

    private void Awake()
    {
        if (instanceGameS == null)
        {
            instanceGameS = this;
        }
    }

    void Start()
    {
        cardController = new KarutaSystem(_KarutaHudaPrehub);

        // �J�[�h���V���b�t�����ĕ��ׂ�
        cardController.Initialize();

        SetAnswer();
        if (PhotonNetwork.LocalPlayer.IsMasterClient == true)
        {
            PlayerScript.playerScript.PlayerPut();
        }
    }

    public void SetAnswer()
    {
        // ���ׂĂ̎D�����I��������A���s�����
        if (_count == 44)
        {
            PlayerPoint();
        }

        cancellationTokenSource = new CancellationTokenSource();

        List<KarutaHuda> karutaEhuda = cardController.GetKarutaList();
        List<int> shuffleNumber = cardController.GetnumberList();
        jin = karutaEhuda[shuffleNumber[_count]].Jin;

        // 
        for (int i = 0; i < karutaEhuda.Count; i++)
        {
            karutaEhuda[i].gameObject.GetComponent<BoxCollider>().enabled = karutaEhuda[i].Jin != jin;
        }

        // ���ɓǂ܂��D�I�u�W�F
        karuta = karutaEhuda[shuffleNumber[_count]].gameObject;
        // ���ɓǂ܂��D��ID
        karuta_hudaID = karutaEhuda[shuffleNumber[_count]];
        audioSource.PlayOneShot(karuta.GetComponent<KarutaHuda>().Getsound());

        // ���ɓǂ܂��D�̃R���C�_�[
        BoxCollider boxCollider = karuta.GetComponent<BoxCollider>();
        boxCollider.enabled = true;

        Debug.Log(shuffleNumber[_count]);

        _count++;

        setAnswerEvent.Invoke(cancellationTokenSource.Token);
    }

    public void GetPoint(Collider huda, bool player)
    {
        waitTime = 3000;

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
                text.text = _Player1List.Count + "��";
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
                    text.text = _Player1List.Count + "��";
                }
                return;
            }
        }
        else
        {
            cpu.PlayAnime("WinPose");

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
        waitTime = 6000;
        StartCoroutine(effects.ShowEffect(Getkaruta_hudaID()));

        // �u�v���������
        if (Getkaruta_hudaID() == 17)
        {
            Debug.Log("�u�v���Q�b�g�I");
            waitTime = 6000;
            StartCoroutine(effects.ShowEffect(Getkaruta_hudaID()));
        }
    }

    // �u�v��2�_���ɒu�������鏈��
    private int PutPoint(int Id)
    {
        if (Id == 17)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }

    public GameObject Getanswer()
    {
        return cardController.GetKarutaList()[cardController.GetnumberList()[_count - 1]].gameObject;
    }

    // �ŏI�I�ȏ��s����
    private void PlayerPoint()
    {
        if (GivePoint() > 22)
        {
            SceneManager.LoadScene("WinScene");
        }
        else
        {
            SceneManager.LoadScene("LoseScene");
        }
    }

    // �u�v��2�_���Ƃ����ŏI���_�i�v���C���[���j��Ԃ�
    public int GivePoint()
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
            //����t���������ł��ĂȂ�
            MissEvent.Invoke();
            SoundEffectSystem.instance1.MakeSoundNoTouch();
        }
    }

    public int Getkaruta_hudaID()
    {
        return karuta_hudaID.hudaID;
    }

    private async void Wait()
    {
        await Task.Delay(waitTime);
        SetAnswer();
    }
}

