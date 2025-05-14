using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 9f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    private int extraJumps;
    private int extraJumpsValue = 1;

    private bool canJump = true;
    private float jumpCooldown = 0.3f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private Animator animator; // 👈 Ajout de l'Animator

    private void Update()
    {
        if (isDashing)
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        if (IsGrounded())
        {
            extraJumps = extraJumpsValue;
        }

        // Met à jour le paramètre isJumping selon si le joueur est au sol
        animator.SetBool("isJumping", !IsGrounded());

        if (Input.GetButtonDown("Jump") && canJump)
        {
            if (IsGrounded())
            {
                StartCoroutine(PerformJump());
            }
            else if (extraJumps > 0)
            {
                StartCoroutine(PerformJump());
                extraJumps--;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        Flip();
    }

    private IEnumerator PerformJump()
    {
        canJump = false;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower); // Correction: velocity, pas linearVelocity
        yield return new WaitForSeconds(jumpCooldown);
        canJump = true;
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y); // Correction: velocity

        // Met à jour les vitesses pour le blend tree dans Animator
        animator.SetFloat("xVelocity", Mathf.Abs(rb.linearVelocity.x));
        animator.SetFloat("yVelocity", rb.linearVelocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(transform.localScale.x * dashingPower, 0f); // Correction: velocity
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
