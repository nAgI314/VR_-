using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    public static FadeOut fadeOut;

   // float fadeSpeed = 0.04f;
   // float alfa=0;
    
    Image fadeimage;
    bool loadStop = false;
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
    public async void Fadeout()
    {
        GameObject panel = GameObject.Find("Panel");
        Transform loading = panel.transform.Find("Loading");
        //alfa = 255;
        GetComponent<Image>().enabled = true;
        //Ç¬Ç¢Ç≈Ç…ï∂éöÇ‡èoÇ∑+loadâÊñ 
        GetComponentInChildren<TextMeshPro>().enabled=true ;
        loading.gameObject.SetActive(true);
        
        while (loadStop==false)
        {
           await Task.Delay(300);
           loading.transform.eulerAngles+= new Vector3(0,0,-45);
        }
        
        //for (bool i = true; i ==true;)
        //{










         //await Task.Delay(100 / 6);
        //alfa += fadeSpeed;
         //fadeimage.color = new Color(0, 0, 0, alfa);
        //if (alfa >= 1)
        //{
          //i = false;
        //}
         //}
         




    }
    public void LoadStop()
    {
        loadStop=true;
    }
}
