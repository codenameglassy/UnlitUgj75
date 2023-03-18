using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    public Animator anim;
    Rigidbody rb;
    Vector3 currentMovement;

    [Header("Movement")]
    public float moveSpeed, spinSpeed;
    private float currentSpeed;
    public float jumpForce;
    public float rotationFactorPerFrame = 1.0f;
    bool isMoving;
    bool canMove = true;

    [Header("CheckSurroundings")]
    public Transform checkGroundPos;
    public float checkGroundRadius;
    public LayerMask whatIsGround;
    bool isGrounded;
    public Transform checkPos;
    public float checkRadius;
    public LayerMask whatIsEnemy;
    bool checkAttack = false;

    [Header("Projectile")]
    public GameObject bombPrefab;
    public Transform projectileSpawnPos;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        HandleRotation();
        CheckSurroundings();

        if (Input.GetKeyDown(KeyCode.Space)) //&& isGrounded
        {
            /* Debug.Log("Jumping");
             currentMovement.y = jumpForce;*/
            anim.SetTrigger("throwBomb");
            //StartCoroutine(Attack());
            StartCoroutine(SpinAttack());
           
        }
    }

    bool isAttacking = false;
    IEnumerator Attack()
    {
        if (isAttacking)
        {
            yield break;
        }
        isAttacking = true;
        canMove = false;
        yield return new WaitForSeconds(0.1f);
        Instantiate(bombPrefab, projectileSpawnPos.position, transform.rotation);
        yield return new WaitForSeconds(0.3f);
        canMove = true;
        isAttacking = false;
    }

    IEnumerator SpinAttack()
    {
        if (isAttacking)
        {
            yield break;
        }
        isAttacking = true;
        canMove = false;
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        anim.SetTrigger("attack");
        AudioManagerCS.instance.Play("spin2");
        yield return new WaitForSeconds(0.1f);
        //int index = Random.Range(1, 3);
     
        currentSpeed = spinSpeed;
        anim.SetBool("spin", true);
        checkAttack = true;
        AudioManagerCS.instance.Play("spin1");
        yield return new WaitForSeconds(0.5f);
        canMove = true;



        yield return new WaitForSeconds(3f);
        anim.SetBool("spin", false);
        currentSpeed = moveSpeed;
        isAttacking = false;
        checkAttack = false;


    }

    public void ResetAttack()
    {
        anim.SetBool("spin", false);
    }

    private void HandleInput()
    {
        if (!canMove)
        {
           
            rb.velocity = Vector3.zero;
            return;
        }
        currentMovement.z = Input.GetAxis("Vertical");
        currentMovement.x = Input.GetAxis("Horizontal");

        if ((Mathf.Abs(currentMovement.z) + Mathf.Abs(currentMovement.x)) >= 0.1)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if (isGrounded)
        {
            currentMovement.y = 0.0f;

        }
        else
        {
            currentMovement.y = -1.0f;
        }

        anim.SetFloat("speed", (Mathf.Abs(currentMovement.z) + Mathf.Abs(currentMovement.x)));
        anim.SetBool("isGrounded", !isGrounded);
       

    }

    private void FixedUpdate()
    {
        if (!canMove)
        {
            return;
        }
        rb.velocity = currentMovement * currentSpeed * Time.deltaTime;

       
    }

    void HandleRotation()
    {
        Vector3 positionToLookAt;
        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;

        Quaternion currentRotation = transform.rotation;

        if (isMoving)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame);
        }



    }

    void CheckSurroundings()
    {
        isGrounded = Physics.CheckSphere(checkGroundPos.position, checkGroundRadius, whatIsGround);

        SpinAttackCheck();

    }

    private void SpinAttackCheck()
    {
        if (!checkAttack)
        {
            return;
        }
        Collider[] checkEnemy = Physics.OverlapSphere(checkPos.position, checkRadius, whatIsEnemy, QueryTriggerInteraction.UseGlobal);

        foreach (Collider enemy in checkEnemy)
        {
            enemy.GetComponent<Enemy>().Death();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(checkGroundPos.position, checkGroundRadius);
        Gizmos.DrawWireSphere(checkPos.position, checkRadius);
    }
}
