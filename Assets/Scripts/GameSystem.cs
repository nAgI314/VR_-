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

    [SerializeField]private AnswerEvent setAnswerEvent = new AnswerEvent();
    CancellationTokenSource cancellationTokenSource;
    KarutaSystem cardController;
    public int _count = 0;
    public int Player1Point = 0;
    public int hudaCount1 = 0;
    public int Player2Point = 0;
    public int hudaCount2 = 0;
    List<GameObject> _Player1List = new List<GameObject>();
    List<GameObject> _Player2List = new List<GameObject>();

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
        
      
        GameObject karuta = karutaEhuda[shuffleNumber[_count]].gameObject;
        audioSource.PlayOneShot( karuta.GetComponent<KarutaHuda>().Getsound());
        
        karuta.GetComponent<BoxCollider>();

        BoxCollider boxCollider = karuta.GetComponent<BoxCollider>();
        boxCollider.enabled = true;
        Debug.Log(shuffleNumber[_count]);

        _count++;
        

        setAnswerEvent.Invoke(cancellationTokenSource.Token);

    }
    public void GetPoint(Collider huda,bool player)
    {
        cancellationTokenSource.Cancel();
        cancellationTokenSource.Dispose();
        if (player == true)
        {
             
             Player1Point= Player1Point + PutPoint(huda.gameObject.GetComponent<KarutaHuda>().hudaID);
             hudaCount1++;
        }
        else
        {

            Player2Point = Player2Point + PutPoint(huda.gameObject.GetComponent<KarutaHuda>().hudaID);
            hudaCount2++;
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

