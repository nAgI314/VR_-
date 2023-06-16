using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultLevel : MonoBehaviour
{
    public static int howDifficultLevel=2000;
   
    [SerializeField] private GameObject easy;
    [SerializeField] private GameObject normal;
    [SerializeField] private GameObject hard;
    [SerializeField] private int easyWaitSecond=6000;
    [SerializeField] private int normalWaitSecond=4000;
    [SerializeField] private int hardWaitSecond=1000;
    
    /*public void Awake() {
        if(difficultLevel==null){
            difficultLevel=this;
        }
    }*/
    void Start()
    {
        DontDestroyOnLoad(this);
        //↓難易度のリセット（easyに）
        howDifficultLevel=easyWaitSecond;
        DifLevTextController.difLevTextController.TextMove(0.45f);
        
        //easy=Transform.Find("LevelEasy");
        //normal=Transform.Find("LevelNormal");
        //hard=Transform.Find("LevelHard");
    }
    
    public void DecideEasyLevel()
    {
        howDifficultLevel=easyWaitSecond;
        DifLevTextController.difLevTextController.TextMove(0.45f);
        Debug.Log("aaaa EASY!");
    }
    public void DecideNormalLevel()
    {
        howDifficultLevel=normalWaitSecond;
        DifLevTextController.difLevTextController.TextMove(0.305f);
    }
    public void DecideHardLevel()
    {
        howDifficultLevel=hardWaitSecond;
        DifLevTextController.difLevTextController.TextMove(0.16f);
    }
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
