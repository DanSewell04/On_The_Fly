using System;
using UnityEngine;

public class DashMovement : MonoBehaviour
{
    public float dashForce = 25f;
    public float dashCooldown = 1f;
    public float dashDuration = 0.2f;
    public KeyCode dashKey = KeyCode.LeftShift;

    private Rigidbody rb;
    private bool isDashing = false;
    private float dashTimer = 0f;
    private float cooldownTimer = 0f;
    private Vector3 dashDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (Input.GetKeyDown(dashKey) && cooldownTimer <= 0f && !isDashing)
        {
            StartDash();
        }

        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0f)
            {
                EndDash();
            }
        }
    }

    void StartDash()
    {
        dashDirection = transform.forward;
        rb.linearVelocity = Vector3.zero;
        rb.AddForce(dashDirection * dashForce, ForceMode.VelocityChange);
        isDashing = true;
        dashTimer = dashDuration;
        cooldownTimer = dashCooldown;

       
    }

    void EndDash()
    {
        isDashing = false;
    }
}
