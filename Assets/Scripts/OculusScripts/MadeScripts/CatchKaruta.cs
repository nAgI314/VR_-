using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchKaruta : OVRGrabbable
{
    //�͂܂ꂽ���Ɏ��s
    public override void GraBegin(OVRGrabber hand, Collider grabPoint)
    {
        base.GraBegin(hand, grabPoint); //���̍s�͏������Ⴞ��

        //�����ɂ�肽�����Ƃ������܂��傤
        gameObject.SetActive(false);
    }
}