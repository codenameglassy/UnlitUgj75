using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Key : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOLocalRotate(new Vector3(0, 360, 0), 2f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Collect());
        }
    }

    IEnumerator Collect()
    {
        transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), .5f).SetEase(Ease.OutBounce);
        AudioManagerCS.instance.Play("key");
        yield return new WaitForSeconds(.5f);
        GameManager.instance.AddKey();
        Destroy(gameObject);
    }

}
