using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]

public class Knight : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float walkStopRate = 0.05f;
    public DetectionZone attackZone;
    public DetectionZone cliffDetectionZone;
    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    Animator animator;
    Damageable damageable;

    private bool deathNotified = false;

    public enum WalkableDirection { Right, Left }
    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right; // Default direction

    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection != value)
            {
                // Flip direction
                gameObject.transform.localScale = new Vector2(
                    gameObject.transform.localScale.x * -1,
                    gameObject.transform.localScale.y
                );

                walkDirectionVector = (value == WalkableDirection.Right) ? Vector2.right : Vector2.left;
            }
            _walkDirection = value;
        }
    }

    public bool _hasTarget = false;

    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);  // ✅ Correct reference
        }
    }

    public bool canMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);  // ✅ Fixed typo here
        }
    }

    public float AttackCooldown
     { 
        get
        {
            return animator.GetFloat(AnimationStrings.attackCooldown);
        } 
        private set
        {
            animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value, 0));
        }
     }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    void Update()
    {
        // Detect whether there's a target in the attack zone
        HasTarget = attackZone.detectedColliders.Count > 0;

        // Reduce attack cooldown if it's active
        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }

        // --- NEW: Check if Knight has died ---
        if (!damageable.IsAlive && !deathNotified)
        {
            deathNotified = true; // Avoid multiple notifications

            // Find the GameManager and notify it
            GameManager gm = FindAnyObjectByType<GameManager>(FindObjectsInactive.Include);
            if (gm != null)
            {
                gm.EnemyDefeated();
            }
        }
    }

    private void FixedUpdate()
    {
        if (touchingDirections.IsGrounded && touchingDirections.IsOnWall)
        {
            FlipDirection();
        }
        if (canMove)
            rb.linearVelocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(Mathf.Lerp(rb.linearVelocity.x, 0, walkStopRate), rb.linearVelocity.y);
    }

    private void FlipDirection()
    {
        WalkDirection = (WalkDirection == WalkableDirection.Right) ? WalkableDirection.Left : WalkableDirection.Right;
    }

    void Start()
    {
        // Ensure it starts moving in the correct direction
        if (_walkDirection == WalkableDirection.Right)
        {
            walkDirectionVector = Vector2.right;
        }
        else if (_walkDirection == WalkableDirection.Left)
        {
            walkDirectionVector = Vector2.left;
        }
        else
        {
            Debug.LogError("Invalid WalkDirection value!");
        }
    }

    public void OnHit(int damege, Vector2 knockback)
    {
        rb.linearVelocity = new Vector2(knockback.x, rb.linearVelocity.y + knockback.y);
    }

    public void OnCliffDetected()
    {
        if(touchingDirections.IsGrounded)
        {
            FlipDirection();
        }
    }
}

