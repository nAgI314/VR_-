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

        // �l��0�łȂ���΁A�R���g���[���[��Trigger��������Ă���
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




    /** �ʂ�Collider(other)�ɐG��Ă���Ԏ��s **/
   
    public void OnTriggerEnter(Collider other)
    {


        other = hudaCollider;
        // �R���g���[���[��Trigger��������Ă���A�Ώۂ��v���C���[���g�łȂ�
        if (isTriggerDown==true && hudaCollider.tag != "Player"&&miss==false)
        {
            if (text != null)
            {
                text.text = "tag:" + hudaCollider.tag;
            }
            //�������玩���őł�����i�_�����ꂽ��j

            
            touchEvent.Invoke(hudaCollider,true);
            //�����܂Ŏ����őł������
            // �R���g���[���[�Ƃ��񂾃I�u�W�F�N�g��transform�𓯊�

            
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