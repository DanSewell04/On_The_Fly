using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Android;

public class PlayerController : MonoBehaviour
{
    public Animator Animator;
    private Rigidbody rb;
    public LayerMask layerMask;
    public bool grounded;

    [Header("Looking")]
    public Transform cameraTransform;
    public float mouseSensitivity = 100f;
    private float xRotation = 0f;

    [Header("Movement")]
    public float movementSpeed = 5f;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Grounded();
        Move();
    }

    private void Update()
    {
        Look();
    }

    private void Grounded()
    {
        Vector3 checkPos = transform.position + Vector3.down * 1.1f;
        grounded = Physics.CheckSphere(checkPos, 0.4f, layerMask);
    }

    private void Move()
    {
        float verticalAxis = Input.GetAxis("Vertical");
        float horizontalAxis = Input.GetAxis("Horizontal");

        Vector3 movement = transform.forward * verticalAxis + transform.right * horizontalAxis;
        if (movement.magnitude > 1) movement.Normalize();

        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);


        Animator.SetFloat("Vertical", verticalAxis);
        Animator.SetFloat("Horizontal", horizontalAxis);
    }

    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Prevent full spin

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
