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
    // Še©‚Ìæ“¾–‡”‚Ì•\¦—p
    [SerializeField] TextMeshPro text;
    [Serializable] private class AnswerEvent : UnityEvent<CancellationToken> { }

    // ƒJƒ‹ƒ^‚ÌƒeƒNƒXƒ`ƒƒ‚ğ“\‚é‘O‚ÌDƒIƒuƒWƒFƒNƒg
    [SerializeField] KarutaHuda _KarutaHudaPrehub = null;
    [SerializeField] AudioSource audioSource = null;

    // ŠÔˆá‚¦‚½D‚ğæ“¾‚µ‚½Û‚ÌƒCƒxƒ“ƒg
    [SerializeField] private UnityEvent MissEvent = new UnityEvent();
<<<<<<< HEAD
=======
    // ³‰ğ‚ÌD‚ğæ“¾‚µ‚½Û‚ÌƒCƒxƒ“ƒg
>>>>>>> develop
    [SerializeField] private AnswerEvent setAnswerEvent = new AnswerEvent();
    CancellationTokenSource cancellationTokenSource;
    // ƒJƒ‹ƒ^‚ÌD‚ğŠÇ—‚µ‚Ä‚¢‚é‚à‚Ì
    KarutaSystem cardController;
    // “Ç‚İã‚°‚½—İŒv–‡”
    public int _count = 0;
<<<<<<< HEAD
    public float Player1Point = 0;
    public int hudaCount1 = 0;
    public float Player2Point = 0;
    public int hudaCount2 = 0;
    public bool turu_flag = false;
=======
    // “¾“_”
    public int Player1Point = 0;
    public int Player2Point = 0;
    // æ“¾–‡”
    public int hudaCount1 = 0;
    public int hudaCount2 = 0;
    // æ“¾‚µ‚½D‚ÌƒŠƒXƒg
>>>>>>> develop
    Stack<KarutaHuda> _Player1List = new Stack<KarutaHuda>();
    Stack<KarutaHuda> _Player2List = new Stack<KarutaHuda>();
    GameObject karuta = null;
    KarutaHuda karuta_hudaID = null;
    private bool jin;
    public static GameSystem instanceGameS = null;
<<<<<<< HEAD
    int hudaAmount = 44;
    public int karutaCount;
=======

    [SerializeField] private Text debugText = default;

    [SerializeField] private CPU cpu = default;

    [SerializeField] private ShortVersionEffects effects = default;

    private int waitTime = 0;
>>>>>>> develop

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

        // ƒJ[ƒh‚ğƒVƒƒƒbƒtƒ‹‚µ‚Ä•À‚×‚é
        cardController.Initialize();

        SetAnswer();
        if (PhotonNetwork.LocalPlayer.IsMasterClient == true)
        {
            PlayerScript.playerScript.PlayerPut();
        }
    }

    public void SetAnswer()
    {
<<<<<<< HEAD
        List<KarutaHuda> karutaEhuda = cardController.GetKarutaList();
        List<int> shuffleNumber = cardController.GetnumberList();

        if (SceneManager.GetActiveScene().name == "ShortVersionScene")//ï¿½Vï¿½ï¿½ï¿½[ï¿½gï¿½oï¿½[ï¿½Wï¿½ï¿½ï¿½ï¿½ï¿½Ìï¿½
        {
            hudaAmount = 6;
        } 
        

        if (_count == hudaAmount)
=======
        // ‚·‚×‚Ä‚ÌD‚ğæ‚èI‚í‚Á‚½AŸ”s”»’è‚Ö
        if (_count == 44)
>>>>>>> develop
        {
            PlayerPoint();
        }

        cancellationTokenSource = new CancellationTokenSource();

        
        
        jin = karutaEhuda[shuffleNumber[_count]].Jin;
<<<<<<< HEAD
        
=======

        // 
>>>>>>> develop
        for (int i = 0; i < karutaEhuda.Count; i++)
        {
            karutaEhuda[i].gameObject.GetComponent<BoxCollider>().enabled = karutaEhuda[i].Jin != jin;
        }
<<<<<<< HEAD
       
=======

        // Ÿ‚É“Ç‚Ü‚ê‚éDƒIƒuƒWƒF
>>>>>>> develop
        karuta = karutaEhuda[shuffleNumber[_count]].gameObject;
        // Ÿ‚É“Ç‚Ü‚ê‚éD‚ÌID
        karuta_hudaID = karutaEhuda[shuffleNumber[_count]];
<<<<<<< HEAD
        audioSource.PlayOneShot( karuta.GetComponent<KarutaHuda>().Getsound());

=======
        audioSource.PlayOneShot(karuta.GetComponent<KarutaHuda>().Getsound());

        // Ÿ‚É“Ç‚Ü‚ê‚éD‚ÌƒRƒ‰ƒCƒ_[
>>>>>>> develop
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
<<<<<<< HEAD
                text.text = _Player1List.Count+"æš";

=======
                text.text = _Player1List.Count + "–‡";
                cpu.PlayAnime("LosePose");
                EffectCheck();
>>>>>>> develop
            }
            else
            {
                MissEvent.Invoke();
                SoundEffectSystem.instance1.MakeSoundNoTouch();
                if (_Player1List.Count > 0)
                {
                    _Player2List.Push(_Player1List.Pop());
<<<<<<< HEAD
                    text.text = _Player1List.Count + "æš";

=======
                    text.text = _Player1List.Count + "–‡";
>>>>>>> develop
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
<<<<<<< HEAD
    private float PutPoint(int Id)
=======

    // ƒGƒtƒFƒNƒg‚ğo‚·‚©‚Ç‚¤‚©‚Ìƒ`ƒFƒbƒN
    private void EffectCheck()
    {
        waitTime = 6000;
        // StartCoroutine(effects.ShowEffect(Getkaruta_hudaID()));

        // u‚Âv‚ğæ‚Á‚½
        if (Getkaruta_hudaID() == 17)
        {
            Debug.Log("u‚Âv‚ğƒQƒbƒgI");
            waitTime = 6000;
            // StartCoroutine(effects.ShowEffect(Getkaruta_hudaID()));
        }
    }

    // u‚Âv‚ğ2“_•ª‚É’u‚«Š·‚¦‚éˆ—
    private int PutPoint(int Id)
>>>>>>> develop
    {
        if(SceneManager.GetActiveScene().name == "ShortVersionScene")
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

    // ÅI“I‚ÈŸ”s”»’è
    private void PlayerPoint()
    {
<<<<<<< HEAD
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
                karutaCount=_Player1List.Count;
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
=======
        if (GivePoint() > 22)
>>>>>>> develop
        {
            SceneManager.LoadScene("WinScene");
        }
        else
        {
            SceneManager.LoadScene("LoseScene");
        }
    }
<<<<<<< HEAD
    public float GivePoint()
=======

    // u‚Âv‚ğ2“_•ª‚Æ‚µ‚½ÅI“¾“_iƒvƒŒƒCƒ„[‘¤j‚ğ•Ô‚·
    public int GivePoint()
>>>>>>> develop
    {
        //List<KarutaHuda> _Player1ListList = new List<KarutaHuda>();
        //for (int i=1;i <= _Player1List.Count; i++)
        //{
        //  _Player1ListList.Add(_Player1List.Pop());

        //}
        for (int i = 0; i < _Player1List.Count; i++)
        {
<<<<<<< HEAD
            Player1Point = Player1Point + PutPoint(_Player1List.ToArray()[i].HudaID);

=======
            Player1Point = Player1Point + PutPoint(_Player1List.ToArray()[i].hudaID);
>>>>>>> develop
        }
        return Player1Point;
    }

<<<<<<< HEAD
=======
    // ³‰ğA•s³‰ğ‚ÌŒ‹‰Ê‚ğ•Ô‚·
>>>>>>> develop
    public bool IsCorrectCard(Collider huda)
    {
        return huda.gameObject.GetComponent<KarutaHuda>().Jin == jin;
    }

<<<<<<< HEAD
    public void DisableAllColliders()//ï¿½Sï¿½ï¿½ï¿½ï¿½colliderï¿½ï¿½offï¿½É‚ï¿½ï¿½ï¿½
=======
    public void DisableAllColliders()//‘S•”‚Ìcollider‚ğoff‚É‚·‚é
>>>>>>> develop
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
            //ï¿½ï¿½ï¿½ï¿½tï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Å‚ï¿½ï¿½Ä‚È‚ï¿½
            MissEvent.Invoke();
            SoundEffectSystem.instance1.MakeSoundNoTouch();
        }
    }

    public int Getkaruta_hudaID()
    {
        Debug.Log($"hudaID:{karuta_hudaID.HudaID}");
          return karuta_hudaID.HudaID;
    }

<<<<<<< HEAD
    async void Wait()
    {      
        if(_count==42)  //æœ€å¾Œã«æœ­ã‚’æ¨ªã«ä¸¦ã¹ã‚‹å‡¦ç†
        {
           cardController.LastCardChange(cardController.GetKarutaList()[cardController.GetnumberList()[_count]].gameObject,cardController.GetKarutaList()[cardController.GetnumberList()[_count+1]].gameObject);
          
        }
        await Task.Delay(1500);
=======
    private async void Wait()
    {
        await Task.Delay(waitTime);
>>>>>>> develop
        SetAnswer();
    }
    
}

