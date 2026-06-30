using UnityEngine;
using UnityEngine.InputSystem;

public class VegitoController : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;
    private Animator animator;

    [Header("Movement")]
    public float moveSpeed = 6f;
    public float jumpForce = 12f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundRadius = 0.15f;
    public LayerMask groundLayer;

    private float moveInput;
    public bool isGrounded { get; private set; }
    public bool facingRight { get; private set; } = true;
    public bool isAttacking { get; set; }

    public float mobileMoveInput;

    private SpriteRenderer spriteRenderer;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
       // isGrounded = false;   // ép luôn
        CheckGround();
        float keyboardInput = Input.GetAxisRaw("Horizontal");

        moveInput = keyboardInput;

        if (mobileMoveInput != 0)
        {
            moveInput = mobileMoveInput;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Grounded = " + isGrounded);
            Jump();
        }
        UpdateAnimator();

        if (DialogueManager.Instance != null &&
        DialogueManager.Instance.IsTalking())
        {
            rb.velocity = Vector2.zero;
            animator.SetFloat("Speed", 0);
            return;
        }

    }

    void FixedUpdate()
    {
        if (DialogueManager.Instance != null &&
        DialogueManager.Instance.IsTalking())
        {
            rb.velocity = Vector2.zero;
            return;
        }

        Move();
        Debug.Log(isGrounded);
    }

    void Move()
    {
        if (isAttacking)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            return;
        }
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        if (moveInput > 0 && !facingRight)
            Flip();
        else if (moveInput < 0 && facingRight)
            Flip();
    }

    public void MoveLeft()
    {
        mobileMoveInput = -1;
    }

    public void MoveRight()
    {
        mobileMoveInput = 1;
    }

    public void StopMove()
    {
        mobileMoveInput = 0;
    }
    public void InputJump()
    {
        Jump();
    }

    public void Jump()
    {
        if (!isGrounded)
            return;

        Debug.Log("Trying to jump, grounded = " + isGrounded);
        //if (!isGrounded) return;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        animator.SetTrigger("Jump");
    }

    void Flip()
    {
        facingRight = !facingRight;
        spriteRenderer.flipX = !facingRight;
    }

    void CheckGround()
    {
        if (groundCheck == null) {
            Debug.LogError("GroundCheck chưa được gán trong Inspector!");
            return;
        }
        isGrounded = Physics2D.OverlapCircle(
        groundCheck.position,
        groundRadius,
        groundLayer
        );
        animator.SetBool("isGrounded", isGrounded);
    }

    void UpdateAnimator()
    {
        animator.SetFloat("Speed", Mathf.Abs(moveInput));
        animator.SetFloat("VerticalVelocity", rb.velocity.y);
    }
    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>().x;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Jump();
        }
    }
}