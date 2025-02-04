using UnityEngine;
using Interfaces;

/// <summary>
/// Ship movement
/// 
/// This is Bjørn's new gravity, copied from Johns BallGravity.
/// This just points the gravity towards a planet instead of just down on the Y-axis.
/// 
/// Summary of changes made on 2025-02-02:
/// - Added public fields for explosionPrefab and explosionRef for handling explosion effects.
/// - Changed initialization of Vector3 fields to use the new keyword with default values.
/// - Introduced properties for Drag and current Planet.
/// - Added private field for LanderController reference.
/// - Implemented Awake method to initialize LanderController reference.
/// - Updated HandleMovement method to check PlayerState before applying physics calculations.
/// - Added OnTriggerEnter method to handle collisions with landing platforms and planets, invoking HandleLose method.
/// - Implemented HandleLose method to manage game state and instantiate explosion effects.
/// - Added InstantiateExplosionEffect method to play explosion sound and instantiate explosion prefab.
/// - Renamed reset method to ResetValues and moved it to the Public Methods region.
/// </summary>
public class Gravity : MonoBehaviour, IPhysicsObject
{
    #region Public Values
    public GameObject explosionPrefab;
    public Transform explosionRef;
    [Space(10)]

    public float GRAVITY_CONSTANT = -9.8f;               // -- for earth,  -1.6 for moon 
    public Vector3 velocity = new(0, 0, 0);             //current direction and speed of movement
    public Vector3 acceleration = new(0, 0, 0);         //movement controlled by player movement force and gravity
    public Vector3 thrust = new(0, 0, 0);               //player applied thrust vector
    public Vector3 finalForce = new(0, 0, 0);           //final force to be applied this frame
    public Vector3 impulse = new(0, 0, 0);
    public float mass = 1.0f;
    public float height = 0;
    public float timeScalar = 1.0f;

    public FMODUnity.StudioEventEmitter emitMediumExplosion;
    public FMODUnity.StudioEventEmitter emitThrusters;

    #endregion

    #region Properties
    public float Drag { get; set; } = 0.01f;              //Drag for air resistance and such.
    public Planet Planet { get; set; }                    //Current planet
    #endregion

    #region Private Values
    // References
    private LanderController landerController;
    private Thrusters thrusters;

    // Values 
    private const float BrakingForce = 3f;               // Braking constant that you can adjust as needed
    #endregion

    #region Mono
    private void Awake()
    {
        landerController = GetComponent<LanderController>();
        thrusters = GetComponent<Thrusters>();
    }
    void Update()
    {
        HandleMovement();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Landing Platform") || other.gameObject.CompareTag("Planet"))
        {
            HandleLose();
        }
    }
    #endregion

    #region Internal
    void HandleMovement()
    {
        if (landerController.PlayerState != PlayerState.Flying) return;

        float deltaTime = Time.deltaTime * timeScalar;

        Vector3 direction = Planet == null ? -transform.up : transform.position - Planet.transform.position;
        Vector3 gravityAcceleration = direction.normalized * GRAVITY_CONSTANT;

        if (thrusters.ThrusterState == ThrusterState.On)
        {
            // Full throttle when the thruster is on
            acceleration = gravityAcceleration + thrust / mass;

            // Reset thrust after application
            thrust = Vector3.zero;
        }
        else
        {
            // If the propeller is off, apply braking force
            acceleration = gravityAcceleration - velocity * Drag - velocity.normalized * BrakingForce;
        }

        // Update speed and position
        velocity += acceleration * deltaTime;
        transform.position += velocity * deltaTime;
    }
    void HandleLose()
    {
        Debug.Log("Destroy");
        InstantiateExplosionEffect();
        transform.root.gameObject.SetActive(false);
        OrbitCamera.Instance.AllowCameraManagment(false);
        OrbitCamera.Instance.SetCursorState(false);
        LevelManager.Instance.SetGameAsLose();
    }
    private void InstantiateExplosionEffect()
    {
        //<JK> just fix it
        emitThrusters.Stop();
        emitMediumExplosion.Play();
        Instantiate(explosionPrefab, explosionRef.position, Quaternion.identity);
    }
    #endregion

    #region Public Methods
    public void ResetValues()
    {
        velocity *= 0;
        acceleration *= 0;
        impulse *= 0;
        thrust *= 0;
    }
    #endregion
}
