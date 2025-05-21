using UnityEngine;

public class RootAttack : MonoBehaviour
{
    public float speed = 5f;
    public float lifeTime = 2f;

    private bool hasHit = false;

    private void Start()
    {
        Destroy(gameObject, lifeTime); // auto-destruction si rien n’est touché
    }

    private void Update()
    {
        if (!hasHit)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
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

            // ✅ Récupère le script dans l'enfant
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
