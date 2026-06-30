using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatAir : MonoBehaviour
{
    private Animator animator;
    private VegitoController controller;

    private bool isAirAttacking;
    private bool canAirCombo;
    private bool attackBuffered;
    private int airComboStep = 0;

    public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask enemyLayer;
    public int damage = 40;

    private HashSet<EnemyHealth> hitEnemies = new HashSet<EnemyHealth>();
    void Awake()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<VegitoController>();
    }

    void Update()
    {
        if (controller == null) return;

        // ❗ Nếu đang dưới đất thì không xử lý air input
        if (controller.isGrounded) return;

        if (Input.GetKeyDown(KeyCode.J))
        {
            attackBuffered = true;
            TryAirAttack();
        }
    }

    public void TryAirAttack()
    {
        // Nếu đang ground thì chặn (bảo vệ thêm)
        if (controller.isGrounded) return;

        if (!isAirAttacking)
        {
            isAirAttacking = true;
            airComboStep = 1;
            canAirCombo = false;
            attackBuffered = false;

            animator.ResetTrigger("Air Attack 2");
            animator.SetTrigger("Air Attack");
        }
        else if (canAirCombo && airComboStep == 1)
        {
            airComboStep = 2;
            canAirCombo = false;
            attackBuffered = false;

            animator.ResetTrigger("Air Attack");
            animator.SetTrigger("Air Attack 2");
        }

        hitEnemies.Clear();
    }

    // 🔹 Animation Event giữa Air Attack 1
    public void EnableAirCombo()
    {
        if (airComboStep == 1)
        {
            canAirCombo = true;

            if (attackBuffered)
            {
                TryAirAttack();
            }
        }
    }

    // 🔹 Animation Event cuối Air Attack 2 (hoặc cuối Air Attack nếu không combo)
    public void EndAirAttack()
    {
        ResetAirCombo();
    }

    private void ResetAirCombo()
    {
        isAirAttacking = false;
        canAirCombo = false;
        airComboStep = 0;
        attackBuffered = false;

        animator.ResetTrigger("Air Attack");
        animator.ResetTrigger("Air Attack 2");
    }

    public void DealAirDamage()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRange,
            enemyLayer
        );

        foreach (Collider2D hit in hits)
        {
            EnemyHealth enemy = hit.GetComponent<EnemyHealth>();

            if (enemy != null && !hitEnemies.Contains(enemy))
            {
                hitEnemies.Add(enemy);
                enemy.TakeDamage(damage);

                Debug.Log("Air Hit: " + hit.name);
            }
        }
    }
}