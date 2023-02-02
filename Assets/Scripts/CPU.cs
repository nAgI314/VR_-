using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;


public class CPU : MonoBehaviour
{
    [SerializeField] GameSystem gameSystem;
    [SerializeField] private Animator action = default;

    private void Start()
    {
        action.Play("SearchPose", 0 ,0);
    }

    public async void OnSetAnswerAsync(CancellationToken token)
    {
        action.Play("SearchPose", 0, 0);

        await Task.Delay(6000, token);
        if (token.IsCancellationRequested == false)
        {
            SoundEffectSystem.instance1.MakeSoundNoTouch();
            GetHuda();
            // action.Play("WinPose", 0, 0);
        }

        
    }

    private void GetHuda()
    {
        gameSystem.GetPoint(gameSystem.Getanswer().GetComponent<Collider>(),false);

        

    }
}