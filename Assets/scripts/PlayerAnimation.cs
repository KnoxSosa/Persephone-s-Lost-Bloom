using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private PlayerMovement movement;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        float xVelocity = Mathf.Abs(rb.linearVelocity.x);
        float yVelocity = rb.linearVelocity.y;
        bool grounded = IsGrounded();

        // Paramètres clairs
        bool isRunning = xVelocity > 0.1f && grounded;
        bool isJumpingUp = yVelocity > 0.1f && !grounded;
        bool isFalling = yVelocity < -0.1f && !grounded;
        bool isIdle = xVelocity <= 0.1f && grounded;

        // Reset des états
        anim.SetBool("isRunning", isRunning);
        anim.SetBool("isJumpingUp", isJumpingUp);
        anim.SetBool("isFalling", isFalling);
        anim.SetBool("isIdle", isIdle);
    }
}