using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Transform playerTransform, startPos;
   
    private void Awake()
    {
        instance = this;

        Init();
    }

    // Start is called before the first frame update
    void Start()
    {
        AudioManagerCS.instance.Play("theme");
    }

    void Init()
    {
       
    }

    public void ResetPlayerPos()
    {
        StartCoroutine(Enum_Reset_Player());
    }

    IEnumerator Enum_Reset_Player()
    {
        yield return new WaitForSeconds(.5f);
        playerTransform.DOMove(new Vector3(playerTransform.position.x, playerTransform.position.y + 10f, playerTransform.position.z), 2f);
        yield return new WaitForSeconds(2f);
        playerTransform.DOMove(startPos.position, 3f);
    }
 
}
