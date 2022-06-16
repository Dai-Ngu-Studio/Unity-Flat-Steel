using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

public class Damagable : MonoBehaviourPun
{
    public int MaxHealth = 100;

    [SerializeField] private int currentHealth;

    public int Health
    {
        get { return currentHealth; }
        set
        {
            currentHealth = value;
            onHealthChanged?.Invoke((float)currentHealth / MaxHealth);
        }
    }

    // public UnityEvent onDead;
    public void onDead()
    {
        PhotonNetwork.Destroy(transform.parent.gameObject);
    }

    public UnityEvent<float> onHealthChanged;
    public UnityEvent onHit, onHeal;

    private PhotonView view;

    private void Start()
    {
        Health = MaxHealth;
        view = GetComponent<PhotonView>();
    }

    public void Hit(int inflictedDamage)
    {
        view.RPC("HitRPC", RpcTarget.AllBuffered, inflictedDamage);
    }

    [PunRPC]
    void HitRPC(int inflictedDamage)
    {
        Health -= inflictedDamage;
        if (Health <= 0)
        {
            onDead();
        }
        else
        {
            onHit?.Invoke();
        }
    }
}