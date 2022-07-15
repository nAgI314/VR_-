using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Touch : MonoBehaviour
{
    OVRInputTest eventController;
    [Serializable] private class TouchEvent : UnityEvent<Collider,bool> { }
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
            touchEvent.Invoke(other,true);
            
        }
       
    }
}
