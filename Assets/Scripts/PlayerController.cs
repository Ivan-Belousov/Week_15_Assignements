using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))] // This ensures Rigidbody2D is added to the GameObject
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f; // Movement speed
    private Vector2 moveInput;

    public bool IsMoving { get; private set; }

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // Initialization logic (if needed)
    }

    void Update()
    {
        // Frame-based updates (if needed)
    }

    private void FixedUpdate()
    {
        // Apply movement using Rigidbody2D
        rb.linearVelocity = new Vector2(moveInput.x * walkSpeed, rb.linearVelocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // Capture movement input
        moveInput = context.ReadValue<Vector2>();

        // Update movement state
        IsMoving = moveInput != Vector2.zero;
    }
}