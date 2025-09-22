using UnityEngine;
using UnityEngine.InputSystem;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerView playerView;
    [SerializeField] private InteractableChest chestInteract;
    [SerializeField] private GameObject startScreen;
    [SerializeField] private InputActionReference InputAction;
    bool inputStart = false;

    void Awake()
    {
        playerMovement.enabled = false;
        playerView.enabled = false;
        chestInteract.enabled = false;
    }
   private void Start()
    {
        InputAction.action.performed += HandleInputAction;
    }

    void HandleInputAction(InputAction.CallbackContext context)
    {
        inputStart = true;
        Debug.Log(inputStart);
    }
    // Update is called once per frame
    private void Update()
    {
        if(inputStart == true)
        {
            playerMovement.enabled = true;
            playerView.enabled = true;
            chestInteract.enabled = true;

            Destroy(startScreen);
        }
    }
}
