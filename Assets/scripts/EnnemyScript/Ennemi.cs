using UnityEngine;

public class Ennemi : MonoBehaviour
{
    public float speed = 2f;
    public Transform pointA;
    public Transform pointB;
    private bool movingToB = true;
    private bool isRooted = false;
    public int health = 3;

    void Update()
    {
        if (isRooted) return;

        if (movingToB)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, pointB.position) < 0.1f)
                movingToB = false;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pointA.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, pointA.position) < 0.1f)
                movingToB = true;
        }
    }

    public void GetRooted()
    {
        isRooted = true;
        transform.position -= new Vector3(0, 0.3f, 0);
        Debug.Log("L'ennemi est enraciné !");
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log("Ennemi touché ! Vie restante : " + health);
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
