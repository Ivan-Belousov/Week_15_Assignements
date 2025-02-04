using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
    [Header("Ground Check Settings")]
    public ContactFilter2D castFilter;
    public float groundDistance = 0.05f;
    public float wallDistance = 0.2f;
    public float ceilingDistance = 0.05f;

    private Rigidbody2D rb;
    private CapsuleCollider2D touchingCol;
    private Animator animator;

    // An array to store results of the Cast method.
    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];

    [SerializeField]
    private bool _isGrounded;

    // Property to get/set grounded state. When set, it also updates the animator.
    public bool IsGrounded
    {
        get
        {
            return _isGrounded;
        }
        private set
        {
            _isGrounded = value;
            // Update the animator parameter. Replace "isGrounded" with your actual parameter name
            // or use AnimationStrings.isGrounded if you have that static class.
            animator.SetBool(AnimationStrings.isGrounded, value);
        }
    }


    [SerializeField]
    private bool _isOnWall;

    // Property to get/set grounded state. When set, it also updates the animator.
    public bool IsOnWall
    {
        get
        {
            return _isOnWall;
        }
        private set
        {
            _isOnWall = value;
            // Update the animator parameter. Replace "isOnWall" with your actual parameter name
            // or use AnimationStrings.isOnWall if you have that static class.
            animator.SetBool(AnimationStrings.isOnWall, value);
        }
    }

    [SerializeField]
    private bool _isOnCeiling;
    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;


    // Property to get/set grounded state. When set, it also updates the animator.
    public bool IsOnCeiling
    {
        get
        {
            return _isOnCeiling;
        }
        private set
        {
            _isOnCeiling = value;
            // Update the animator parameter. Replace "isOnCeiling" with your actual parameter name
            // or use AnimationStrings.isOnCeiling if you have that static class.
            animator.SetBool(AnimationStrings.isOnCeiling, value);
        }
    }

    // Merge Awake methods into one.
    private void Awake()
    {
        touchingCol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Use Unity's FixedUpdate (note the correct name) to perform the ground check.
    private void FixedUpdate()
    {
        // Cast downward from the collider. If the cast returns any hit, we are grounded.
        IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        IsOnWall = touchingCol.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
        IsOnCeiling = touchingCol.Cast(Vector2.up, castFilter, ceilingHits, ceilingDistance) > 0;
    }
}
