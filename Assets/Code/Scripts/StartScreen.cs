using UnityEngine;
using UnityEngine.InputSystem;

public class ScreenStart : MonoBehaviour
{
    public static ScreenStart Instance;

    [Header("References")]
    public GameObject introPanel;          // Black panel at the start
    public PlayerMovement playerMovement;  // Control player movement
    public PlayerView playerView;          // Control animations
    public Animator playerAnimator;        // Control animations

    [Header("Input System")]
    [SerializeField] private InputActionReference startAction; // Enter to start the game

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void OnEnable() // Secure input suscription
    {
        if (startAction != null)
            startAction.action.performed += OnStartPressed;
    }

    private void OnDisable() // Secure input desuscription
    {
        if (startAction != null)
            startAction.action.performed -= OnStartPressed;
    }

    private void Start() // Settings to start the game
    {
        introPanel.SetActive(true);  // all panels off at start

        // Blocked inputs
        playerMovement.enabled = false;

        //  Animations and physics paused
        Time.timeScale = 0f;
        if (playerAnimator != null)
            playerAnimator.speed = 0f;
    }

    // Input action to start the game
    private void OnStartPressed(InputAction.CallbackContext context)
    {
        StartGame();
    }

    private void StartGame()
    {
        introPanel.SetActive(false);

        // inputs unblocked
        playerMovement.enabled = true;

        //  Animations and physics resumed
        Time.timeScale = 1f;
        if (playerAnimator != null)
            playerAnimator.speed = 1f;
        // disable the start action
        if (startAction != null)
            startAction.action.Disable();
    }
}
