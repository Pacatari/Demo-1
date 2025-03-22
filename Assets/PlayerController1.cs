using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3f;         // Velocidad de movimiento horizontal
    public float maxJumpForce = 3f;     // Fuerza máxima de salto
    public float jumpChargeRate = 0.5f;   // Velocidad de carga del salto
    private float lastMoveDirection = 0f;  // Almacena la última dirección de movimiento
    public float Dash = 8f;
    private bool SaltoCorto = false ;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float jumpCharge;
    private bool isChargingJump;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

   void Update()
    {
        float move = Input.GetAxis("Horizontal");

        // Si el personaje está en el suelo, permite moverse y actualizar la dirección
        if (isGrounded)
        {
            rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

            // Guardar la última dirección válida si hay movimiento
            if (move != 0)
            {
                lastMoveDirection = move > 0 ? 1 : -1;
            }

            // Girar al moverse
            if (move > 0)
            {
                transform.localScale = new Vector3(1f,1f,1f);
            }
            else if (move < 0)
            {
                transform.localScale = new Vector3(1f,1f,1f);
            }
        }

        // Iniciar carga de salto solo si está en el suelo
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isChargingJump = true;
            jumpCharge = 0f;
        }

        // Cargar el salto
        if (isChargingJump && Input.GetKey(KeyCode.Space))
        {
            jumpCharge += jumpChargeRate * Time.deltaTime;
            jumpCharge = Mathf.Clamp(jumpCharge, 0, maxJumpForce);
        }

        // Liberar el salto
        if (Input.GetKeyUp(KeyCode.Space) && isChargingJump)
        {
            // Mantiene la última dirección marcada antes de saltar
            rb.velocity = new Vector2(lastMoveDirection * moveSpeed, jumpCharge);
            isChargingJump = false;
            isGrounded = false;  // El personaje ya no está en el suelo
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (SaltoCorto == false)
            {
            rb.velocity = new Vector2(rb.velocity.x, Dash); // Aplica la fuerza máxima de salto
            SaltoCorto = true;
            }
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            SaltoCorto = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
