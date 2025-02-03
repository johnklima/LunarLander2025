using Interfaces;
using UnityEngine;

/// <summary>
/// Atmosphere of the planet
/// Controls Drag
/// </summary>
public class Atmosphere : MonoBehaviour
{
    [SerializeField, Range(0f, 0.5f)] private float drag = 0.01f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IPhysicsObject physicsObject))
        {
            physicsObject.Drag = drag;
            physicsObject.Planet = GetComponent<Planet>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IPhysicsObject physicsObject))
        {
            physicsObject.Drag = 0f;
            physicsObject.Planet = null;
        }
    }
}
