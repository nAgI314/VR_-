using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;


public class CPU : MonoBehaviour
{
    
    [SerializeField] GameSystem gameSystem;
    [SerializeField] int waittime=6000;
    private GameObject DifficultLevel_obj;
    private DifficultLevel difficultLevel;
    
    
    void Start()
    {
        DifficultLevel_obj = GameObject.Find("LevelDecide");
        difficultLevel = DifficultLevel_obj.GetComponent<DifficultLevel>();
        waittime=DifficultLevel.howDifficultLevel;
    }
    
    public async void OnSetAnswerAsync(CancellationToken token)
    {
        await Task.Delay(waittime, token);
        if (token.IsCancellationRequested == false)
        {
            SoundEffectSystem.instance1.MakeSoundNoTouch();
            GetHuda();
        }
    }
    private void GetHuda()
    {
        gameSystem.GetPoint(gameSystem.Getanswer().GetComponent<Collider>(),false);

        

    }
}