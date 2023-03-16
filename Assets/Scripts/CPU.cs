using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;


public class CPU : MonoBehaviour
{
    [SerializeField] private GameSystem gameSystem;
    [SerializeField] private Animator action = default;
    [SerializeField] private int waitTime = 3000;
    private GameObject DifficultLevel_obj;
    private DifficultLevel difficultLevel;
    
    void Start()
    {
        DifficultLevel_obj = GameObject.Find("LevelDecide");
        difficultLevel = DifficultLevel_obj.GetComponent<DifficultLevel>();
        waitTime = DifficultLevel.howDifficultLevel;
    }
    
    public async void OnSetAnswerAsync(CancellationToken token)
    {
        PlayAnime("SearchPose");
        await Task.Delay(waitTime, token);
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

    public void PlayAnime(string name)
    {
        action.Play(name, 0, 0);
    }
}