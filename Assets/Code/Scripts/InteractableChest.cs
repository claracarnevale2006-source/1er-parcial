using UnityEngine;
using UnityEngine.InputSystem;

public class CofreInteractuable : MonoBehaviour
{
    [SerializeField] private Sprite SpriteOn;
    [SerializeField] private Sprite SpriteOff;
    [SerializeField] private InputActionReference InteracAction;
    private SpriteRenderer spriteRenderer;

    public bool isOn = false;
    private bool playerInRange = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = SpriteOff;
        if (InteracAction != null)
        {
            InteracAction.action.performed += HandleInteractInput;
        }
    }

    private void OnDestroy()
    {
        if (InteracAction != null)
        {
            InteracAction.action.performed -= HandleInteractInput;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
        Debug.Log("Is close");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
        Debug.Log("Is not close");
    }

    private void HandleInteractInput(InputAction.CallbackContext context)
    {  
       
        if (playerInRange && context.performed)
        {
            isOn = !isOn;
            if (isOn)
            {
                spriteRenderer.sprite = SpriteOn;
            }
            else
            {
                spriteRenderer.sprite = SpriteOff;
            }
        }
        Debug.Log("Interact input received");
    }
}