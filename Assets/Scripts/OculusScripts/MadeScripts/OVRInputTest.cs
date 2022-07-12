using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRInputTest : MonoBehaviour
{
    private OVRInput.Controller controller;
    private bool isTriggerDown = false;

    public bool GetIsTriggerDown()
    {
        return isTriggerDown;
    }
    void Start()
    {
        controller = GetComponent<OVRControllerHelper>().m_controller;
    }

    void Update()
    {
        // 値が0でなければ、コントローラーのTriggerが押されている
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller) > 0)
        {
            isTriggerDown = true;
        }
        else
        {
            isTriggerDown = false;
        }
    }
    int Player1Point;
    /** 別のCollider(other)に触れている間実行 **/
    void OnTriggerStay(Collider other)
    {

        // コントローラーのTriggerが押されており、対象がプレイヤー自身でない
        if (isTriggerDown && other.tag != "Player")
        {
            //ここから自分で打ったやつ（点数いれたり）
            int Correct=2;//ここは今は雑
            if(Correct==0) 
            {

                Player1Point++ ; 
            }
            //ここまで自分で打ったやつ
            // コントローラーとつかんだオブジェクトのtransformを同期
            other.gameObject.transform.position = transform.position;
            other.gameObject.transform.rotation = transform.rotation;
        }
    }
}