using UnityEngine;
//thewalkingbutterfly
public class ForceKinematic : MonoBehaviour
{
    void Start()
    {
        Rigidbody[] rigidbodies = FindObjectsOfType<Rigidbody>();

        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = true;
        }

        Debug.Log("All Rigidbodies set to Kinematic at runtime.");
    }
}
