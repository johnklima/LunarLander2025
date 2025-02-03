using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoRotation : MonoBehaviour
{
    //Hook lander's rotation (real coder would know better how to do it)
    public Quaternion rotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Update rotation to same as lander
        this.transform.rotation = rotation;
    }
}
