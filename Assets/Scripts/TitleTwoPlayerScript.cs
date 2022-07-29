using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TitleTwoPlayerScript : MonoBehaviour
{

    [SerializeField] TMPro.TextMeshPro text;

    private AudioSource audio;
    void OnTriggerEnter(Collider other)
    {
        audio = GetComponent<AudioSource>();
        audio.Play();
        FadeOut.fadeOut.Fadeout();
        BotuPhotonScript.botuPhotonScript.Event.AddListener(()=>{ Invoke("LoadTwoPlayerScene", 1f); });
        //‚ ‚Æ‚ÅBotuPhotonScript.botuPhotonScript.YetEvent.AddListener(()=>{ ); });
        BotuPhotonScript.botuPhotonScript.Ready();

        //Invoke("LoadTwoPlayerScene", 1f);
        //StartCoroutine(Checking (() =>
        // {

    }
    void LoadTwoPlayerScene()
    {    //text.text = "tag";
        SceneManager.LoadScene("NewTwoPlayerScene");
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
