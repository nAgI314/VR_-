using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] GameObject PlayerObj=null;
    public static PlayerScript playerScript;
    private void Awake()
    {
        if (playerScript == null)
        {
            playerScript = this;
        }
    }
    public void PlayerPut()
    {
        //PhotonNetwork.Instantiate(,new Vector3(0,0,0) ,);
        PlayerObj.transform.position = new Vector3(2.3f, 0.45f, 0.6f) ;
        PlayerObj.transform.eulerAngles = new Vector3(0, 180, 0);
    }
}
