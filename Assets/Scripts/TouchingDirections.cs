using UnityEngine;

public class TouchingDirections : MonoBehaviour
{

    RigidBody2D rb;

    private void Awake()
    {
        rb = GetComponent<RigidBody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


    private void FixedUpdated()
    {

    }
}
