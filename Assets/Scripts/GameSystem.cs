using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GameSystem : MonoBehaviour
{
  
    [SerializeField] KarutaHuda _KarutaHudaPrehub = null;


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
    //リストにとったカードを入れる処理はまだしてない
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
                //かるたのつを持っていたら
                {

                }
                //もっていなかったら
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

        List<KarutaHuda> karutaEhuda = cardController.GetKarutaList();
        List<int> shuffleNumber = cardController.GetnumberList();

      
        GameObject karuta = karutaEhuda[shuffleNumber[_count]].gameObject;
        karuta.GetComponent<BoxCollider>();
        BoxCollider boxCollider = karuta.GetComponent<BoxCollider>();
        boxCollider.enabled = true;
        Debug.Log(shuffleNumber[_count]);

        _count++;
    }
    public void GetPoint(Collider huda)
    {
        if (huda.gameObject.GetComponent<KarutaHuda>().hudaID == 17)
        {
            Player1Point ++;
        }
        Player1Point++;
        huda.gameObject.SetActive(false);
        SetAnswer();
    }

}

