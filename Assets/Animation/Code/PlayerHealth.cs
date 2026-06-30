using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHp = 100;
    private int currentHp;

    public Image healthFill;

    [Header("Combat")]
    public Animator animator;
    public float hurtTime = 0.3f;

    private bool isDead = false;
    private bool isHurt = false;

    [Header("Game Over")]
    public GameOverManager gameOverManager;

    void Start()
    {
        currentHp = maxHp;
        UpdateUI();
    }

    public void TakeDamage(int dmg)
    {
        if (isDead || isHurt) return;

        currentHp -= dmg;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);

        UpdateUI();

        // 👉 play hurt animation
        if (animator != null)
            animator.SetTrigger("Hurt");

        if (currentHp <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(HurtLock());
        }
    }

    void UpdateUI()
    {
        if (healthFill != null)
        {
            healthFill.fillAmount = (float)currentHp / maxHp;
        }
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log("Player chết 💀");

        // 👉 play death animation
        if (animator != null)
            animator.SetTrigger("Death");

        // 👉 stop movement
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<VegitoController>().enabled = false;

        // 👉 delay rồi mới game over
        StartCoroutine(DeathRoutine());
    }

    IEnumerator HurtLock()
    {
        isHurt = true;
        yield return new WaitForSeconds(hurtTime);
        isHurt = false;
    }

    IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(1.2f);

        if (gameOverManager != null)
            gameOverManager.ShowGameOver();
    }
}