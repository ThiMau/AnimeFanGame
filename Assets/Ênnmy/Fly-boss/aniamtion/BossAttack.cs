using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public Animator animator;
    public Transform player;

    [Header("Attack Settings")]
    public float attackRange = 10f;
    public float attackCooldown = 2f;

    private float lastAttackTime;
    private bool isAttacking;

    public GameObject fireballPrefab;
    public Transform firePoint;

    public bool isActive = false; // ❗ KEY

    void Update()
    {
        Debug.Log("Update running");

        if (!isActive)
        {
            Debug.Log("Not Active");
            return;
        }

        if (animator == null)
        {
            Debug.Log("Animator NULL");
            return;
        }

        if (player == null)
        {
            Debug.Log("Player NULL");
            return;
        }

        float distance = Vector2.Distance(transform.position, player.position);
        Debug.Log("Distance: " + distance);

        if (distance <= attackRange)
        {
            Debug.Log("IN RANGE");
            TryAttack();
        }
    }

    void TryAttack()
    {
        if (Time.time < lastAttackTime + attackCooldown) return;
        if (isAttacking) return;

        Attack();
    }



    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void SpawnFireBall()
    {
        Debug.Log("SPAWN FIREBALL"); // test

        GameObject fb = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);

        Vector2 dir = (player.position - firePoint.position).normalized;

        fb.GetComponent<FireBall>().SetDirection(dir);
    }

    void Attack()
    {
        isAttacking = true;
        lastAttackTime = Time.time;

        animator.SetTrigger("Attack");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            isActive = true;
        }
    }

    public void EndAttack()
    {
        isAttacking = false;
    }
}