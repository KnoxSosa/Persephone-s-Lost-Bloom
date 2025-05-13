using UnityEngine;

public class FallDamage : MonoBehaviour
{
    public float fallThreshold = -10f;
    public int damageAmount = 1;
    public LayerMask groundLayerMask;

    private float fallVelocity;
    private Rigidbody2D rb;
    private bool isFalling = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (rb.linearVelocity.y < 0)
        {
            isFalling = true;
            fallVelocity = rb.linearVelocity.y;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isFalling && ((1 << collision.gameObject.layer) & groundLayerMask) != 0)
        {
            if (fallVelocity < fallThreshold)
            {
                GetComponent<PlayerHealth>().TakeDamage(damageAmount);
            }
            isFalling = false;
            fallVelocity = 0;
        }
    }
}
