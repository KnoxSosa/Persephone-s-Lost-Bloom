using UnityEngine;
using System.Collections;

public class SlashPlayerAttack : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 1;
    public float attackRate = 2f;
    public float hitStopDuration = 0.05f; // Durée du freeze

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
            // Touche X (clavier) ou bouton Carré (manette PS4)
            if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.JoystickButton3))
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

        if (hitEnemies.Length > 0)
        {
            StartCoroutine(HitStop()); // ← Petit freeze d’impact
        }

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Ennemi touché : " + enemy.name);
            enemy.GetComponent<Ennemi>()?.TakeDamage(attackDamage);
        }
    }

    IEnumerator HitStop()
    {
        float originalTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(hitStopDuration);
        Time.timeScale = originalTimeScale;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
