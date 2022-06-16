using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectGeneratorRandomPositionUtil : MonoBehaviourPun
{
    public GameObject objectPrefab;

    public float radius = 0.2f;
    private PhotonView view;
    private void Start()
    {
        view = GetComponent<PhotonView>();
    }

    protected Vector2 GetRandomPosition()
    {
        return Random.insideUnitCircle * radius + (Vector2)transform.position;
    }

    protected Quaternion Random2DRotation()
    {
        return Quaternion.Euler(0, 0, Random.Range(0, 360));
    }

    public void CreteObject()
    {
        view.RPC("CreteObjectRPC", RpcTarget.AllBuffered);
    }
    [PunRPC]
    public void CreteObjectRPC()
    {
        Vector2 position = GetRandomPosition();
        GameObject impactObject = GetObject();
        impactObject.transform.position = position;
        impactObject.transform.rotation = Random2DRotation();
    }
    protected virtual GameObject GetObject()
    {
        return Instantiate(objectPrefab);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
