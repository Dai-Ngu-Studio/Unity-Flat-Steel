using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    public int MaxHealth;

    [SerializeField]
    private int currentHealth
    {
        get
        {
            return currentHealth;
        }
        set
        {
            currentHealth = value;
            
        }
    }
    
}
