using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    public static FadeOut fadeOut;

   // float fadeSpeed = 0.04f;
   // float alfa=0;
    
    Image fadeimage;
    // Start is called before the first frame update
    private void Awake()
    {
        if (fadeOut == null)
        {
            fadeOut = this;
        }
    }
    void Start()
    {
       // alfa = GetComponent<Image>().color.a;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FadeoutController()
    {
        
        

        
    }
    public void Fadeout()
    {
        //alfa = 255;
        GetComponent<Image>().enabled = true;
        //for (bool i = true; i ==true;)
        //{

           // await Task.Delay(100 / 6);
            //alfa += fadeSpeed;
           // fadeimage.color = new Color(0, 0, 0, alfa);
            //if (alfa >= 1)
            //{
              //  i = false;
            //}
       // }
        
        
        

        
    }
}
