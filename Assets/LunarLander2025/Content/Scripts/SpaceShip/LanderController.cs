using UnityEngine.InputSystem;
using UnityEngine;

/// <summary>
/// In charge of Ship rotation.
/// 
/// Summary of changes made on 2025-02-02:
/// - Removed SpaceShipInput and related input handling logic.
/// - Added public property PlayerState with a default value of PlayerState.Flying.
/// - Introduced private Thrusters reference and associated checks for thruster state.
/// - Updated Awake method to initialize thrusters reference.
/// - Added checks for thruster state in Update and LateUpdate methods to conditionally apply rotation.
/// - Modified OnEnable and OnDisable methods to use InputsManager for input handling.
/// - Removed unused ShipRotationCanceled method.
/// </summary>
public class LanderController : MonoBehaviour
{
    #region Public Fields
    public float yaw;
    public float pitch;
    public float roll;
    public float rotationTorque = 10;
    #endregion

    #region Properties
    public PlayerState PlayerState { get; set; } = PlayerState.Flying;
    #endregion

    #region Private Values
    // References
    private Thrusters thrusters;

    // Values
    private Vector3 curretRot;
    #endregion

    #region Mono
    private void Awake()
    {
        thrusters = GetComponent<Thrusters>();
    }
    public void Update()
    {
        if (thrusters.ThrusterState != ThrusterState.On) return;

        // adding to the rotation each frame
        curretRot.x += pitch * Time.deltaTime;
        curretRot.y += roll * Time.deltaTime;
        curretRot.z += yaw * Time.deltaTime;

        // Rotates the transform 
        transform.Rotate(curretRot.x, curretRot.y, curretRot.z, Space.Self);
    }
    public void LateUpdate()
    {
        if (thrusters.ThrusterState != ThrusterState.On) return;
        curretRot = Vector3.Lerp(curretRot, Vector3.zero, 0.003f);
    }
    private void OnEnable()
    {
        InputsManager.Player.ShipRotation.performed += ShipRotationStarted;
    }
    private void OnDisable()
    {
        InputsManager.Player.ShipRotation.performed -= ShipRotationStarted;
    }
    #endregion

    #region Internal
    private void ShipRotationStarted(InputAction.CallbackContext obj)
    {
        if (thrusters.ThrusterState != ThrusterState.On) return;

        // changes the rotation based on the input vector2 value. 
        yaw = obj.ReadValue<Vector2>().x * rotationTorque;
        pitch = obj.ReadValue<Vector2>().y * rotationTorque;
    }
    #endregion
}
