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
    public float invincibilityDuration = 1.5f;

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

        StartCoroutine(FlashRed()); // Ajout de l'effet rouge

        if (currentHealth <= 0)
        {
            Die();
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
            if (i < currentHealth)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;

            hearts[i].enabled = i < maxHealth;
        }
    }

    public void RestoreFullHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void ActivateInvincibility()
    {
        if (isInvincible) return;
        StartCoroutine(InvincibilityRoutine());
    }

    private IEnumerator InvincibilityRoutine()
    {
        isInvincible = true;
        float timer = 0f;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        while (timer < invincibilityDuration)
        {
            sr.enabled = false;
            yield return new WaitForSeconds(0.1f);
            sr.enabled = true;
            yield return new WaitForSeconds(0.1f);
            timer += 0.2f;
        }

        isInvincible = false;
    }

    private IEnumerator FlashRed()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color originalColor = sr.color;

        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = originalColor;
    }

    public bool IsInvincible()
    {
        return isInvincible;
    }
}
