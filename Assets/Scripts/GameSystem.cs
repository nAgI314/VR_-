using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public int Player1Point = 0;
    public int Player2Point = 0;
    List<GameObject> _Player1List = new List<GameObject>();
    List<GameObject> _Player2List = new List<GameObject>();

    
    //リストにとったカードを入れる処理はまだしてない
    // Start is called before the first frame update
    void Start()
    {

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
}
