using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRInputTest : MonoBehaviour
{
    public KarutaSystem KarutaSystem;
    public GameSystem GameSystem;

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
    

    

    /** 別のCollider(other)に触れている間実行 **/
    void OnTriggerStay(Collider other)
    {
        int Player1Point;
        Player1Point = GameSystem.Player1Point;
        Debug.Log(Player1Point);
        Texture Correct;
        Correct = KarutaSystem.Correct;
        Debug.Log(Correct);

        // コントローラーのTriggerが押されており、対象がプレイヤー自身でない
        if (isTriggerDown && other.tag != "Player")
        {
            other.gameObject.transform.position = transform.position;
            //ここから自分で打ったやつ（点数いれたり）



            if (other.gameObject==Correct) 
            {

                Player1Point++ ; 
            }
            //ここまで自分で打ったやつ
            // コントローラーとつかんだオブジェクトのtransformを同期
            
            other.gameObject.transform.rotation = transform.rotation;
        }
    }
}