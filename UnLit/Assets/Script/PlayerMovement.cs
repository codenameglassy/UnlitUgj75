using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    public Animator anim;
    Rigidbody rb;
    Vector3 currentMovement;

    [Header("Movement")]
    public float moveSpeed;
    public float jumpForce;
    public float rotationFactorPerFrame = 1.0f;
    bool isMoving;

    [Header("CheckSurroundings")]
    public Transform checkGroundPos;
    public float checkGroundRadius;
    public LayerMask whatIsGround;
    bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        HandleRotation();
        CheckSurroundings();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log("Jumping");
            currentMovement.y = jumpForce;
            //rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void HandleInput()
    {
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
        rb.velocity = currentMovement * moveSpeed * Time.deltaTime;

       
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

        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(checkGroundPos.position, checkGroundRadius);
    }
}
