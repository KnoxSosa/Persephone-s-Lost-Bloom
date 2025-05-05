using UnityEngine;

public class Ennemi : MonoBehaviour
{
    public float speed = 2f;
    public Transform pointA;
    public Transform pointB;
    private bool movingToB = true;
    private bool isRooted = false;

    void Update()
    {
        if (isRooted) return; // Stoppe le mouvement s’il est enraciné

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
        transform.position -= new Vector3(0, 0.3f, 0); // Visuellement enfoncé dans le sol
        Debug.Log("L'ennemi est enraciné !");
    }
}
