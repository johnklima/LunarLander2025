using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public float maxDistance = 100f; // Maximum distance to obtain the minimum score.
    public float minDistance = 0f; // Minimum distance to obtain the maximum score.

    private static ScoreManager _instance;
    public static ScoreManager Instance
    {
        get
        {
            // If the instance is null, an attempt is made to find an existing one.
            if (_instance == null)
            {
                _instance = FindObjectOfType<ScoreManager>();

                // If not found, an exception is thrown
                if (_instance == null)
                {
                    throw new System.Exception("No instance of ScoreManager was found in the scene.");
                }
            }
            return _instance;
        }
    }

    // Función para calcular el puntaje basado en la distancia.
    public float CalculateScore(Vector3 winPoint, Vector3 landPoint)
    {
        // Calcula la distancia entre el punto de aterrizaje y el centro de la plataforma.
        float distance = Vector3.Distance(winPoint, landPoint);

        // Normaliza la distancia al rango de 0 (minDistance) a 1 (maxDistance).
        float normalizedDistance = Mathf.Clamp01((distance - minDistance) / (maxDistance - minDistance));

        // Convierte la distancia normalizada a un número de estrellas en intervalos de 0.5.
        float stars = Mathf.Lerp(3f, 0.5f, normalizedDistance);

        // Redondea el número de estrellas al múltiplo más cercano de 0.5.
        stars = Mathf.Round(stars * 2f) / 2f;

        return stars;
    }
}