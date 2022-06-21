using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Damagable : MonoBehaviourPun
{
    public int MaxHealth = 100;

    [SerializeField] private int currentHealth;
    private PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        Health = MaxHealth;
    }

    public int Health
    {
        get { return currentHealth; }
        set
        {
            currentHealth = value;
            if (view.IsMine)
            {
                UpdateHealthUI((float)currentHealth / MaxHealth);
            }
        }
    }

    // public UnityEvent onDead;
    public void onDead()
    {
        PhotonNetwork.Destroy(transform.parent.gameObject);
    }

    public void UpdateHealthUI(float currentPercentageHealth)
    {
        Slider slider = GameObject.Find("HealthBar").GetComponent<Slider>();
        slider.value = currentPercentageHealth;
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
    }
}