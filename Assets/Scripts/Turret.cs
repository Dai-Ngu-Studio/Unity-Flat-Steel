using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

public class Turret : MonoBehaviourPun
{
    public List<Transform> turretBarrels;
    public GameObject bulletPrefab;
    public float reloadDelay = 0.5f;

    private bool canShoot = true;
    private Collider2D[] tankColliders;
    public float currentDelay = 0;

    public UnityEvent OnShoot, OnCantShoot;
    public UnityEvent<float> OnReloading;

    private PhotonView view;

    private void Awake()
    {
        tankColliders = GetComponentsInParent<Collider2D>();
    }

    private void Start()
    {
        view = GetComponent<PhotonView>();
        OnReloading?.Invoke(currentDelay);
    }

    private void Update()
    {
        if (canShoot == false)
        {
            currentDelay -= Time.deltaTime;
            OnReloading?.Invoke(currentDelay);
            if (currentDelay <= 0)
            {
                canShoot = true;
            }
        }
    }

    // public void Shoot()
    // {
    //     view.RPC("ShootRPC", RpcTarget.AllBuffered);
    // }

    
    public void Shoot()
    {
        if (canShoot)
        {
            canShoot = false;
            currentDelay = reloadDelay;
            foreach (var barrel in turretBarrels)
            {
                Vector2 bulletStartVector = new Vector2();
                bulletStartVector = barrel.position;
                GameObject bullet =
                    PhotonNetwork.Instantiate(bulletPrefab.name, bulletStartVector, Quaternion.identity);
                // bullet.transform.position = barrel.position;
                bullet.transform.localRotation = barrel.rotation;
                bullet.GetComponent<Bullet>().Initialize();
                foreach (var collider in tankColliders)
                {
                    Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), collider);
                }
            }
            view.RPC("OnShootRPCEvent", RpcTarget.AllBuffered);
            OnReloading?.Invoke(currentDelay);
        }
        else
        {
            OnCantShoot?.Invoke();
        }
    }
    [PunRPC]
    public void OnShootRPCEvent()
    {
        OnShoot?.Invoke();
    }
}