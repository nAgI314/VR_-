using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
//�悭�킩��Ȃ�����������u
public class avater : MonoBehaviourPunCallbacks//,IPunObservable
{
    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
