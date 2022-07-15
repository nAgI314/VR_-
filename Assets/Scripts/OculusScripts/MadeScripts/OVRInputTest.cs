using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class OVRInputTest : MonoBehaviour
{
    public UnityEvent<GameObject> touchEvent = new UnityEvent<GameObject>();

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
      
        
        // �l��0�łȂ���΁A�R���g���[���[��Trigger��������Ă���
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller) > 0)
        {
            isTriggerDown = true;
        }
        else
        {
            isTriggerDown = false;
        }
    }




    /** �ʂ�Collider(other)�ɐG��Ă���Ԏ��s **/
   
    public void OnTriggerStay(Collider other)
    {
        int Player1Point;
        Player1Point = GameSystem.Player1Point;
        Debug.Log(Player1Point);
      

        // �R���g���[���[��Trigger��������Ă���A�Ώۂ��v���C���[���g�łȂ�
        if (isTriggerDown && other.tag != "Player")
        {
            other.gameObject.transform.position = transform.position;
            //�������玩���őł�����i�_�����ꂽ��j


            touchEvent.Invoke(other.gameObject);
            //�����܂Ŏ����őł������
            // �R���g���[���[�Ƃ��񂾃I�u�W�F�N�g��transform�𓯊�

            other.gameObject.transform.rotation = transform.rotation;
        }
    }
}