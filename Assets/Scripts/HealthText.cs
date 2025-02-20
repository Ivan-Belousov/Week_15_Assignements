using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    public Vector3 moveSpeed = new Vector3(0, 75, 0);
    public float timeToFade = 1f;

    private float timeElapsed = 0f;
    private Color startColor;
    private RectTransform textTransform; // Declare at class level
    TextMeshProUGUI textMeshPro; // Use TextMeshProUGUI for UI text

    private void Awake()
    {
        textTransform = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI>(); // Get the correct component
        startColor = textMeshPro.color;
    }

    private void Update() // Correctly named Update method
    {
        textTransform.position += moveSpeed * Time.deltaTime;

        timeElapsed += Time.deltaTime;

        if(timeElapsed < timeToFade)
        {
            float fadeAlpha = startColor.a * (1 - (timeElapsed / timeToFade));
            textMeshPro.color = new Color(startColor.r, startColor.g, startColor.b, fadeAlpha);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

