using UnityEngine;

public class RootProjectile : MonoBehaviour
{
    private Animator animator;
    private bool hasHit = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerHit()
    {
        if (hasHit) return;

        hasHit = true;

        Debug.Log("🎯 TriggerHit() appelé — Animation Hit lancée !");

        animator.SetBool("HasHit", true);

        // Attendre que l'anim Hit se termine avant de détruire le projectile
        float hitAnimDuration = animator.GetCurrentAnimatorStateInfo(0).length;
        Destroy(gameObject, hitAnimDuration > 0 ? hitAnimDuration : 0.5f);
    }
}
