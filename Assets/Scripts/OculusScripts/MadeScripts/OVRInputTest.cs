using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable] public class TouchEvent : UnityEvent<Collider, bool> { }
public class OVRInputTest : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshPro text;
    [SerializeField] OVRInput.Controller m_controller;
    OVRInputTest eventController;
    //public UnityEvent<GameObject> touchEvent = new UnityEvent<GameObject>();
    [SerializeField] private TouchEvent touchEvent = new TouchEvent();

    public KarutaSystem KarutaSystem;
    public GameSystem GameSystem;

    private OVRInput.Controller controller;
    private bool isTriggerDown = false;

    bool miss;
    float timer;

    public static OVRInputTest instanceOVRIn = null;
    public Collider hudaCollider=null;

    public bool GetIsTriggerDown()
    {
        return isTriggerDown;
    }
    void Start()
    {
        
        //controller = GetComponent<OVRControllerHelper>().m_controller;
    }

    void Update()
    {
        float value = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger,m_controller);

        // 値が0でなければ、コントローラーのTriggerが押されている
        if (value > 0.5)
        {
            isTriggerDown = true;
            
        }
        else
        {
            isTriggerDown = false;
            
        }
        if (miss == true)
        {
            timer += Time.deltaTime;
            if (timer >= 0.5)
            {
                miss = false;
            }
        }

    }




    /** 別のCollider(other)に触れている間実行 **/
   
    public void OnTriggerEnter(Collider other)
    {


        other = hudaCollider;
        // コントローラーのTriggerが押されており、対象がプレイヤー自身でない
        if (isTriggerDown==true && hudaCollider.tag != "Player"&&miss==false)
        {
            if (text != null)
            {
                text.text = "tag:" + hudaCollider.tag;
            }
            //ここから自分で打ったやつ（点数いれたり）

            
            touchEvent.Invoke(hudaCollider,true);
            //ここまで自分で打ったやつ
            // コントローラーとつかんだオブジェクトのtransformを同期

            
        }
    }
    public void Miss()
    {
        miss = true;
        timer = 0;
    }
    public Collider GethudaCollider()
    {
        return hudaCollider;
    }
}