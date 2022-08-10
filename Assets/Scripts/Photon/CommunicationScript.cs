using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CommunicationScript : MonoBehaviour
{
    [SerializeField] public GameSystem gameSystem;
    private int correctCardID;
    private int timeToStartReading;
    private int timeTookToGot;
    private int timeTookToGotByOpponent;
    private bool gotCard;
    private bool gotCardByOpponent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnStartReading(CancellationToken cancellationToken)
    {
        correctCardID = GameSystem.instanceGameS.Getkaruta_hudaID();
        timeToStartReading = PhotonNetwork.ServerTimestamp;
        gotCard = false;
        gotCardByOpponent = false;
    }
    public void OnTouchCard(Collider collider,Boolean player)
    {
        gotCard = true;
        timeTookToGot = PhotonNetwork.ServerTimestamp - timeToStartReading;
        if (GameSystem.instanceGameS.IsCorrectCard(OVRInputTest.instanceOVRIn.GethudaCollider())==true)
        {
            //続きはここからRPCを使うらしい（スプレッドシートをみて）
        }
        else
        {

        }
    }

}
