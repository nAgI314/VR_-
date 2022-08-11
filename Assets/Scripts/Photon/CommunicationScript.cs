using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CommunicationScript : MonoBehaviourPunCallbacks
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
            photonView.RPC(nameof(TakenCardByOpponent), RpcTarget.Others,correctCardID, timeTookToGot);
            GameSystem.instanceGameS.DisableAllColliders();
        }
        else
        {
            //‚±‚±‚Í•sˆÀ
            photonView.RPC(nameof(GotWrongCard), RpcTarget.AllViaServer, correctCardID);
        }
    }
    private void DecidedWhoGetCard()
    {

    }
    [PunRPC]
    private void TakenCardByOpponent()
    {

    }
    private void GotCorrectCard(int cardID,bool isMasterClient)
    {

    }
    private void GotWrongCard(int cardID,bool isMasterCLIent)
    {

    }

}
