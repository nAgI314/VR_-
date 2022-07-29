using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TitleCPUScript : MonoBehaviour
{

    [SerializeField] TMPro.TextMeshPro text;

    private AudioSource audio;
    void OnTriggerEnter(Collider other)
    {
        audio = GetComponent<AudioSource>();
        audio.Play();
        FadeOut.fadeOut.Fadeout();
        Invoke("LoadCPUScene", 1f);
        //StartCoroutine(Checking (() =>
        //{
        //text.text = "tag";
    }    
        void LoadCPUScene()
        {
            SceneManager.LoadScene("CPUScene");
        }
        //}));
    //}
    //public delegate void functionType();
    //private IEnumerator Checking (functionType callback)
   // {
     //   while (true)
       // {
         //   yield return new WaitForFixedUpdate();
           // if (!audio.isPlaying)
            //{
              //  callback();
              // break;
           // }
      // }
  }
        
       
    

