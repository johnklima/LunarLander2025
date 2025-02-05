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

    // Function to calculate the score based on distance.
    public float CalculateScore(Vector3 winPoint, Vector3 landPoint)
    {
        // Calculate the distance between the landing point and the center of the platform.
        float distance = Vector3.Distance(winPoint, landPoint);

        // If distance is 0, return 3 stars directly.
        if (distance == 0)
        {
            return 3f;
        }

        // Normalizes the distance to the range from 0 (minDistance) to 1 (maxDistance).
        float normalizedDistance = Mathf.Clamp01((distance - minDistance) / (maxDistance - minDistance));

        // Convert the normalized distance to a number of stars in intervals of 0.5.
        float stars = Mathf.Lerp(2.5f, 0.5f, normalizedDistance);

        // Round the number of stars to the nearest multiple of 0.5.
        stars = Mathf.Round(stars * 2f) / 2f;

        return stars;
    }
}