using System.Collections.Generic;
using UnityEngine;

public class PlayerCombo : MonoBehaviour
{
    private Animator animator;
    private VegitoController controller;

    private bool isAttacking;
    private bool canCombo;
    private int comboStep = 0;

    public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask enemyLayer;
    public int damage = 50;

    private bool attackBuffered;

    private HashSet<EnemyHealth> hitEnemies = new HashSet<EnemyHealth>();
    void Awake()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<VegitoController>();
    }

    void Update()
    {
        if (DialogueManager.Instance != null &&
        DialogueManager.Instance.IsTalking())
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.J)) {
            InputAttack();
        }
    }

    public void InputAttack()
    {
        attackBuffered = true;
        Attack();
    }

    void Attack()
    {
       // if (!controller.isGrounded) return;
        if (!isAttacking)
        {
            //animator.ResetTrigger("Attack");
            animator.ResetTrigger("Attack2");

            isAttacking = true;
            comboStep = 1;
            canCombo = false;
            attackBuffered = false;

            hitEnemies.Clear();

            animator.SetTrigger("Attack");
        }
        //isAttacking &&
        else if (canCombo && comboStep == 1)
        {
            comboStep = 2;
            canCombo = false;
            attackBuffered = false;

            hitEnemies.Clear();

            animator.ResetTrigger("Attack"); // reset cái cũ
            animator.SetTrigger("Attack2");  // trigger mới
        }
    }
    // Gọi bằng Animation Event gần cuối Attack1
    public void EnableCombo()
    {
        if (comboStep == 1)
        { 
            canCombo = true;
            if (attackBuffered)
            {
                Attack();
            }
        }
    }

    // Gọi bằng Animation Event cuối animation
    public void EndAttack()
    {
        isAttacking = false;
        canCombo = false;
        comboStep = 0;
        attackBuffered = false;

        hitEnemies.Clear();

        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Attack2");
    }
    public void ResetCombo()
    {
        comboStep = 0;
        isAttacking = false;
    }

    //Gây Dame
    public void DealDamage()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRange,
            enemyLayer
        );

        foreach (Collider2D hit in hits)
        {
            Debug.Log("Hit: " + hit.name);

            EnemyHealth enemy = hit.GetComponent<EnemyHealth>();
            if (enemy != null && !hitEnemies.Contains(enemy))
            {
                hitEnemies.Add(enemy);
                enemy.TakeDamage(damage);

                Debug.Log("Hit: " + hit.name);
            }
        }
    }

    //Range
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}