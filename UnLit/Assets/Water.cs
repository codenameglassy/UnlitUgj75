using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public GameObject splashVfx;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Water Boarding");
            AudioManagerCS.instance.Play("splash");
            Instantiate(splashVfx, other.transform.position, splashVfx.transform.rotation);
            GameManager.instance.ResetPlayerPos();
        }
    }
   
}
