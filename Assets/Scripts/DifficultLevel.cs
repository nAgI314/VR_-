using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultLevel : MonoBehaviour
{
    public static int howDifficultLevel=2000;
   
    [SerializeField] private GameObject easy;
    [SerializeField] private GameObject normal;
    [SerializeField] private GameObject hard;
    [SerializeField] private int easyWaitSecond=4000;
    [SerializeField] private int normalWaitSecond=2000;
    [SerializeField] private int hardWaitSecond=1000;
    
    /*public void Awake() {
        if(difficultLevel==null){
            difficultLevel=this;
        }
    }*/
    void Start()
    {
        DontDestroyOnLoad(this);
        //easy=Transform.Find("LevelEasy");
        //normal=Transform.Find("LevelNormal");
        //hard=Transform.Find("LevelHard");
    }
    
    public void DecideEasyLevel()
    {
        howDifficultLevel=easyWaitSecond;
        Debug.Log("aaaa EASY!");
    }
    public void DecideNormalLevel()
    {
        howDifficultLevel=normalWaitSecond;
    }
    public void DecideHardLevel()
    {
        howDifficultLevel=hardWaitSecond;
    }
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
