using UnityEngine;
using System.Collections;

public class Ennemi : MonoBehaviour
{
    public float speed = 2f;
    public Transform pointA;
    public Transform pointB;

    private bool movingToB = true;
    private bool isRooted = false;
    private bool isAttacking = false;
    private bool isDead = false;

    public int health = 3;

    public float detectionRadius = 5f;
    public float attackRange = 1f;
    public int attackDamage = 1;
    public float attackCooldown = 1.5f;
    public float pushForce = 5f;

    private float lastAttackTime = 0f;

    private SpriteRenderer sr;
    private Color originalColor;

    private Transform player;
    private float fixedY;

    private Animator anim;

    public GameObject iceEffectPrefab;  // <-- Assign this in the Inspector
    private GameObject iceEffectInstance;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        originalColor = sr.color;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        fixedY = transform.position.y;
    }

    void Update()
    {
        if (isDead || isRooted || isAttacking)
        {
            SetWalking(false);
            return;
        }

        if (player != null)
        {
            float distanceToPlayer = Mathf.Abs(transform.position.x - player.position.x);

            if (distanceToPlayer <= detectionRadius)
            {
                if (distanceToPlayer > attackRange)
                {
                    Vector3 targetPosition = new Vector3(player.position.x, fixedY, transform.position.z);
                    float direction = targetPosition.x - transform.position.x;

                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                    FlipSprite(direction);
                    SetWalking(true);
                }
                else
                {
                    SetWalking(false);
                    if (Time.time - lastAttackTime > attackCooldown)
                    {
                        StartCoroutine(PrepareAndAttack());
                        lastAttackTime = Time.time;
                    }
                }
            }
            else
            {
                Patrol();
            }
        }
        else
        {
            Patrol();
        }
    }

    void SetWalking(bool walking)
    {
        if (anim != null)
            anim.SetBool("IsWalking", walking);
    }

    void FlipSprite(float direction)
    {
        if (sr != null)
            sr.flipX = direction > 0;
    }

    void Patrol()
    {
        Vector3 target = movingToB ? pointB.position : pointA.position;
        target.y = fixedY;

        float direction = target.x - transform.position.x;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        FlipSprite(direction);
        SetWalking(true);

        if (Vector3.Distance(transform.position, target) < 0.1f)
            movingToB = !movingToB;
    }

    private IEnumerator PrepareAndAttack()
    {
        isAttacking = true;
        SetWalking(false);

        if (anim != null)
            anim.SetTrigger("PrepareAttack");

        yield return new WaitForSeconds(0.5f);

        if (anim != null)
            anim.SetTrigger("Attack");

        AttackPlayer();
        yield return new WaitForSeconds(0.3f);
        isAttacking = false;
    }

    void AttackPlayer()
    {
        if (player == null) return;

        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            float distance = Mathf.Abs(transform.position.x - player.position.x);
            if (distance <= attackRange + 0.5f)
            {
                playerHealth.TakeDamage(attackDamage);

                Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
                if (rb != null && rb.bodyType == RigidbodyType2D.Dynamic)
                {
                    Vector2 pushDir = (player.position - transform.position).normalized;
                    rb.AddForce(pushDir * pushForce, ForceMode2D.Impulse);
                }
            }
        }
    }

    public void GetRooted()
    {
        if (isRooted) return;

        isRooted = true;
        Debug.Log("❄️ Ennemi enraciné !");
        SetWalking(false);

        // Freeze animation sur la frame actuelle
        if (anim != null)
            anim.speed = 0f;

        // Instantie l'effet de glace à une position centrée
        if (iceEffectPrefab != null && iceEffectInstance == null)
        {
            iceEffectInstance = Instantiate(iceEffectPrefab, transform.position, Quaternion.identity, transform);
            iceEffectInstance.transform.localPosition = new Vector3(0, 1.3f, 0); // ← position ajustée ici
        }

        StartCoroutine(UnrootAfterDelay(3f));
    }

    private IEnumerator UnrootAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        isRooted = false;
        Debug.Log("✅ Ennemi libéré !");
        
        // Restaure le contrôle de l'animator
        if (anim != null)
            anim.speed = 1f;

        if (iceEffectInstance != null)
            Destroy(iceEffectInstance);
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        health -= amount;
        Debug.Log("Ennemi touché ! Vie restante : " + health);

        StartCoroutine(FlashWhite());

        if (health <= 0)
        {
            Die();
        }
    }

    private IEnumerator FlashWhite()
    {
        if (sr != null)
        {
            sr.color = Color.cyan;
            yield return new WaitForSeconds(0.1f);
            sr.color = originalColor;
        }
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        SetWalking(false);

        if (anim != null)
            anim.SetTrigger("Death");

        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;

        Destroy(gameObject, 1f);
    }
}
