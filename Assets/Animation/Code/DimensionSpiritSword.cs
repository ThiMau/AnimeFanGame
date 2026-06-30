using UnityEngine;
using System.Collections;

public class DimensionSpiritSword : MonoBehaviour
{
    public GameObject hitbox;
    public float dashForce = 14f;
    public float dashDuration = 0.25f;

    private Rigidbody2D rb;
    private Animator animator;
    private VegitoController controller;

    private bool isDashing;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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

        if (Input.GetKeyDown(KeyCode.L) && !isDashing )
        {
            Debug.Log("Dash triggered");
            StartCoroutine(DashAttack());
        }
        //StartCoroutine(DashAttack());
        
    }

    IEnumerator DashAttack()
    {
        isDashing = true;

        animator.SetTrigger("Dimension Spirit Sword");

        hitbox.SetActive(true);

        float direction = controller.facingRight ? 1 : -1;

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f; // không rơi khi dash

        rb.velocity = new Vector2(direction * dashForce, 0);

        yield return new WaitForSeconds(dashDuration);

        rb.gravityScale = originalGravity;
        rb.velocity = new Vector2(0, rb.velocity.y);

        hitbox.SetActive(false);

        isDashing = false;
    }
}