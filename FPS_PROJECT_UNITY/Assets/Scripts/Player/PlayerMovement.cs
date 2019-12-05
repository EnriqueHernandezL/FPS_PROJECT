using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;

    [SerializeField]
    private float speed, jumpSpeed, gravity;

    private Vector3 moveDirection;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        handlePlayerMovement();
    }

    void handlePlayerMovement()
    {
        if (characterController.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f, Input.GetAxis(Axis.VERTICAL));
            moveDirection *= speed;

            if (Input.GetKeyDown(KeyCode.Space))
                moveDirection.y = jumpSpeed;
        }

        moveDirection.y -= gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);
    }
}
