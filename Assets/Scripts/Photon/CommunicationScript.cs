using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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
    private bool gotCorrectCard;
    private bool gotCorrectCardByOpponent;
    private bool gotWrongCard;
    private bool gotWrongCardByOpponent;

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
        gotCorrectCard = false;
        gotCorrectCardByOpponent = false;
        gotWrongCard = false;
        gotWrongCardByOpponent = false;
    }
    public void OnTouchCard(Collider collider, bool player)
    {
        gotCard = true;
        
        //ê≥âÇéÊÇ¡ÇΩÇ©ÇÃîªíË
        if (GameSystem.instanceGameS.IsCorrectCard(OVRInputTest.instanceOVRIn.GethudaCollider())==true)
        {
            gotCorrectCard = true;
            timeTookToGot = PhotonNetwork.ServerTimestamp - timeToStartReading;
            photonView.RPC(nameof(TakenCardByOpponent), RpcTarget.Others,correctCardID, timeTookToGot);
            GameSystem.instanceGameS.DisableAllColliders();
        }
        else
        {
            gotWrongCard = false;
            //Ç±Ç±ÇÕïsà¿
            photonView.RPC(nameof(GotWrongCard), RpcTarget.AllViaServer, correctCardID);

        }
    }
    private void DecidedWhoGetCard()
    {
        if(gotCorrectCard==false || timeTookToGotByOpponent<timeTookToGot || timeTookToGotByOpponent==timeTookToGot && PhotonNetwork.IsMasterClient == true)
        {
            photonView.RPC(nameof(GotCorrectCard), RpcTarget.AllViaServer, correctCardID, !PhotonNetwork.IsMasterClient);
        }
    }
    [PunRPC]
    private async void TakenCardByOpponent(int cardID,int time)
    {//Ç†Ç¢ÇƒÇ™ê≥âÇéÊÇ¡ÇΩ
        if (cardID == correctCardID)
        {
            timeTookToGotByOpponent = time;
            gotCorrectCardByOpponent = true;
            if (gotCorrectCard == true)
            {
                if (timeTookToGotByOpponent <= timeTookToGot)
                {
                    DecidedWhoGetCard();
                }
                else
                {

                }
                
            }
            else
            {
                await Task.Delay(timeTookToGotByOpponent);
                if (gotCorrectCard == true)
                {
                    if (timeTookToGotByOpponent <= timeTookToGot)
                    {
                        DecidedWhoGetCard();
                    }
                }
                else
                {
                    DecidedWhoGetCard();
                }
            }
            

            
        }
    }
    private void GotCorrectCard(int cardID,bool isMasterClient)
    {
        if (cardID == correctCardID)
        {
            if (isMasterClient == PhotonNetwork.IsMasterClient)
            {
                GameSystem.instanceGameS.GetPoint(OVRInputTest.instanceOVRIn.GethudaCollider(),isMasterClient==PhotonNetwork.IsMasterClient);
            }
            else
            {

            }
        }
    }
    private void GotWrongCard(int cardID,bool isMasterClient)
    {
        if(cardID==correctCardID)
        {
            //if (gotCard == false && gotCardByOpponent == false)
            //{
                gotWrongCard = false;
                GameSystem.instanceGameS.Miss(isMasterClient == PhotonNetwork.IsMasterClient);
            //}
        }
    }

}
