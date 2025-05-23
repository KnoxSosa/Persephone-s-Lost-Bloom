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
    private float jumpCooldown = 0.1f;
    private bool wasGroundedLastFrame = false;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private Animator animator;

    public bool isAttacking = false;

    [Header("Audio")]
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip dashSound;
    [SerializeField] private AudioClip landSound;
    [SerializeField] private AudioClip[] walkClips;

    private AudioSource sfxSource;

    private int walkClipIndex = 0;
    private float stepInterval = 0.35f;
    private float stepTimer = 0f;

    private void Start()
    {
        sfxSource = gameObject.AddComponent<AudioSource>();
    }

    private void Update()
    {
        if (isDashing)
            return;

        if (isAttacking && IsGrounded())
        {
            horizontal = 0f;
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            Flip();
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        if (IsGrounded())
        {
            if (!wasGroundedLastFrame)
            {
                PlaySound(landSound);
            }
            extraJumps = extraJumpsValue;
        }

        wasGroundedLastFrame = IsGrounded();

        animator.SetBool("isJumping", !IsGrounded());

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton1)) && canJump)
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

        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.JoystickButton7)) && canDash)
        {
            StartCoroutine(Dash());
        }

        HandleWalkSteps();
        Flip();
    }

    private void FixedUpdate()
    {
        if (isDashing)
            return;

        if (!(isAttacking && IsGrounded()))
        {
            rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        }

        animator.SetFloat("xVelocity", Mathf.Abs(rb.linearVelocity.x));
        animator.SetFloat("yVelocity", rb.linearVelocity.y);
    }

    private IEnumerator PerformJump()
    {
        canJump = false;
        PlaySound(jumpSound);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
        yield return new WaitForSeconds(jumpCooldown);
        canJump = true;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        animator.SetBool("isDashing", true);

        PlaySound(dashSound);

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;

        yield return new WaitForSeconds(dashingTime);

        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        animator.SetBool("isDashing", false);

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private void HandleWalkSteps()
    {
        if (IsGrounded() && Mathf.Abs(horizontal) > 0.1f)
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
                PlayStepSound();
                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = 0f;
            walkClipIndex = 0;
        }
    }

    private void PlayStepSound()
    {
        if (walkClips.Length == 0) return;

        sfxSource.PlayOneShot(walkClips[walkClipIndex]);

        walkClipIndex++;
        if (walkClipIndex >= walkClips.Length)
            walkClipIndex = 0;
    }

    public bool IsGrounded()
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

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}
