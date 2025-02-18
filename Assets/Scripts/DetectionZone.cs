using System.Collections.Generic; // ✅ Added this
using UnityEngine;
using UnityEngine.Events;

public class DetectionZone : MonoBehaviour
{
    public UnityEvent noCollidersRemain;
    public List<Collider2D> detectedColliders = new List<Collider2D>(); // ✅ Fixed variable name
    private Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>(); // Optional: Assign the collider if needed
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        detectedColliders.Add(collision); // ✅ Fixed typo (was detectColliders)
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        detectedColliders.Remove(collision); // ✅ Fixed typo (was detectColliders)
        if(detectedColliders.Count <= 0)
        {
            noCollidersRemain.Invoke();
        }
    }
}

