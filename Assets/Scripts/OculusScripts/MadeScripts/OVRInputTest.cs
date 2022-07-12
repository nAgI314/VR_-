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
    int Player1Point;
    /** �ʂ�Collider(other)�ɐG��Ă���Ԏ��s **/
    void OnTriggerStay(Collider other)
    {

        // �R���g���[���[��Trigger��������Ă���A�Ώۂ��v���C���[���g�łȂ�
        if (isTriggerDown && other.tag != "Player")
        {
            //�������玩���őł�����i�_�����ꂽ��j
            int Correct=2;//�����͍��͎G
            if(Correct==0) 
            {

                Player1Point++ ; 
            }
            //�����܂Ŏ����őł������
            // �R���g���[���[�Ƃ��񂾃I�u�W�F�N�g��transform�𓯊�
            other.gameObject.transform.position = transform.position;
            other.gameObject.transform.rotation = transform.rotation;
        }
    }
}