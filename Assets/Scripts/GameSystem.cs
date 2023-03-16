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
    // 各自の取得枚数の表示用
    [SerializeField] TextMeshPro text;
    [Serializable] private class AnswerEvent : UnityEvent<CancellationToken> { }

    // カルタのテクスチャを貼る前の札オブジェクト
    [SerializeField] KarutaHuda _KarutaHudaPrehub = null;
    [SerializeField] AudioSource audioSource = null;

    // 間違えた札を取得した際のイベント
    [SerializeField] private UnityEvent MissEvent = new UnityEvent();
    // 正解の札を取得した際のイベント

    [SerializeField] private AnswerEvent setAnswerEvent = new AnswerEvent();
    CancellationTokenSource cancellationTokenSource;

    // カルタの札を管理しているもの
    KarutaSystem cardController;

    // 読み上げた累計枚数
    public int _count = 0;

    // 得点数
    public float Player1Point = 0;
    public float Player2Point = 0;

    // 取得枚数
    public int hudaCount1 = 0;
    public int hudaCount2 = 0;

    public bool turu_flag = false;

    // 取得した札のリスト
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

    [SerializeField] private int startWaitTime = 0;
    private int waitTime = 0;

    private void Awake()
    {
        if (instanceGameS == null)
        {
            instanceGameS = this;
        }
    }

    async void Start()
    {
        cardController = new KarutaSystem(_KarutaHudaPrehub);

        // カードをシャッフルして並べる
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

        if (SceneManager.GetActiveScene().name == "ShortVersionScene")//・ｽV・ｽ・ｽ・ｽ[・ｽg・ｽo・ｽ[・ｽW・ｽ・ｽ・ｽ・ｽ・ｽﾌ趣ｿｽ
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

        // 次に読まれる札オブジェ
        karuta = karutaEhuda[shuffleNumber[_count]].gameObject;
        // 次に読まれる札のID
        karuta_hudaID = karutaEhuda[shuffleNumber[_count]];

        audioSource.PlayOneShot( karuta.GetComponent<KarutaHuda>().Getsound());

        audioSource.PlayOneShot(karuta.GetComponent<KarutaHuda>().Getsound());

        // 次に読まれる札のコライダー
        BoxCollider boxCollider = karuta.GetComponent<BoxCollider>();
        boxCollider.enabled = true;

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

    // エフェクトを出すかどうかのチェック
    private void EffectCheck()
    {
        if (SceneManager.GetActiveScene().name == "ShortVersionScene")
        {
            // 「つ」を取った時
            if (Getkaruta_hudaID() == 0)
            {
                waitTime = 6000;
                effects.ShowEffect(Getkaruta_hudaID());
            }

            // 「え」を取った時
            if (Getkaruta_hudaID() == 1)
            {
                waitTime = 7000;
                effects.ShowEffect(Getkaruta_hudaID());
            }

            // 「す」を取った時
            if (Getkaruta_hudaID() == 2)
            {
                waitTime = 6000;
                effects.ShowEffect(Getkaruta_hudaID());
            }

            // 「た」を取った時
            if (Getkaruta_hudaID() == 3)
            {
                waitTime = 6000;
                effects.ShowEffect(Getkaruta_hudaID());
            }

            // 「ね」を取った時
            if (Getkaruta_hudaID() == 4)
            {
                waitTime = 6500;
                effects.ShowEffect(Getkaruta_hudaID());
            }

            // 「ひ」を取った時
            if (Getkaruta_hudaID() == 5)
            {
                waitTime = 6000;
                effects.ShowEffect(Getkaruta_hudaID());
            }
        }
    }

    // 「つ」を2点分に置き換える処理
    private float PutPoint(int Id)
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

    // 最終的な勝敗判定
    private void PlayerPoint()
    {
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
        {
            SceneManager.LoadScene("WinScene");
        }
        else
        {
            SceneManager.LoadScene("LoseScene");
        }
    }

    // 「つ」を2点分とした最終得点（プレイヤー側）を返す
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

    // 正解、不正解の結果を返す
    public bool IsCorrectCard(Collider huda)
    {
        return huda.gameObject.GetComponent<KarutaHuda>().Jin == jin;
    }

    public void DisableAllColliders()//全部のcolliderをoffにする
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
            //・ｽ・ｽ・ｽ・ｽt・ｽ・ｽ・ｽ・ｽ・ｽ・ｽ・ｽ・ｽ・ｽﾅゑｿｽ・ｽﾄなゑｿｽ
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
        if(_count==42)  //譛蠕後↓譛ｭ繧呈ｨｪ縺ｫ荳ｦ縺ｹ繧句・逅・
        {
           cardController.LastCardChange(cardController.GetKarutaList()[cardController.GetnumberList()[_count]].gameObject,cardController.GetKarutaList()[cardController.GetnumberList()[_count+1]].gameObject);
          
        }
        await Task.Delay(waitTime);
        SetAnswer();
    }
    
}

