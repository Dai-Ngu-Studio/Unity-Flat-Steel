using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject player;
    private float X, Y;
    public Canvas ChoosePositionCanvas;
    public Canvas HealthCanvas;
    public GameObject BackgroundMusic;
    public void SpawnAtPosition(int position)
    {
        Debug.Log(position);
        switch (position)
        {
            case 1:
                X = -13;
                Y = 7;
                break;
            case 2:
                X = 13;
                Y = 7;
                break;
            case 3:
                X = -13;
                Y = -7;
                break;
            case 4:
                X = 13;
                Y = -7;
                break;
        }

        ChoosePositionCanvas.gameObject.SetActive(false);
        HealthCanvas.gameObject.SetActive(true);
        Vector2 randomPosition = new Vector2(X,Y);
        var playerOnServer = PhotonNetwork.Instantiate(player.name, randomPosition, Quaternion.identity);
        var playerTransform = playerOnServer.transform.Find("Tank/TankTurretParent").gameObject.transform;
        
        BackgroundMusic.gameObject.SetActive(true);
        StaticScript.Instance.CameraFollower(playerTransform);
        StaticScript.Instance.ChangeOrthoSize(2.5f);
    }
}