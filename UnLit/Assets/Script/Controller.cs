using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    CharacterController controller;

   
    Vector3 currentMovement;

    bool isMoving = false;


    public float moveSpeed, jumpHeight;
    public float rotationFactorPerFrame =  1.0f;
    public Animator anim;
    private void Awake()
    {
       // controller = GetComponent<CharacterController>();
      
    }
    private void Update()
    {
        HandleMovement();
        HandleRotation();

        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            currentMovement.y = jumpHeight;
        }
    }

    private void FixedUpdate()
    {
        controller.Move(currentMovement * moveSpeed * Time.deltaTime);
        anim.SetFloat("speed", (Mathf.Abs(currentMovement.z) + Mathf.Abs(currentMovement.x)));
    }

    void HandleMovement()
    {
       currentMovement.z = Input.GetAxis("Vertical");
       currentMovement.x = Input.GetAxis("Horizontal");

        if((Mathf.Abs(currentMovement.z) + Mathf.Abs(currentMovement.x)) >= 0.1)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        currentMovement.y = controller.isGrounded ? 0.0f : -4.0f;
       // Vector3 currentMovement = new Vector3()
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
}
