using UnityEngine;

//thewalkingbutterfly
public class OrbitSimulation : MonoBehaviour
{
    // Sun position (center of the orbit)
    private Vector3 sunPosition = new Vector3(0, 0, -5);

    // Initial positions (set in the Inspector)
    public Vector3 earthInitialPosition = new Vector3(0, 30, -500);
    public Vector3 moonInitialPositionRelativeToEarth = new Vector3(0, 0, -50);
    public Vector3 marsInitialPosition = new Vector3(0, 30, -800);

    // Orbital parameters for Earth
    public float earthOrbitRadius = 500f; // Distance from the Sun
    public float earthOrbitSpeed = 10f; // Speed of the orbit
    public float earthEllipseRatio = 0.8f; // Controls the "flatness" of the ellipse (1 = circle)

    // Orbital parameters for Moon
    public float moonOrbitRadius = 50f; // Distance from the Earth
    public float moonOrbitSpeed = 20f; // Speed of the orbit
    public float moonEllipseRatio = 1f; // Controls the "flatness" of the ellipse (1 = circle)

    // Orbital parameters for Mars
    public float marsOrbitRadius = 800f; // Distance from the Sun
    public float marsOrbitSpeed = 7f; // Speed of the orbit
    public float marsEllipseRatio = 0.9f; // Controls the "flatness" of the ellipse (1 = circle)

    // Current angles in the orbits (in radians)
    private float earthCurrentAngle = 0f;
    private float moonCurrentAngle = 0f;
    private float marsCurrentAngle = 0f;

    // References to Earth, Moon, and Mars GameObjects
    public Transform earthTransform;
    public Transform moonTransform;
    public Transform marsTransform;

    void Start()
    {
        // Set the Earth's initial position
        earthTransform.position = earthInitialPosition;

        // Calculate the Earth's initial angle based on its starting position
        Vector3 earthOffset = earthInitialPosition - sunPosition;
        earthCurrentAngle = Mathf.Atan2(earthOffset.z, earthOffset.x);

        // Set the Moon's initial position (relative to Earth)
        moonTransform.position = earthInitialPosition + moonInitialPositionRelativeToEarth;

        // Calculate the Moon's initial angle based on its starting position
        Vector3 moonOffset = moonTransform.position - earthTransform.position;
        moonCurrentAngle = Mathf.Atan2(moonOffset.z, moonOffset.x);

        // Set Mars's initial position
        marsTransform.position = marsInitialPosition;

        // Calculate Mars's initial angle based on its starting position
        Vector3 marsOffset = marsInitialPosition - sunPosition;
        marsCurrentAngle = Mathf.Atan2(marsOffset.z, marsOffset.x);
    }

    void Update()
    {
        // Update the Earth's orbit around the Sun
        SimulateEarthOrbit();

        // Update the Moon's orbit around the Earth
        SimulateMoonOrbit();

        // Update Mars's orbit around the Sun
        SimulateMarsOrbit();
    }

    void SimulateEarthOrbit()
    {
        // Increment the Earth's angle based on time and speed
        earthCurrentAngle += earthOrbitSpeed * Time.deltaTime * Mathf.Deg2Rad;

        // Calculate the Earth's new position in an elliptical orbit
        float earthX = Mathf.Cos(earthCurrentAngle) * earthOrbitRadius * earthEllipseRatio;
        float earthZ = Mathf.Sin(earthCurrentAngle) * earthOrbitRadius;

        // Update the Earth's position
        earthTransform.position = sunPosition + new Vector3(earthX, 0, earthZ);
    }

    void SimulateMoonOrbit()
    {
        // Increment the Moon's angle based on time and speed
        moonCurrentAngle += moonOrbitSpeed * Time.deltaTime * Mathf.Deg2Rad;

        // Calculate the Moon's new position relative to the Earth in an elliptical orbit
        float moonX = Mathf.Cos(moonCurrentAngle) * moonOrbitRadius * moonEllipseRatio;
        float moonZ = Mathf.Sin(moonCurrentAngle) * moonOrbitRadius;

        // Update the Moon's position (relative to the Earth)
        moonTransform.position = earthTransform.position + new Vector3(moonX, 0, moonZ);
    }

    void SimulateMarsOrbit()
    {
        // Increment Mars's angle based on time and speed
        marsCurrentAngle += marsOrbitSpeed * Time.deltaTime * Mathf.Deg2Rad;

        // Calculate Mars's new position in an elliptical orbit
        float marsX = Mathf.Cos(marsCurrentAngle) * marsOrbitRadius * marsEllipseRatio;
        float marsZ = Mathf.Sin(marsCurrentAngle) * marsOrbitRadius;

        // Update Mars's position
        marsTransform.position = sunPosition + new Vector3(marsX, 0, marsZ);
    }
}