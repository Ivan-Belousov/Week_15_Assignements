using System.Collections.Generic; // ✅ Added this
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
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
    }
}

