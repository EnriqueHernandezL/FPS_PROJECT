using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;

    [SerializeField]
    private float speed, walkSpeed, sprintSpeed, crouchSpeed, jumpSpeed, gravity, height, crouchHeight;

    private Vector3 moveDirection;

    private MovementState movementState;

    private Transform lookRootTransform;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        lookRootTransform = transform.GetChild(0);
    }

    // Start is called before the first frame update
    void Start()
    {
        movementState = MovementState.walking;
    }

    // Update is called once per frame
    void Update()
    {
        handlePlayerMovement();
    }

    void handlePlayerMovement()
    {
        switch (movementState)
        {
            case MovementState.walking:
                speed = walkSpeed;
                lookRootTransform.localPosition = new Vector3(0f, height, 0f);
                break;
            case MovementState.sprinting:
                speed = sprintSpeed;
                lookRootTransform.localPosition = new Vector3(0f, height, 0f);
                break;
            case MovementState.crouching:
                speed = crouchSpeed;
                lookRootTransform.localPosition = new Vector3(0f, crouchHeight, 0f);
                break;
        }

        if (characterController.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f, Input.GetAxis(Axis.VERTICAL));
            

            if(movementState == MovementState.sprinting)
            {
                moveDirection.x = 0f;
                moveDirection.z = Mathf.Clamp(moveDirection.z,0f,10000f);
            }

            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            if (Input.GetKeyDown(KeyCode.Space))
                moveDirection.y = jumpSpeed;

            // Only check for start/stop sprinting if we are not crouching
            if (movementState != MovementState.crouching)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    movementState = MovementState.sprinting;
                }
                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    movementState = MovementState.walking;
                }
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                movementState = MovementState.crouching;
            }
            if (Input.GetKeyUp(KeyCode.C))
            {
                movementState = MovementState.walking;
            }

        }

        moveDirection.y -= gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);
    }
}

public enum MovementState
{
    walking,
    sprinting,
    crouching
}
