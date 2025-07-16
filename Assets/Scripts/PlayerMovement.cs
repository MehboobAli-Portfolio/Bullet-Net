using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed=3.5f; // Speed at which the player moves
    // Rigidbody component for physics-based movement
    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the Rigidbody component attached to this GameObject
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get input from the user
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Create a movement vector
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Move the player
        rb.MovePosition(transform.position + movement * moveSpeed * Time.deltaTime);   
    }
}
