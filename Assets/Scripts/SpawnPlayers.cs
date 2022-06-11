using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SpawnPlayers : MonoBehaviour
{
    public GameObject player;
    public float minX, minY, maxX, maxY;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        PhotonNetwork.Instantiate(player.name, randomPosition, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
