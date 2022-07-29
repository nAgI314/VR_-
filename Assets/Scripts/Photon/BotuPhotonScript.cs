using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using Oculus.Interaction.Input;
using UnityEngine.Events;

public class BotuPhotonScript : MonoBehaviourPunCallbacks
{
    [SerializeField] public UnityEvent Event=new UnityEvent();
    
    public static BotuPhotonScript botuPhotonScript;
    private void Awake()
    {
        if (botuPhotonScript == null) 
        {
            botuPhotonScript = this;
        }
    }
    private void Start()
    {
        
        DontDestroyOnLoad(gameObject);
    }
    // Use this for initialization
    public bool Ready()
    {
        //���o�[�W�����ł͈����K�{�ł������APUN2�ł͕s�v�ł��B
        return PhotonNetwork.ConnectUsingSettings();
        
    }

    void OnGUI()
    {
        //���O�C���̏�Ԃ���ʏ�ɏo��
        GUILayout.Label(PhotonNetwork.NetworkClientState.ToString());
    }


    //���[���ɓ����O�ɌĂяo�����
    public override void OnConnectedToMaster()
    {
        // "room"�Ƃ������O�̃��[���ɎQ������i���[����������΍쐬���Ă���Q������j
        PhotonNetwork.JoinRandomRoom();
        
    }

    //���[���ɓ�����ɌĂяo�����
    public override void OnJoinedRoom()
    {
        StartGame();


        //�L�����N�^�[�𐶐�(��ł�邩��)
        // PhotonNetwork.Instantiate("PlayerAvatar", new Vector3(2.295f,0.602f,-0.018f), Quaternion.identity, 0);
         //hotonNetwork.Instantiate("ClearBananaMan",new Vector3(2.299f,-0.33f,-0.2f), Quaternion.identity, 0);
        //��������������ł���悤�ɃX�N���v�g��L���ɂ���


    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Hashtable hashtable = new Hashtable();
        hashtable["seed"] = (int)(Random.Range(float.MinValue, float.MaxValue));
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.CustomRoomPropertiesForLobby = new[] { "seed" };

        //TODO: �������[�v
        PhotonNetwork.JoinOrCreateRoom(Random.Range(float.MinValue,float.MaxValue).ToString(), new RoomOptions(), TypedLobby.Default);
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        StartGame();

    }
    private void StartGame()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            Event.Invoke();
        }
    }
}



