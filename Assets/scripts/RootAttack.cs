using UnityEngine;

public class RootAttack : MonoBehaviour
{
    public float speed = 5f;
    public float lifeTime = 2f;
    public Vector2 direction = Vector2.right; // direction configurable

    private bool hasHit = false;

    private void Start()
    {
        Destroy(gameObject, lifeTime);

        // 🔁 Retourne le projectile visuellement si la direction est vers la gauche
        if (direction.x < 0)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    private void Update()
    {
        if (!hasHit)
        {
            transform.Translate(direction.normalized * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("➡️ Collision détectée avec : " + other.name);

        if (!hasHit && other.CompareTag("Enemy"))
        {
            Debug.Log("🎯 Ennemi touché !");
            other.GetComponent<Ennemi>()?.GetRooted();

            hasHit = true;
            enabled = false;

            RootProjectile rootAnim = GetComponentInChildren<RootProjectile>();
            if (rootAnim != null)
            {
                rootAnim.TriggerHit();
            }
            else
            {
                Debug.LogWarning("⚠️ Aucun RootProjectile trouvé dans les enfants !");
                Destroy(gameObject);
            }
        }
    }
}
