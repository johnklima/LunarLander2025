using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LateralThrustResolver : MonoBehaviour
{

    public Transform target;
    public Transform landerRotator;

    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion targetRot;

        transform.LookAt(target);

        targetRot = transform.rotation;


        //if (Time.time - timer > 10000) 
        {
            landerRotator.rotation = Quaternion.Slerp(landerRotator.rotation, targetRot, Time.deltaTime);
            timer = Time.time;
        }
        

}
}
