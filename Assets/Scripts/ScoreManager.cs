using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshPro text;
    public GameObject score_object = null;

    public GameSystem gameSystem;
    // Start is called before the first frame update
    void Start()
    {

        float hudaCount1;
        hudaCount1 = GameSystem.instanceGameS.karutaCount;
        //int hudaCount2;
        //hudaCount2 = gameSystem.hudaCount2;

        //Text score_text = score_object.GetComponent<Text>();
        text.text = "score:" + hudaCount1+ "枚";
    }

        // Update is called once per frame
        void Update()
        {

        }
    }

