using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WhichObject : MonoBehaviour
{
    [SerializeField] private UnityEvent DifficultLevelEvent = new UnityEvent();
    //[SerializeField] private UnityEvent EasyEvent = new UnityEvent();
    //[SerializeField] private UnityEvent NormalEvent = new UnityEvent();
    //[SerializeField] private UnityEvent HardEvent = new UnityEvent();
    private GameObject whichObject=null;
    private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        whichObject=this.gameObject;
    }

    void OnTriggerEnter(Collider other)
    {
        audio = GetComponent<AudioSource>();
        GetComponent<AudioSource>().Play();
        DifficultLevelEvent.Invoke();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
