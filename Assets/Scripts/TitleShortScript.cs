using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TitleShortScript : MonoBehaviour
{

    [SerializeField] TMPro.TextMeshPro text;

    private AudioSource audio;
    void OnTriggerEnter(Collider other)
    {
        audio = GetComponent<AudioSource>();
        audio.Play();
        FadeOut.fadeOut.Fadeout();
        Invoke("LoadShortVersionScene", 1f);
        //BotuPhotonScript.botuPhotonScript.YetEvent.AddListener(()=>{( ); });
        

        //Invoke("LoadShortVersionScene", 1f);
        //StartCoroutine(Checking (() =>
        // {

    }
    void LoadShortVersionScene()
    {    //text.text = "tag";
        SceneManager.LoadScene("ShortVersionScene");
    }
        //}));
    //}
    //public delegate void functionType();
    //private IEnumerator Checking (functionType callback)
    //{
      //  while (true)
       // {
         //   yield return new WaitForFixedUpdate();
           // if (!audio.isPlaying)
            //{
              //  callback();
                //break;
           // }
       // }
   // }
        
       
    
}
