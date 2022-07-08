using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchKaruta : OVRGrabbable
{
    //’Í‚Ü‚ê‚½‚ÉÀs
    public override void GraBegin(OVRGrabber hand, Collider grabPoint)
    {
        base.GraBegin(hand, grabPoint); //‚±‚Ìs‚ÍÁ‚µ‚¿‚á‚¾‚ß

        //‚±‚±‚É‚â‚è‚½‚¢‚±‚Æ‚ğ‘‚«‚Ü‚µ‚å‚¤
        gameObject.SetActive(false);
    }
}