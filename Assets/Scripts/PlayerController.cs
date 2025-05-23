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
    public Camera playerCamera;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCamera.fieldOfView = SettingsManager.Instance.fieldOfView;
        
    }

    private void FixedUpdate()
    {
        Grounded();
        Move();
    }

    private void Update()
    {
        Jump();
        float mouseX = Input.GetAxis("Mouse X") * SettingsManager.Instance.mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * SettingsManager.Instance.mouseSensitivity;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
            Animator.SetTrigger("Jump"); 
        }
    }

    private void Grounded()
    {
        grounded = Physics.CheckSphere(transform.position + Vector3.down * 0.5f, 0.3f, layerMask);
        Debug.Log($"Grounded: {grounded}");
    }

    private void Move()
    {
        float verticalAxis = Input.GetAxis("Vertical");
        float horizontalAxis = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(horizontalAxis, 0, verticalAxis);
        if (movement.magnitude > 1) movement.Normalize();

        rb.MovePosition(rb.position + movement * 0.04f);

        // Send movement values to Animator (which is on the CharacterModel)
        Animator.SetFloat("Vertical", verticalAxis);
        Animator.SetFloat("Horizontal", horizontalAxis);
    }
}
