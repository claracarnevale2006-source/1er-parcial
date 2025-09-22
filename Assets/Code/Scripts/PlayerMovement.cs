using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference MoveInput;
    [SerializeField] private InputActionReference JumpInput;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundMagnitude = 0.2f;


    public Rigidbody2D rb;
    public float moveSpeed = 5f; //move in frames
    private Vector2 horizontalMovement;
    public bool isGrounded;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MoveInput.action.started += Move;
        MoveInput.action.performed += Move;
        MoveInput.action.canceled += Move;

        JumpInput.action.performed += Jump;
    }

    // Update is called once per frame
    void Update()
    {
       rb.linearVelocity = new Vector2(horizontalMovement.x, rb.linearVelocity.y);
    }

    private void FixedUpdate()
    {
        RaycastHit2D groundedCollision = Physics2D.Raycast(groundCheck.position, Vector2.down, groundMagnitude, groundLayer);
        if(groundedCollision.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
    public void Move(InputAction.CallbackContext context)
    {
        var InputMovement = context.ReadValue<Vector2>();
        horizontalMovement.x = InputMovement.x * moveSpeed;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(isGrounded && context.performed)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }
}


