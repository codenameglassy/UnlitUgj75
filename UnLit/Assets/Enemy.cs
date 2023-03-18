using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    [Header("References")]
    public Animator anim;
    public AIPath aiPath;
    public Seeker seeker;
    public AIDestinationSetter destinationSetter;
    public GameObject hitVfx;


    [Header("States")]
    public EnemyState enemyState;
    public enum EnemyState
    {
        Idle, Move, Death
    }
   



    [Header("Check Surroundings")]
    public float playerCheckRadius;
    public LayerMask whatIsPlayer;
    public Transform checkPos;


    [Header("Projectile")]
    public GameObject projectilePrefab;
    public Transform projectileSpawnPos;


    private void Awake()
    {
       
    }
    // Start is called before the first frame update
    void Start()
    {
        HandleState();   
    }

    // Update is called once per frame
    void Update()
    {
        //SetAnimation();
        CheckSurroundings();

    }

    private void SetState(EnemyState state)
    {
        enemyState = state;
    }

    private void HandleState()
    {
        switch (enemyState)
        {
            case EnemyState.Idle:
                StartCoroutine(IdleState());
                break;

            case EnemyState.Move:
                MoveToPlayer();
                break;

            case EnemyState.Death:
                StartCoroutine(DeathState());
                break;
        }
    }

    private void MoveToPlayer()
    {
        destinationSetter.target = GameManager.instance.playerTransform;
    }

    private void Shoot()
    {
        Instantiate(projectilePrefab, projectileSpawnPos.position, transform.rotation);
    }

    public void CheckSurroundings()
    {
        bool CheckPlayer = Physics.CheckSphere(checkPos.position, playerCheckRadius, whatIsPlayer);

        if (CheckPlayer)
        {
            SetState(EnemyState.Idle);
            HandleState();
            Debug.Log("Player is colliding");
        }
    }

    private void OnDrawGizmos()
    {
        //check player gizmos
        Gizmos.DrawWireSphere(checkPos.position, playerCheckRadius);
    }

   

    bool isIdleState = false;
    IEnumerator IdleState()
    {
        if (isIdleState)
        {
            yield break;
        }
        isIdleState = true;

        destinationSetter.target = null;
        transform.LookAt(GameManager.instance.playerTransform);

     
        Shoot();
        yield return new WaitForSeconds(1f);
        SetState(EnemyState.Move);
        HandleState();

        isIdleState = false;
    }

    bool isDeathState = false;
    IEnumerator DeathState()
    {
        if (isDeathState)
        {
            yield break;
        }
        isDeathState = true;
        Instantiate(hitVfx, checkPos.position, Quaternion.identity);
        Destroy(gameObject);

    }

    public void Death()
    {
        SetState(EnemyState.Death);
        HandleState();
    }
}
