
using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3.5f;    // Player movement speed
    public float rotateSpeed = 100f;  // Player rotation speed

    private Rigidbody rb;     // Reference to Rigidbody component
    private Animator anim;    // Reference to Animator component

    private bool isJumping = true; // Flag to check if player is jumping
    void Start()
    {
        rb = GetComponent<Rigidbody>();    // Get Rigidbody component
        anim = GetComponent<Animator>();   // Get Animator component
    }

    void FixedUpdate()
    {
        // Get movement and mouse input
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float mouseX = Input.GetAxis("Mouse X");

        // Calculate movement vector (X: left/right, Z: forward/back)
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);

        // Move the player using Rigidbody (relative to facing direction)
        rb.MovePosition(transform.position + transform.TransformDirection(movement) * moveSpeed * Time.deltaTime);

        // Rotate the player left/right based on mouse movement
        if ((moveHorizontal != 0f || moveVertical != 0f) &&mouseX != 0f)
        {
            Vector3 rotateY = new Vector3(0f, mouseX * rotateSpeed * Time.deltaTime, 0f);
            rb.MoveRotation(rb.rotation * Quaternion.Euler(rotateY));
        }

        // Update animator parameters for blending animations
        anim.SetFloat("blendV", moveVertical);
        anim.SetFloat("blendH", moveHorizontal);
    }
    private void Update()
    {
        if (Input.GetButtonDown("Jump") && isJumping)
        {
            isJumping = false;
            rb.AddForce(Vector3.up * 6.0f, ForceMode.Impulse);
            StartCoroutine(ResetJump());
        }
    }

    private IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(1f); // Simulated cooldown
        isJumping = true;
    }
}