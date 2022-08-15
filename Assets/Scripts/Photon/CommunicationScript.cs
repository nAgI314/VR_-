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
    /// <summary>
    /// 読み札が読み上げられたときに呼び出される関数
    /// </summary>
    /// <param name="cancellationToken"></param>
    public void OnStartReading(CancellationToken cancellationToken)
    {
        correctCardID = GameSystem.instanceGameS.Getkaruta_hudaID();
        timeToStartReading = PhotonNetwork.ServerTimestamp;
        timeTookToGot = int.MinValue;
        gotCard = false;
        gotCardByOpponent = false;
        //gotCorrectCard = false;
        //gotCorrectCardByOpponent = false;
        //gotWrongCard = false;
        //gotWrongCardByOpponent = false;
    }
    /// <summary>
    /// カードをタッチしたときに呼び出される関数
    /// </summary>
    /// <param name="collider"></param>
    /// <param name="player"></param>
    public void OnTouchCard(Collider collider, bool player)
    {
        
        
        //正解を取ったかの判定
        if (GameSystem.instanceGameS.IsCorrectCard(collider)==true)
        {
            gotCard = true;
            //gotCorrectCard = true;
            timeTookToGot = PhotonNetwork.ServerTimestamp - timeToStartReading;
            photonView.RPC(nameof(TakenCardByOpponent), RpcTarget.Others,correctCardID, timeTookToGot);
            GameSystem.instanceGameS.DisableAllColliders();
        }
        else
        {
            //gotWrongCard = false;
            //ここは不安
            photonView.RPC(nameof(GotWrongCard), RpcTarget.AllViaServer, correctCardID,PhotonNetwork.IsMasterClient );

        }
    }
    /// <summary>
    /// カードをとった側が決まった時に呼び出す
    /// </summary>
    private void DecidedWhoGetCard()
    {
        if(gotCorrectCard==false ||
            timeTookToGotByOpponent<timeTookToGot ||
            timeTookToGotByOpponent==timeTookToGot && PhotonNetwork.IsMasterClient == true)
        {
            photonView.RPC(nameof(GotCorrectCard), RpcTarget.AllViaServer, correctCardID, !PhotonNetwork.IsMasterClient);
        }
    }
    /// <summary>
    /// 相手が正解を取った
    /// </summary>
    /// <param name="cardID"></param>
    /// <param name="time"></param>
    [PunRPC]
    private async void TakenCardByOpponent(int cardID,int time)
    {//あいてが正解を取った
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
    [PunRPC]
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
    [PunRPC]
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
