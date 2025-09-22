using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    private Rigidbody2D playerRB;
    private Animator playerAnimator;
    private SpriteRenderer playerSpriteRenderer;

    private bool isJumping = false;
    private bool wasGrounded = true;
    private float playerSpeed;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        playerSpeed = Mathf.Abs(playerRB.linearVelocity.x);
        UpdateAnimations();
        FlipSprite();
    }

    void UpdateAnimations()
    {
        // Detectar cuando INICIA el salto (deja el suelo)
        if (!isJumping && !playerMovement.isGrounded && wasGrounded)
        {
            isJumping = true;
            playerAnimator.SetBool("isJumping", true);
            playerAnimator.SetBool("isWalking", false);
        }

        // Detectar cuando TERMINA el salto (vuelve al suelo)
        if (isJumping && playerMovement.isGrounded)
        {
            isJumping = false;
            playerAnimator.SetBool("isJumping", false);
        }

        // Control de walking/idle solo cuando está en el suelo y no saltando
        if (playerMovement.isGrounded && !isJumping)
        {
            playerAnimator.SetBool("isWalking", playerSpeed > 0.1f);
        }

        // Guardar el estado actual para el próximo frame
        wasGrounded = playerMovement.isGrounded;
    }

    void FlipSprite()
    {
        if (playerRB.linearVelocity.x > 0.1f)
        {
            playerSpriteRenderer.flipX = false;
        }
        else if (playerRB.linearVelocity.x < -0.1f)
        {
            playerSpriteRenderer.flipX = true;
        }
    }
}