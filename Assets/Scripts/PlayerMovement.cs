using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://youtu.be/_QajrabyTJc  
public class PlayerMovement : MonoBehaviour
{
    private CharacterController _controller;
    
    [Range(20f, 50f)]
    public float speed;
    public float gravity = - 9.81f;
    public float playerHealth;
    

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float jumpHeight = 3f;
    
    private Vector3 velocity;
    private bool isGrounded;
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        _controller.Move(move * (speed * Time.deltaTime));
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; 
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        
        }
        
        velocity.y += gravity * Time.deltaTime;
        _controller.Move(velocity * Time.deltaTime);
    }
    
    public void TakeDamage(float amount)
    {
        playerHealth -= amount;
        Debug.Log("health has decresed" + playerHealth);
        if (playerHealth <= 0f)
        {
            Die();
        }
    }
    
    //destroy the target 
    void Die()
    {
        Destroy(gameObject);
    }
}
