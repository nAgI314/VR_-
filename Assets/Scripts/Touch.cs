using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Touch : MonoBehaviour
{
    [Serializable] private class TouchEvent : UnityEvent<Collider> { }
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

        touchEvent.Invoke(other);
        Debug.Log("test");
    }
}
