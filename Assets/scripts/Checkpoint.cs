using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator animator;
    private bool isActivated = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("IsActive", false);  // Assurez-vous que IsActive est false dès le début
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActivated && other.CompareTag("Player"))
        {
            Debug.Log("Checkpoint touché !");
            isActivated = true;

            // Lance l'animation d'activation
            animator.SetTrigger("Activate");
            animator.SetBool("IsActive", true);  // Active le feu allumé

            RespawnManager.instance.SetCheckpoint(transform.position);
        }
    }

    // Cette méthode est appelée à la fin de l'animation "Activate"
    public void DeactivateActiveState()
    {
        animator.SetBool("IsActive", false);  // Réinitialise le Bool à false pour arrêter la boucle
    }
}
