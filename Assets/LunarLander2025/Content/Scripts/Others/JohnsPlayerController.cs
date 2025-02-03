using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnsPlayerController : MonoBehaviour
{

    public BallGravitySphere gravity;
    public Transform lateralTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //TODO: consume fuel, on/off thruster particles

        //empty thrust (zero thrust)
        gravity.thrust = Vector3.zero;

        if(Input.GetKey(KeyCode.Space))
        {
            gravity.thrust = new Vector3(0, 0, 5.0f);
        }

        float horz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        Vector3 pos = Vector3.zero; 

        if( Mathf.Abs(horz) > 0.0f )
        {
            pos = pos + Vector3.right * horz * 5;

        }
        if (Mathf.Abs(vert) > 0.0f)
        {
            pos = pos + Vector3.forward * vert * 5;

        }

        lateralTarget.localPosition = pos;

}
}
