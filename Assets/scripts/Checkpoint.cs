using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator animator;
    private bool isActivated = false;

    public AudioSource activationSource;  // Son d'activation
    public AudioSource loopSource;        // Son de feu permanent

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("IsActive", false);

        if (loopSource != null)
        {
            loopSource.Stop();  // On s'assure qu'il ne joue pas au début
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActivated && other.CompareTag("Player"))
        {
            Debug.Log("Checkpoint touché !");
            isActivated = true;

            // Jouer le son d’activation
            if (activationSource != null)
            {
                activationSource.Play();
            }

            // Lancer le son de feu en boucle
            if (loopSource != null && !loopSource.isPlaying)
            {
                loopSource.Play();
            }

            // Lancer l'animation
            animator.SetTrigger("Activate");
            animator.SetBool("IsActive", true);

            RespawnManager.instance.SetCheckpoint(transform.position);
        }
    }

    // Optionnel : méthode si tu veux désactiver le feu plus tard
    public void Deactivate()
    {
        isActivated = false;
        animator.SetBool("IsActive", false);
        if (loopSource != null && loopSource.isPlaying)
        {
            loopSource.Stop();
        }
    }

    // Si tu veux garder ça
    public void DeactivateActiveState()
    {
        animator.SetBool("IsActive", false);
    }
}
