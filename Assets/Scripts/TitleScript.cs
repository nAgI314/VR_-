using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TitleScript : MonoBehaviour
{

    [SerializeField] TMPro.TextMeshPro text;

    private AudioSource audio;
    void OnTriggerEnter(Collider other)
    {
        //audio = GetComponent<AudioSource>();
        //audio.Play();

        //StartCoroutine(Checking (() =>
        //{
        text.text = "tag";
            SceneManager.LoadScene("MainScene");
        //}));
    }
    //public delegate void functionType();
    //private IEnumerator Checking (functionType callback)
    //{
      //  while (true)
       // {
        //    yield return new WaitForFixedUpdate();
         //   if (!audio.isPlaying)
           // {
             //   callback();
               // break;
           // }
       // }
   // }
        
       
    
}
