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
    public int Player1Point = 0;
    public int Player2Point = 0;
    // 取得枚数
    public int hudaCount1 = 0;
    public int hudaCount2 = 0;
    // 取得した札のリスト
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

        // カードをシャッフルして並べる
        cardController.Initialize();

        SetAnswer();
        if (PhotonNetwork.LocalPlayer.IsMasterClient == true)
        {
            PlayerScript.playerScript.PlayerPut();
        }
    }

    public void SetAnswer()
    {
        // すべての札を取り終わった時、勝敗判定へ
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

        // 次に読まれる札オブジェ
        karuta = karutaEhuda[shuffleNumber[_count]].gameObject;
        // 次に読まれる札のID
        karuta_hudaID = karutaEhuda[shuffleNumber[_count]];
        audioSource.PlayOneShot(karuta.GetComponent<KarutaHuda>().Getsound());

        // 次に読まれる札のコライダー
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
        waitTime = 6000;
        StartCoroutine(effects.ShowEffect(Getkaruta_hudaID()));

        // 「つ」を取った時
        if (Getkaruta_hudaID() == 17)
        {
            Debug.Log("「つ」をゲット！");
            waitTime = 6000;
            StartCoroutine(effects.ShowEffect(Getkaruta_hudaID()));
        }
    }

    // 「つ」を2点分に置き換える処理
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

    // 最終的な勝敗判定
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

    // 「つ」を2点分とした最終得点（プレイヤー側）を返す
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
            //お手付き処理ができてない
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

