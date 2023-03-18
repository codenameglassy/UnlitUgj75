using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Transform playerTransform;
   
    private void Awake()
    {
        instance = this;

        Init();
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    void Init()
    {
       
    }

 
}
