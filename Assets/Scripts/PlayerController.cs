using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))] // Ensure Rigidbody2D is added to the GameObject
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f; // Walk speed
    public float runSpeed = 8f; // Run speed
    public float airWalkSpeed = 3f; // Movement Speed while in Air

    public float jumpImpulse = 10f; // Jump Impulse
    private Vector2 moveInput;
    private TouchingDirections touchingDirections;
    Damageable damageable;

    public float CurrentMoveSpeed 
    {
        get
        {
            if (CanMove)  // Check if the player can move
            {
                if (IsMoving && !touchingDirections.IsOnWall)
                {
                    if (touchingDirections.IsGrounded)
                    {
                        return IsRunning ? runSpeed : walkSpeed;
                    }
                    else
                    {
                        return airWalkSpeed;
                    }
                }
                else
                {
                    return 0;
                }
            } 
            else
            {
                return 0;
            }
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
        get => _isFacingRight; 
        private set 
        {
            if (_isFacingRight != value)
            {
                _isFacingRight = value;
                Flip();
            }
        } 
    }

    // Fixed the CanMove property here by adding missing closing braces
    public bool CanMove 
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public bool IsAlive 
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }

    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        damageable = GetComponent<Damageable>();
    }

    private void FixedUpdate()
    {
        if(!damageable.IsHit)
            rb.linearVelocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.linearVelocity.y);
        animator.SetFloat(AnimationStrings.yVelocity, rb.linearVelocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if(IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;
            SetFacingDirection(moveInput);
        } 
        else
        {
            IsMoving = false;
        }
        
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

    private void Flip()
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

    public void OnJump(InputAction.CallbackContext context)
    {
        // ToDo: Check if Alive as well
        if (context.started && touchingDirections.IsGrounded && CanMove)
        {
            animator.SetTrigger(AnimationStrings.jumpTrigger); // ✅ Updated jump trigger
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpImpulse);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.attackTrigger); // ✅ Updated attack trigger
        }
    }

    public void OnRangedAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.rangedAttackTrigger); // ✅ Updated attack trigger
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.linearVelocity = new Vector2(knockback.x, rb.linearVelocity.y + knockback.y);
    }
}
