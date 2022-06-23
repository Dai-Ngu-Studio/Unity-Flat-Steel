using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject player;
    public float minX, minY, maxX, maxY;

    private CinemachineVirtualCamera cinemachineVirtualCamera;

    void Start()
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        var playerOnServer = PhotonNetwork.Instantiate(player.name, randomPosition, Quaternion.identity);
        var playerTransform = playerOnServer.transform.Find("Tank/TankTurretParent").gameObject.transform;

        Camera.main.gameObject.TryGetComponent<CinemachineBrain>(out var brain);
        if (brain == null)
        {
            brain = Camera.main.gameObject.AddComponent<CinemachineBrain>();
        }

        cinemachineVirtualCamera = gameObject.GetComponent<CinemachineVirtualCamera>();
        cinemachineVirtualCamera.Follow = playerTransform;
        cinemachineVirtualCamera.LookAt = playerTransform;
    }

    // Update is called once per frame
    void Update()
    {
    }
}