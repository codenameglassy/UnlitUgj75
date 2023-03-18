using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Projectile : MonoBehaviour
{
    Rigidbody rb;

    public float forwardForce;//, upForce;
    public float explosionsDelay;
    public GameObject explosionPrefab;

    public GameObject[] bombModels;
    public enum BombType
    {
        Normal , Silver , Gold
    }

    public BombType bombType;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        SelectBomb();
    }

    private void Start()
    {
       
        rb.AddForce(transform.forward * forwardForce, ForceMode.Impulse);
        //rb.AddForce(Vector3.up * upForce, ForceMode.Impulse);

        transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), .2f).SetEase(Ease.OutBack).OnComplete(ScaleBack);
        transform.DOLocalRotate(new Vector3(360, 0, 0), 1f, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);

        Invoke("Explode", explosionsDelay);
    }

    void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    void ScaleBack()
    {
        transform.DOScale(new Vector3(1f, 1f, 1f), .5f);
    }

    void SelectBomb()
    {
        switch (bombType)
        {
            case BombType.Normal:
                bombModels[0].SetActive(true);
                break;
            case BombType.Silver:
                bombModels[1].SetActive(true);
                break;
            case BombType.Gold:
                bombModels[2].SetActive(true);
                break;
        }
    }
}
