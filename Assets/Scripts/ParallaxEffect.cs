using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;

    // Starting positions for the parallax game object
    Vector2 startingPostition;
    // Start Z value of the parallax game object
    float startingZ;

    // Camera movement since the start of the scene
    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startingPostition;

    // Z distance from the target object
    float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;

    // Clipping place based on the camera's clipping planes
    float clippingPlace => (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));

    // Parallax factor determines how much the background moves relative to the camera
    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlace;

    void Start()
    {
        // Set starting position and Z value
        startingPostition = transform.position;
        startingZ = transform.position.z;
    }

    void Update()
    {
        // Calculate the new position with the parallax effect
        Vector2 newPosition = startingPostition + camMoveSinceStart * parallaxFactor;

        // Update the position of the parallax object
        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}


