using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchKaruta : OVRGrabbable
{
    //掴まれた時に実行
    public override void GraBegin(OVRGrabber hand, Collider grabPoint)
    {
        base.GraBegin(hand, grabPoint); //この行は消しちゃだめ

        //ここにやりたいことを書きましょう
        gameObject.SetActive(false);
    }
}