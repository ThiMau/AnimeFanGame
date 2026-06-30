using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public int maxHp = 4000;
    private int currentHp;

    private Animator anim;

    [Header("UI")]
    public Image healthFill;

    // ✅ NEW
    private bool isDead = false;
    private bool isInvincible = false;

    public float invincibleTime = 0.2f;

    public GameObject bossUI;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        currentHp = maxHp;
        UpdateHealthUI();

        // đảm bảo UI không bị dính từ trước
        if (bossUI != null)
            bossUI.SetActive(false);
    }

    public void TakeDamage(int dmg)
    {
        if (isDead || isInvincible) return;

        currentHp -= dmg;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);

        Debug.Log("Boss HP: " + currentHp);

        anim.SetTrigger("Hurt");

        UpdateHealthUI();

        StartCoroutine(InvincibleCoroutine());

        if (currentHp <= 0)
        {
            Die();
        }
    }

    //void OnTriggerEnter2D(Collider2D other){ if (other.CompareTag("Player")){bossUI.SetActive(true);} }

    IEnumerator InvincibleCoroutine()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
    }

    void UpdateHealthUI()
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

        anim.SetTrigger("Die");

       // if (bossUI != null){ bossUI.SetActive(false); }

        Destroy(gameObject, 1.5f);
    }
}