using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed;

    public Rigidbody2D rb;
    public Animator animator;

    [SerializeField] Transform DirectionalIndicator;

    Vector2 movement;

    // Update is called once per frame -- Input
    void Update()
    {
        MovementInput();
        Rotate();
    }

    // FixedUpdate is called 50 times per second -- Movement
    void FixedUpdate() 
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    void Rotate() {
        float angle = Utility.AngleTowardsMouse(DirectionalIndicator.position);
        DirectionalIndicator.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }

    void MovementInput () {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(movement.x != 0 || movement.y != 0) {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        }
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }
}
