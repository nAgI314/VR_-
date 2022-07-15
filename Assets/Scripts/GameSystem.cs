using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using System.Threading;
using System.Threading.Tasks;

public class GameSystem : MonoBehaviour
{
    [Serializable] private class AnswerEvent : UnityEvent<CancellationToken> { }
    [SerializeField] KarutaHuda _KarutaHudaPrehub = null;
    [SerializeField] AudioSource audioSource = null;

    [SerializeField]private AnswerEvent setAnswerEvent = new AnswerEvent();
    CancellationTokenSource cancellationTokenSource;
    KarutaSystem cardController;
    int _count = 0;
    public int Player1Point = 0;
    public int Player2Point = 0;
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

        if (Player1Point + Player2Point == 44)
        {
            if (Player1Point > Player2Point)
            {

            }
            else if (Player2Point > Player1Point)
            {

            }
            else if (Player1Point == Player2Point)
            {
                //Ç©ÇÈÇΩÇÃÇ¬ÇéùÇ¡ÇƒÇ¢ÇΩÇÁ
                {

                }
                //Ç‡Ç¡ÇƒÇ¢Ç»Ç©Ç¡ÇΩÇÁ
                {

                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    void SetAnswer()
    {
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
           
        }
        else
        {

            Player2Point = Player2Point + PutPoint(huda.gameObject.GetComponent<KarutaHuda>().hudaID);
          
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
        return cardController.GetKarutaList() [cardController.GetnumberList()[_count]].gameObject;
    }

}

