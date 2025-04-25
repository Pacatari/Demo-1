using UnityEngine;
using TMPro;

public class ScoreByHeight : MonoBehaviour
{
    public float scoreMultiplier = 10f; // Multiplicador para calcular la puntuación
    private float initialHeight; // Altura inicial del objeto
    private float maxHeightReached; // Altura máxima alcanzada relativa a la inicial
    private int currentScore = 0; // Puntuación actual

    public TextMeshProUGUI scoreText; // Referencia al componente TextMeshProUGUI del UI

    void Start()
    {
        // Guardar la altura inicial del objeto
        initialHeight = transform.position.y;
        maxHeightReached = 0f; // Inicializar la altura máxima relativa en 0
        UpdateScore(); // Inicializar la puntuación en 0
    }

    void Update()
    {
        // Calcular la altura relativa al punto inicial
        float relativeHeight = transform.position.y - initialHeight;

        // Verificar si la altura relativa supera la altura máxima alcanzada
        if (relativeHeight > maxHeightReached)
        {
            maxHeightReached = relativeHeight; // Actualizar la altura máxima relativa
            UpdateScore(); // Actualizar la puntuación
        }
    }

    void UpdateScore()
    {
        // Calcular la puntuación actual basada en la altura máxima relativa alcanzada
        currentScore = Mathf.FloorToInt(maxHeightReached * scoreMultiplier);

        // Actualizar el texto del UI si el campo scoreText está asignado
        if (scoreText != null)
        {
            scoreText.text = "Puntuación: " + currentScore;
        }
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }
}