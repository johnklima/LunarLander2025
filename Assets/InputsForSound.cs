using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputsForSound : MonoBehaviour
{

    public FMODUnity.StudioEventEmitter emitEngineStart;
    public FMODUnity.StudioEventEmitter emitMainThruster;
    public FMODUnity.StudioEventEmitter emitSideThruster;

    // Start is called before the first frame update
    void Start()
    {
        emitEngineStart.Play();
    }

    float intensity;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space ))
        {
            emitMainThruster.Play();
            intensity += Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.Space))
        {
            //while held
            emitMainThruster.SetParameter("Thruster Intensity", intensity);
            intensity += Time.deltaTime;
            if (intensity > 1.0f)
                intensity = 1.0f; 
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            emitMainThruster.Stop();
            intensity = 0;
        }

        bool sideInputOn = Input.GetKeyDown(KeyCode.W) ||
                           Input.GetKeyDown(KeyCode.A) || 
                           Input.GetKeyDown(KeyCode.S) || 
                           Input.GetKeyDown(KeyCode.D);

        bool sideInputOff = Input.GetKeyUp(KeyCode.W) ||
                            Input.GetKeyUp(KeyCode.A) ||
                            Input.GetKeyUp(KeyCode.S) ||
                            Input.GetKeyUp(KeyCode.D);

        if (sideInputOn)
        {
            emitSideThruster.Play();
        }


        if (sideInputOff)
        {
            emitSideThruster.Stop();
        }

    }
}
