using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;


public class CPU : MonoBehaviour
{
    [SerializeField] private GameSystem gameSystem;
    [SerializeField] private Animator action = default;

    private void Start()
    {
        // PlayAnime("InitPose");
    }

    public async void OnSetAnswerAsync(CancellationToken token)
    {
        PlayAnime("SearchPose");

        await Task.Delay(6000, token);
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