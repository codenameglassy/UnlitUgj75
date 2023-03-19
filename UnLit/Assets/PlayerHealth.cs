using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth;
    private float currentHealth;
    public GameObject bloodVfx;
    public Transform hitPos;
    public TextMeshProUGUI heartText;
    private void Awake()
    {
        currentHealth = maxHealth;
        heartText.text = currentHealth.ToString();
    }

    private void Start()
    {
       
    }

    public void TakeDamage(float damageAmt)
    {
        currentHealth -= damageAmt;
        heartText.text = currentHealth.ToString();
        heartText.transform.DOScale(new Vector2(1.5f, 1.5f), .1f).OnComplete(ScaleBack);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            heartText.text = currentHealth.ToString();
            heartText.transform.DOScale(new Vector2(1.5f, 1.5f), .1f).OnComplete(ScaleBack);

            Destroy(gameObject);
        }
    }

    public void Heal(float healAmt)
    {
        currentHealth += healAmt;

        if(currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
            heartText.text = currentHealth.ToString();
            heartText.transform.DOScale(new Vector2(1.5f, 1.5f), .1f).OnComplete(ScaleBack);

            return;
        }

        heartText.text = currentHealth.ToString();
        heartText.transform.DOScale(new Vector2(1.5f, 1.5f), .1f).OnComplete(ScaleBack);

    }

    void ScaleBack()
    {
        heartText.transform.DOScale(new Vector2(1f, 1f), .1f);
    }
}
