using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPos;
    public GameObject enemyPrefab;

    public float startDelay, repeatDelay;
    public float maxEnemyCount;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", startDelay, repeatDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
      
        int index = Random.Range(0, spawnPos.Length);
        Instantiate(enemyPrefab, spawnPos[index].position, Quaternion.identity);
    }
}
