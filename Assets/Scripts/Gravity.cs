using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is Bjørn's new gravity, copied from Johns BallGravity.
/// This just points the gravity towards a planet instead of just down on the Y-axis.
/// </summary>
public class Gravity : MonoBehaviour
{

    public Planet planet;
    
    //gravity in meters per second per second
    public float GRAVITY_CONSTANT = -9.8f;                      // -- for earth,  -1.6 for moon 

    public Vector3 velocity = new Vector3(0, 0, 0);             //current direction and speed of movement
    public Vector3 acceleration = new Vector3(0, 0, 0);         //movement controlled by player movement force and gravity

    public Vector3 thrust = new Vector3(0, 0, 0);               //player applied thrust vector
    public Vector3 finalForce = new Vector3(0, 0, 0);           //final force to be applied this frame

    public float mass = 1.0f;

    public float height = 0;

    public Vector3 impulse = new Vector3(0, 0, 0);

    public float timeScalar = 1.0f;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        handleMovement();
    }

    void handleMovement()
    {

        //set the rate of integration, which is (almost) equivalent to
        //explosion by mass for impulse calc. problem is, gravity
        //is no longer a constant. but for gameplay, maybe not an issue?
        float forceDeltaTime = Time.deltaTime * timeScalar; 
        

        Vector3 direction = transform.position - planet.transform.position;
        float distanceSquared = direction.sqrMagnitude;

        if (distanceSquared > 0.001f)
        {
            direction.Normalize();
            Vector3 gravity = direction * (GRAVITY_CONSTANT * mass);
            finalForce = gravity;
        }
        else
        {
            finalForce = Vector3.zero;
        }
        
        finalForce += thrust;
        
        finalForce -= new Vector3(1f, 1f, 1f);
        
        acceleration = finalForce / mass;
        velocity += acceleration * forceDeltaTime;
        velocity += impulse;

        //move the object
        transform.position += velocity * forceDeltaTime;

        //reset impulse
        impulse *= 0;
    }

    public void reset()
    {
        velocity *= 0;
        acceleration *= 0;
        impulse *= 0;
        thrust *= 0;
       
    }

}
