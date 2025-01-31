using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))] // Ensure Rigidbody2D is added to the GameObject
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f; // Walk speed
    public float runSpeed = 8f; // Run speed
    private Vector2 moveInput;

    public float CurrentMoveSpeed 
    {
        get
        {
            if (IsMoving)
            {
                return IsRunning ? runSpeed : walkSpeed;
            }
            return 0;
        }
    }

    [SerializeField] private bool _isMoving = false;
    public bool IsMoving 
    { 
        get => _isMoving;
        private set 
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        } 
    }

    [SerializeField] private bool _isRunning = false;
    private bool IsRunning
    {
        get => _isRunning;
        set
        {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }

    private bool _isFacingRight = true;
    public bool IsFacingRight 
    { 
        get => _isFacingRight;  // ✅ Fixed infinite recursion
        private set 
        {
            if (_isFacingRight != value)
            {
                _isFacingRight = value;
                Flip(); // ✅ Added a method for flipping instead of modifying scale directly
            }
        } 
    }

    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.linearVelocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;
        SetFacingDirection(moveInput);
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;  // Face Right
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false; // Face Left
        }
    }

    private void Flip() // ✅ Flips the character smoothly
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Flip only the X-axis
        transform.localScale = scale;
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;  // Start running
        } 
        else if (context.canceled)
        {
            IsRunning = false; // Stop running
        }
    }
}
