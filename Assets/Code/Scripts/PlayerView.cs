using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    private Rigidbody2D playerRB;
    private Animator playerAnimator;
    private SpriteRenderer playerSpriteRenderer;

    bool isWalking;
    bool isJumping;
    bool isFalling;
    private float playerSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      playerRB = GetComponent<Rigidbody2D>();
      playerAnimator = GetComponent<Animator>();
      playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
       playerSpeed = Mathf.Abs(playerRB.linearVelocity.x);
        CheckMovement();
        ControlAnimations();    
        FlipSprite();
    }

    void CheckMovement()
    {
        if (playerMovement.isGrounded)
        {
            if(playerSpeed > 0)
            {
                isWalking = true;
            }
            else
            {
                isWalking = false;
            }
            
        }
        else
        {
            if(playerRB.linearVelocity.y > 0)
            {
                playerAnimator.SetBool("isJumping", true);
                playerAnimator.SetBool("isFalling", false);
            }
            else if(playerRB.linearVelocity.y < 0)
            {
                playerAnimator.SetBool("isJumping", false);
                playerAnimator.SetBool("isFalling", true);
            }
            else
            {
                playerAnimator.SetBool("isJumping", false);
                playerAnimator.SetBool("isFalling", false);
            }
        }
    }

    void ControlAnimations()
    {
        if(isWalking)
        {
            playerAnimator.SetBool("isWalking", true);
        }
        else
        {
            playerAnimator.SetBool("isWalking", false);
        }
    }

    void FlipSprite()
    {
        if(playerRB.linearVelocity.x > 0)
        {
            playerSpriteRenderer.flipX = false;
        }
        else if(playerRB.linearVelocity.x < 0)
        {
            playerSpriteRenderer.flipX = true;
        }
    }
}
