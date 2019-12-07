using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    private float sensitivity = 5f;

    private Vector2 lookDirection, currentDirection;

    [SerializeField]
    private Transform playerTransform;

    public Transform lookRootTransform;

    void Awake()
    {
        lookRootTransform = GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        LookAround();
    }

    void LookAround()
    {
        lookDirection = new Vector2(-Input.GetAxis(MouseAxis.MOUSE_VERTICAL), Input.GetAxis(MouseAxis.MOUSE_HORIZONTAL));
        currentDirection += lookDirection * sensitivity;

        currentDirection.x = Mathf.Clamp(currentDirection.x,-80f,70f);

        playerTransform.localRotation = Quaternion.Euler(0f, currentDirection.y, 0f);
        lookRootTransform.localRotation = Quaternion.Euler(currentDirection.x, 0f, 0f);
    }
}
