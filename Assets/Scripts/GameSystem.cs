using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    [Serializable] private class AnswerEvent : UnityEvent<CancellationToken> { }
    
    [SerializeField] KarutaHuda _KarutaHudaPrehub = null;
    [SerializeField] AudioSource audioSource = null;

    [SerializeField] private UnityEvent MissEvent = new UnityEvent();
    [SerializeField]private AnswerEvent setAnswerEvent = new AnswerEvent();
    CancellationTokenSource cancellationTokenSource;
    KarutaSystem cardController;
    public int _count = 0;
    public int Player1Point = 0;
    public int hudaCount1 = 0;
    public int Player2Point = 0;
    public int hudaCount2 = 0;
    Stack<KarutaHuda> _Player1List = new Stack<KarutaHuda>();
    Stack<KarutaHuda> _Player2List = new Stack<KarutaHuda>();

    bool jin;

    private void Awake()
    {
        cardController = new KarutaSystem(_KarutaHudaPrehub);
    }
   
    // Start is called before the first frame update
    void Start()
    {
        cardController.Initialize();
        SetAnswer();


    }

    // Update is called once per frame
    void Update()
    {

    }
    async void SetAnswer()
    {
        await Task.Delay(1000);
        if (_count == 44)
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
        
        GameObject karuta = karutaEhuda[shuffleNumber[_count]].gameObject;
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
            if (huda.gameObject.GetComponent<KarutaHuda>().Jin == jin)
            {
                SoundEffectSystem.instance1.MakeSoundTouch();
                cancellationTokenSource.Cancel();
                cancellationTokenSource.Dispose();
                Player1Point = Player1Point + PutPoint(huda.gameObject.GetComponent<KarutaHuda>().hudaID);
                hudaCount1++;
                _Player1List.Push(huda.gameObject.GetComponent<KarutaHuda>());

            }
            else
            {
                MissEvent.Invoke();
                SoundEffectSystem.instance1.MakeSoundNoTouch();
                if (_Player1List.Count > 0)
                {

                    _Player2List.Push(_Player1List.Pop());
                    
                }
                return;
            }
        }
        else
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
            Player2Point = Player2Point + PutPoint(huda.gameObject.GetComponent<KarutaHuda>().hudaID);
            hudaCount2++;
            _Player2List.Push(huda.gameObject.GetComponent<KarutaHuda>());
        }
        huda.gameObject.SetActive(false);
        audioSource.Stop();

        SetAnswer();
    }
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
        return cardController.GetKarutaList() [cardController.GetnumberList()[_count-1]].gameObject;
    }

    private void PlayerPoint() 
    {
        if (Player1Point > 22)
        {
            SceneManager.LoadScene("WinScene");
        }
        else
        {
            SceneManager.LoadScene("LoseScene");
        }
    }

}

