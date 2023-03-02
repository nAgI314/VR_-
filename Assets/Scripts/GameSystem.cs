using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using Photon.Pun;


public class GameSystem : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshPro text;
    [Serializable] private class AnswerEvent : UnityEvent<CancellationToken> { }
    
    [SerializeField] KarutaHuda _KarutaHudaPrehub = null;
    [SerializeField] AudioSource audioSource = null;

    [SerializeField] private UnityEvent MissEvent = new UnityEvent();
    [SerializeField] private AnswerEvent setAnswerEvent = new AnswerEvent();
    CancellationTokenSource cancellationTokenSource;
    KarutaSystem cardController;
    public int _count = 0;
    public int Player1Point = 0;
    public int hudaCount1 = 0;
    public int Player2Point = 0;
    public int hudaCount2 = 0;
    public bool turu_flag = false;
    Stack<KarutaHuda> _Player1List = new Stack<KarutaHuda>();
    Stack<KarutaHuda> _Player2List = new Stack<KarutaHuda>();
    GameObject karuta=null;
    KarutaHuda karuta_hudaID = null;
    bool jin;
    public static GameSystem instanceGameS = null;
    int hudaAmount = 44;

    private void Awake()
    {
        if (instanceGameS == null)
        {
            instanceGameS = this;
        }
        
    }
   
    // Start is called before the first frame update
    void Start()
    {
        cardController = new KarutaSystem(_KarutaHudaPrehub);
        cardController.Initialize();
        SetAnswer();
        if (PhotonNetwork.LocalPlayer.IsMasterClient == true)
        {
            PlayerScript.playerScript.PlayerPut();
        }
        

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetAnswer()
    {
        if (SceneManager.GetActiveScene().name == "ShortVersionScene")//�V���[�g�o�[�W�����̎�
        {
            hudaAmount = 6;
        } 
        //if(_count==42)  最後に札を横に並べる処理
        //{
        //   cardController.LastCardChange();
        //}

        if (_count == hudaAmount)
        {
            PlayerPoint();
        }

        cancellationTokenSource = new CancellationTokenSource();

        List<KarutaHuda> karutaEhuda = cardController.GetKarutaList();
        List<int> shuffleNumber = cardController.GetnumberList();
        
        jin = karutaEhuda[shuffleNumber[_count]].Jin;
        
        for (int i = 0; i < karutaEhuda.Count; i++)
        {
            karutaEhuda[i].gameObject.GetComponent<BoxCollider>().enabled = karutaEhuda[i].Jin != jin;
        }
       
        karuta = karutaEhuda[shuffleNumber[_count]].gameObject;
        karuta_hudaID = karutaEhuda[shuffleNumber[_count]];
        audioSource.PlayOneShot( karuta.GetComponent<KarutaHuda>().Getsound());

        BoxCollider boxCollider = karuta.GetComponent<BoxCollider>();
        boxCollider.enabled = true;

        Debug.Log(shuffleNumber[_count]);

        _count++;
        

        setAnswerEvent.Invoke(cancellationTokenSource.Token);

    }
    public void GetPoint(Collider huda,bool player)
    {
        

        if (player == true)
        {
            if (IsCorrectCard(huda)==true)
            {
                SoundEffectSystem.instance1.MakeSoundTouch();
                cancellationTokenSource.Cancel();
                cancellationTokenSource.Dispose();
                //Player1Point = Player1Point + PutPoint(huda.gameObject.GetComponent<KarutaHuda>().hudaID);
                hudaCount1++;
                _Player1List.Push(huda.gameObject.GetComponent<KarutaHuda>());
                text.text = _Player1List.Count+"枚";

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
    private int PutPoint(int Id)
    {
        if(SceneManager.GetActiveScene().name == "ShortVersionScene")
        {
                return 1;
        }
        else
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
    }
    public GameObject Getanswer()
    {
        return cardController.GetKarutaList() [cardController.GetnumberList()[_count-1]].gameObject;
    }

    private void PlayerPoint() 
    {
        int MyPoint = GivePoint();
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
    public int GivePoint()
    {
        //List<KarutaHuda> _Player1ListList = new List<KarutaHuda>();
        //for (int i=1;i <= _Player1List.Count; i++)
        //{
          //  _Player1ListList.Add(_Player1List.Pop());

        //}
        for (int i=0;i< _Player1List.Count; i++)
        {
            Player1Point = Player1Point + PutPoint(_Player1List.ToArray()[i].HudaID);

        }
        return Player1Point;
    }

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
        Debug.Log($"hudaID:{karuta_hudaID.HudaID}");
          return karuta_hudaID.HudaID;
    }

    async void Wait()
    {      
        await Task.Delay(1500);
        SetAnswer();
    }
    
}

