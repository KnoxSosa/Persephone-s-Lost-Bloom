using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private bool isInvincible = false;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(int amount)
    {
        if (IsInvincible()) return;

        currentHealth -= amount;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            ActivateInvincibility(true); // clignotement rouge (court)
        }
    }

    void Die()
    {
        GetComponent<PlayerDeathHandler>().Die();
    }

    void UpdateHealthBar()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].sprite = i < currentHealth ? fullHeart : emptyHeart;
            hearts[i].enabled = i < maxHealth;
        }
    }

    public void RestoreFullHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    // Activation de l'invincibilité, avec paramètre pour clignotement rouge ou blanc
    public void ActivateInvincibility(bool withRedBlink)
    {
        if (isInvincible) return;

        float duration = withRedBlink ? 0.3f : 0.5f;
        StartCoroutine(InvincibilityRoutine(withRedBlink, duration));
    }

    private IEnumerator InvincibilityRoutine(bool withRedBlink, float duration)
    {
        isInvincible = true;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        float timer = 0f;
        float blinkInterval = 0.25f; // clignotement lent

        while (timer < duration)
        {
            sr.color = withRedBlink ? Color.red : Color.white;

            sr.enabled = true;
            yield return new WaitForSeconds(blinkInterval / 2);

            sr.enabled = false;
            yield return new WaitForSeconds(blinkInterval / 2);

            sr.enabled = true;
            sr.color = Color.white;

            timer += blinkInterval;
        }

        sr.color = Color.white;
        sr.enabled = true;
        isInvincible = false;
    }

    public bool IsInvincible()
    {
        return isInvincible;
    }
}
