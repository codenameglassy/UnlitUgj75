using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Coin : MonoBehaviour
{
    Rigidbody rb;

    bool canCollect = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Invoke("EnableCollect", .5f);
        StartCoroutine(Enum_Spread());
     
    }

    private void Update()
    {
       
       

    }

    IEnumerator Enum_Spread()
    {
        rb.AddRelativeForce(Random.onUnitSphere * 20f);
        yield return new WaitForSeconds(1f);
        //rb.isKinematic = true;
        yield return new WaitForSeconds(1f);
        //StartCoroutine(Collected());
        //transform.DOMove(new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), 1f).SetLoops(-1, LoopType.Yoyo);

    }
  
    void EnableCollect()
    {
        canCollect = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!canCollect)
            {
                return;
            }
            Collect();
        }
    }

    private void Collect()
    {
      
        CoinManager.instance.AddCoin();
        StartCoroutine(Collected());
    }
    bool goToPlayer = false;
    IEnumerator Collected()
    {
        //transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), .5f);
       // yield return new WaitForSeconds(0.25f);
        transform.DOScale(new Vector3(0.2f, 0.2f, 0.2f), .5f).SetEase(Ease.InOutBounce);
        yield return new WaitForSeconds(0.25f);
        AudioManagerCS.instance.Play("coin");
        Destroy(gameObject);
    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;
        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }
}
