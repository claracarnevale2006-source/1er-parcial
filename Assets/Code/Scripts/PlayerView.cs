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
        //Detects when the jump starts and the player leaves the ground
        if (!isJumping && !playerMovement.isGrounded && wasGrounded)
        {
            isJumping = true;
            playerAnimator.SetBool("isJumping", true);
            playerAnimator.SetBool("isWalking", false);
        }

        //Detects when the jump finishes and the player goes back to the ground
        if (isJumping && playerMovement.isGrounded)
        {
            isJumping = false;
            playerAnimator.SetBool("isJumping", false);
        }

        //Walking/idle control when the player is in the ground and not jumping
        if (playerMovement.isGrounded && !isJumping)
        {
            playerAnimator.SetBool("isWalking", playerSpeed > 0.1f);
        }

        //Saves the actual state for the next frame
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