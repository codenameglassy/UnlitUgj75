using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    Rigidbody rb;

    public float speed, checkRadius;
    public LayerMask whatIsPlayer;
    public Transform checkPos;
    public GameObject destroVfx, bloodVfx;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    private void Update()
    {
        bool checkPlayer = Physics.CheckSphere(checkPos.position, checkRadius, whatIsPlayer);

        if (checkPlayer)
        {
            if (GameManager.instance.player.isAttacking)
            {
                Instantiate(destroVfx, transform.position, Quaternion.identity);
                AudioManagerCS.instance.Play("deflect");
                FindObjectOfType<PlayerHealth>().TakeDamage(5f);
                Destroy(gameObject);
                return;
            }
            Instantiate(destroVfx, transform.position, Quaternion.identity);
            Instantiate(bloodVfx, checkPos.position, Quaternion.identity);
            AudioManagerCS.instance.Play("hit");
            FindObjectOfType<PlayerHealth>().TakeDamage(10f);
            Destroy(gameObject);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(checkPos.position, checkRadius);
    }

}
