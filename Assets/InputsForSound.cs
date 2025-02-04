using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputsForSound : MonoBehaviour
{

    public FMODUnity.StudioEventEmitter emitEngineStart;
    public FMODUnity.StudioEventEmitter emitMainTruster;
    public FMODUnity.StudioEventEmitter emitSideTruster;

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
            emitMainTruster.Play();
            intensity += Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.Space))
        {
            //while held
            emitMainTruster.SetParameter("Thruster Intensity", intensity);
            intensity += Time.deltaTime;
            if (intensity > 1.0f)
                intensity = 1.0f; 
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            emitMainTruster.Stop();
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
            emitSideTruster.Play();
        }


        if (sideInputOff)
        {
            emitSideTruster.Stop();
        }

    }
}
