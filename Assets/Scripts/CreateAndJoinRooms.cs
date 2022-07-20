using Photon.Pun;
using TMPro;
using UnityEngine;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public TMP_InputField createInput;
    public TMP_InputField joinInput;
    public TMP_InputField idInput;

    public void CreateRoom()
    {
        if (string.IsNullOrWhiteSpace(idInput.text)) return;
        PhotonNetwork.AuthValues.UserId = idInput.text;
        PhotonNetwork.CreateRoom(createInput.text);

    }

    public void JoinRoom()
    {
        if (string.IsNullOrWhiteSpace(idInput.text)) return;
        PhotonNetwork.AuthValues.UserId = idInput.text;
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.CustomProperties == null);
        if (PhotonNetwork.CurrentRoom.CustomProperties == null)
        {
            PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable());
        }
        PhotonNetwork.LoadLevel("Game");
    }

    public override void OnLeftRoom()
    {
        Debug.Log("AVVVVVVVVVVVV");
    }
}
