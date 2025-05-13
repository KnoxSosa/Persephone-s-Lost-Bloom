using UnityEngine;

public class SlashPlayerAttack : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 1;
    public float attackRate = 2f;
    private float nextAttackTime = 0f;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.X)) // Touche d'attaque
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack()
    {
        Debug.Log("Attaque lancée !");
        animator?.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        Debug.Log("Ennemis détectés : " + hitEnemies.Length);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Ennemi touché : " + enemy.name);
            enemy.GetComponent<Ennemi>()?.TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
