using UnityEngine;

public class RootAttack : MonoBehaviour
{
    public float speed = 5f;
    public float lifeTime = 2f;

    private void Start()
    {
        Destroy(gameObject, lifeTime); // se détruit au bout d’un moment
    }

    private void Update()
    {
        // Avancer horizontalement vers la droite
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Ennemi enraciné !");
            other.GetComponent<Ennemi>()?.GetRooted(); // Appelle méthode sur l'ennemi
            Destroy(gameObject);
        }
    }
}
    