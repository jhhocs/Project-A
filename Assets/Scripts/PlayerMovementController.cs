using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class PlayerMovementController : NetworkBehaviour
{
    public float moveSpeed;
    public GameObject PlayerModel;

    public Rigidbody2D rb;
    public Animator animator;

    [SerializeField] Transform DirectionalIndicator;
    Vector2 movement;

    private void Start()
    {
        PlayerModel.SetActive(false);
    }

    // Update is called once per frame -- Input
    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "Game")
        {
            if(PlayerModel.activeSelf == false)
            {
                SetPosition();
                PlayerModel.SetActive(true);
            }
            if(hasAuthority)
            {
                MovementInput();
                Rotate();
            }
        }
    }

    public void SetPosition()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    // FixedUpdate is called 50 times per second -- Movement
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    void Rotate()
    {
        float angle = Utility.AngleTowardsMouse(DirectionalIndicator.position);
        DirectionalIndicator.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }

    public void MovementInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.x != 0 || movement.y != 0)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        }
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }
}
