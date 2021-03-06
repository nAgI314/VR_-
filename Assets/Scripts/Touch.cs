using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Touch : MonoBehaviour
{
    OVRInputTest eventController;
   
    [SerializeField]
    private TouchEvent touchEvent = new TouchEvent();

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  
    private void OnTriggerEnter(Collider other)
    {

        if ( other.tag != "Player")
        {
            //効果音呼び出す
            SoundEffectSystem.instance1.MakeSoundTouch();
            touchEvent.Invoke(other,true);
            
        }
       
    }
}
