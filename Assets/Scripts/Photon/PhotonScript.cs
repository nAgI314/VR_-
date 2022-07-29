using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonScript : MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject networkPlayer;

    [SerializeField]
    Transform cameraRig;

    [SerializeField]
    Transform[] playerPositions;

    GameObject player;

    void Start()
    {
        //PhotonServerSettings�ɐݒ肵�����e���g���ă}�X�^�[�T�[�o�[�֐ڑ�����
        PhotonNetwork.ConnectUsingSettings();
    }

    //�}�X�^�[�T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    public override void OnConnectedToMaster()
    {
        // "OnoTest"�Ƃ������O�̃��[���ɎQ������i���[����������΍쐬���Ă���Q������j
        PhotonNetwork.JoinOrCreateRoom("OnoTest", new RoomOptions(), TypedLobby.Default);
        print("���[���쐬����");
    }

    //�����ɓ�������A�o�^�[����
    public override void OnJoinedRoom()
    {
        //int othersCount = PhotonNetwork.PlayerListOthers.Length;
        PhotonNetwork.Instantiate(networkPlayer.name, new Vector3(2.29f,0.6f,0), Quaternion.identity);
        cameraRig.position = new Vector3(2.3f,0.45f,0f);
    }
}