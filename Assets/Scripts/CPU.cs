using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;


public class CPU : MonoBehaviour
{
    [SerializeField] GameSystem gameSystem;
    public async void OnSetAnswerAsync(CancellationToken token)
    {
        await Task.Delay(6000, token);
        if (token.IsCancellationRequested == false)
        {
            GetHuda();
        }
    }
    private void GetHuda()
    {
        gameSystem.GetPoint(gameSystem.Getanswer().GetComponent<Collider>(),false);

        

    }
}