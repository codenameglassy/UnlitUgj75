using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    [Header("Vfx")]
    public GameObject[] windVfx;
    public float MinX = 0;
    public float MaxX = 10;
    public float MinY = 0;
    public float MaxY = 10;
    public float MinZ = 0;
    public float MaxZ = 10;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnWinds", 1f, 3f);
    }

   void SpawnWinds()
    {
        float x = Random.Range(MinX, MaxX);
        float y = Random.Range(MinY, MaxY);
        float z = Random.Range(MinZ, MaxZ);

        int i = Random.Range(0, windVfx.Length);
        Instantiate(windVfx[i], new Vector3(x, y, z), Quaternion.identity);
    }
}
