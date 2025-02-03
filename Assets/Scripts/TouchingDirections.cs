using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
    [Header("Ground Check Settings")]
    public ContactFilter2D castFilter;
    public float groundDistance = 0.05f;

    private Rigidbody2D rb;
    private CapsuleCollider2D touchingCol;
    private Animator animator;

    // An array to store results of the Cast method.
    RaycastHit2D[] groundHits = new RaycastHit2D[5];

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
    }
}
