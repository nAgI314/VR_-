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
        //旧バージョンでは引数必須でしたが、PUN2では不要です。
        return PhotonNetwork.ConnectUsingSettings();
        
    }

    void OnGUI()
    {
        //ログインの状態を画面上に出力
        GUILayout.Label(PhotonNetwork.NetworkClientState.ToString());
    }


    //ルームに入室前に呼び出される
    public override void OnConnectedToMaster()
    {
        // "room"という名前のルームに参加する（ルームが無ければ作成してから参加する）
        PhotonNetwork.JoinRandomRoom();
        
    }

    //ルームに入室後に呼び出される
    public override void OnJoinedRoom()
    {
        StartGame();


        //キャラクターを生成(後でやるかも)
        // PhotonNetwork.Instantiate("PlayerAvatar", new Vector3(2.295f,0.602f,-0.018f), Quaternion.identity, 0);
         //hotonNetwork.Instantiate("ClearBananaMan",new Vector3(2.299f,-0.33f,-0.2f), Quaternion.identity, 0);
        //自分だけが操作できるようにスクリプトを有効にする


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

        //TODO: 無限ループ
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



