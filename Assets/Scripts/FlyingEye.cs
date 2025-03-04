using System.Collections.Generic;
using UnityEngine;

public class FlyingEye : MonoBehaviour
{

    public DetectionZone biteDetectionZone;
    public float flightSpeed = 2f;
    public List<Transform> waypoints;
    public float WaypointReachedDistance = 0.1f; 
    private bool deathNotified = false;
    Animator animator;
    Rigidbody2D rb;
    Damageable damageable;
    Transform nextWaypoint;
    int waypointNum = 0;

    public bool _hasTarget = false;

    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

     public bool canMove
    {
        get { return animator.GetBool(AnimationStrings.canMove); }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        damageable = GetComponent<Damageable>();
    }

    private void Start()
    {
        nextWaypoint = waypoints[waypointNum];
    } 

    // Update is called once per frame
    void Update()
    {
        HasTarget = biteDetectionZone.detectedColliders.Count > 0;

        if(!damageable.IsAlive && !deathNotified)
        {
            deathNotified = true;
            GameManager gm = FindAnyObjectByType<GameManager>(FindObjectsInactive.Include);
            if (gm != null)
            {
                gm.EnemyDefeated();
            }
        }
    }

    private void FixedUpdate()
    {
        if(damageable.IsAlive)
        {
            if(canMove)
            {
                Flight();
                UpdateDirection();
            }
            else
            {
                rb.linearVelocity = Vector3.zero;
            }
        }
        else
        {
            rb.gravityScale = 2f;
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    } 

    private void Flight()
    {
        // Fly to the next waypoint
        Vector2 directionToWaypoint = (nextWaypoint.position - transform.position).normalized;
        float distance = Vector2.Distance(nextWaypoint.position, transform.position);
        rb.linearVelocity = directionToWaypoint * flightSpeed;

        if(distance <= WaypointReachedDistance)
        {
            waypointNum++;
            if(waypointNum >= waypoints.Count)
            {
                waypointNum = 0;
            }
            nextWaypoint = waypoints[waypointNum];
        }
    }

    private void UpdateDirection()
    {
        Vector3 locScale = transform.localScale;
        if(transform.localScale.x > 0)
        {
            if(rb.linearVelocity.x < 0)
            {
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }

        else
        {
            if(rb.linearVelocity.x > 0)
            {
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }
    }
}
