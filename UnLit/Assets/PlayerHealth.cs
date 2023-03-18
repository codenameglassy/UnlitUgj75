using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth;
    private float currentHealth;
    public GameObject bloodVfx;
    public Transform hitPos;
    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
       
    }

    public void TakeDamage(float damageAmt)
    {
        currentHealth -= damageAmt;
       

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Destroy(gameObject);
        }
    }

    public void Heal(float healAmt)
    {
        currentHealth += healAmt;
        if(currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
