using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScript : MonoBehaviour
{
   
       void OnTriggerEnter(Collider other)
       {
           GetComponent<AudioSource>().Play();
        Invoke("Delay", 1f);
       }
       void Delay()
    {
        SceneManager.LoadScene("TitleScene");
    }
    
}
