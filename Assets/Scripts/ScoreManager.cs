using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public GameObject score_object = null;

    public GameSystem gameSystem;
    // Start is called before the first frame update
    void Start()
    {

        int hudaCount1;
        hudaCount1 = gameSystem.hudaCount1;
        //int hudaCount2;
        //hudaCount2 = gameSystem.hudaCount2;

        Text score_text = score_object.GetComponent<Text>();
        score_text.text = "‚ ‚È‚½‚Ì“¾“_‚Í" + hudaCount1 + "“_";
    }

        // Update is called once per frame
        void Update()
        {

        }
    }

