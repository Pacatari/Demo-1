using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Constantes
    private const float RunSpeed = 4f; // Velocidad de movimiento lateral
    private const float MinJumpForce = 5f; // Fuerza mínima de salto
    private const float MaxJumpForce = 20f; // Fuerza máxima de salto
    private const float JumpChargeRate = 0.5f; // Tasa de carga del salto
    private const float GravityMultiplier = 2f; // Multiplicador de gravedad para caídas rápidas
    private const float TerminalVelocity = -30f; // Velocidad terminal máxima

    // Variables de estado
    private Rigidbody2D rb;
    private bool isOnGround;
    private bool jumpHeld;
    private float jumpForce;
    private float currentGravityScale;
    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentGravityScale = rb.gravityScale;
    }

    void Update()
    {
        HandleInputs();
        HandleJump();
        HandleMovement();
    }

    void FixedUpdate()
    {
        ApplyGravity();
    }

    private void HandleInputs()
    {
        // Movimiento lateral
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalInput * RunSpeed, rb.velocity.y);

        // Salto
        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            jumpHeld = true;
            jumpForce = MinJumpForce;
        }

        if (Input.GetButtonUp("Jump"))
        {
            jumpHeld = false;
        }
    }

    private void HandleJump()
    {
        if (jumpHeld && isOnGround)
        {
            // Incrementar la fuerza del salto mientras se mantiene presionado el botón
            jumpForce = Mathf.Clamp(jumpForce + JumpChargeRate * Time.deltaTime, MinJumpForce, MaxJumpForce);
        }

        if (!isOnGround && !jumpHeld)
        {
            // Aumentar la gravedad rápidamente si el jugador suelta el botón de salto
            currentGravityScale = GravityMultiplier;
        }
        else
        {
            currentGravityScale = 1f;
        }

        if (jumpHeld && isOnGround)
        {
            // Aplicar la fuerza del salto
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isOnGround = false;
        }
    }

    private void HandleMovement()
    {
        // Cambiar la dirección del sprite según el movimiento
        if (rb.velocity.x > 0 && !facingRight)
        {
            FlipSprite();
        }
        else if (rb.velocity.x < 0 && facingRight)
        {
            FlipSprite();
        }
    }

    private void ApplyGravity()
    {
        if (!isOnGround)
        {
            rb.velocity += Vector2.down * currentGravityScale * Physics2D.gravity.magnitude * Time.fixedDeltaTime;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, TerminalVelocity));
        }
    }

    private void FlipSprite()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            currentGravityScale = 1f; // Restablecer la gravedad
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = false;
        }
    }
}