using UnityEngine;

/// <summary>
/// Camera controller
/// 
/// Summary of changes made on 2025-02-02:
/// - Removed Unity.Mathematics and UnityEngine.InputSystem namespaces.
/// - Added new public values for cursor locking, target settings, orbit settings, zoom settings, and collision settings.
/// - Introduced static instance management for the OrbitCamera class.
/// - Added private values for yaw, pitch, zoom, and smooth position.
/// - Added event handling for OnWin and OnLose events from LevelManager.
/// - Introduced new methods for handling input, updating zoom, position, rotation, and collision.
/// - Simplified and restructured the camera rotation and zoom logic.
/// </summary>
public class OrbitCamera : MonoBehaviour
{
    #region Public Values
    [Header("General")]
    public bool cursorLocked = true;

    [Header("Target Settings")]
    public Transform target;
    public Vector3 targetOffset = Vector3.zero;
    public float followSharpness = 10f;

    [Header("Orbit Settings")]
    public float orbitSpeed = 5f;
    public bool invertVertical = false;
    public float minVerticalAngle = -85f;
    public float maxVerticalAngle = 85f;

    [Header("Zoom Settings")]
    public float zoomSpeed = 5f;
    public float minZoom = 2f;
    public float maxZoom = 15f;
    public float zoomSmoothness = 0.3f;

    [Header("Collision")]
    public LayerMask collisionMask;
    public float collisionOffset = 0.3f;
    #endregion

    #region Static Values
    private static OrbitCamera _instance;
    public static OrbitCamera Instance
    {
        get
        {
            // If the instance is null, an attempt is made to find an existing one.
            if (_instance == null)
            {
                _instance = FindObjectOfType<OrbitCamera>();

                // If not found, an exception is thrown
                if (_instance == null)
                {
                    throw new System.Exception("No instance of TPCameraManager was found in the scene.");
                }
            }
            return _instance;
        }
    }
    #endregion

    #region Private Values
    private float currentYaw;
    private float currentPitch;
    private float currentZoom;
    private float desiredZoom;
    private float currentZoomVelocity;
    private Vector3 smoothPosition;
    private bool isEnable;
    #endregion

    #region Mono
    private void OnEnable()
    {
        isEnable = true;
        SetCursorState(true);

        LevelManager.Instance.OnWin += OnWindAndLoseHandler;
        LevelManager.Instance.OnLose += OnWindAndLoseHandler;
    }
    private void Start()
    {
        Vector3 initialDirection = target.position - transform.position;
        currentYaw = Vector3.SignedAngle(Vector3.forward, initialDirection, Vector3.up);
        currentPitch = Vector3.SignedAngle(Vector3.forward, initialDirection, transform.right);
        currentZoom = initialDirection.magnitude;
        desiredZoom = currentZoom;
        smoothPosition = target.position + targetOffset;
    }
    private void Update()
    {
        if (!isEnable) return;
        HandleInput();
        UpdateZoom();
    }
    private void LateUpdate()
    {
        if (!isEnable) return;
        if (target == null) return;

        UpdatePosition();
        HandleCollision();
        UpdateRotation();
    }
    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(cursorLocked);
    }
    #endregion

    #region Internal
    void HandleInput()
    {
        float scroll = InputsManager.Player.Zoom.ReadValue<Vector2>().y;
        float mouseX = InputsManager.Player.CameraRotation.ReadValue<Vector2>().x;
        float mouseY = InputsManager.Player.CameraRotation.ReadValue<Vector2>().y * (invertVertical ? 1 : -1);
        bool allowCameraRotation = InputsManager.Player.AllowCameraRotation.ReadValue<float>() == 1;

        if (allowCameraRotation)
        {
            currentYaw += mouseX * orbitSpeed;
            currentPitch = Mathf.Clamp(
                currentPitch + mouseY * orbitSpeed,
                minVerticalAngle,
                maxVerticalAngle
            );
        }

        if (scroll != 0)
        {
            desiredZoom = Mathf.Clamp(desiredZoom - scroll * zoomSpeed, minZoom, maxZoom);
        }
    }
    void UpdateZoom()
    {
        currentZoom = Mathf.SmoothDamp(currentZoom, desiredZoom, ref currentZoomVelocity, zoomSmoothness);
    }
    void UpdatePosition()
    {
        Vector3 targetPos = target.position + targetOffset;
        smoothPosition = Vector3.Lerp(smoothPosition, targetPos, followSharpness * Time.deltaTime);

        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0);
        Vector3 offset = rotation * Vector3.back * currentZoom;
        transform.position = smoothPosition + offset;
    }
    void UpdateRotation()
    {
        transform.rotation = Quaternion.Euler(currentPitch, currentYaw, 0);
    }
    void HandleCollision()
    {
        RaycastHit hit;
        Vector3 direction = (transform.position - smoothPosition).normalized;
        float checkDistance = Vector3.Distance(smoothPosition, transform.position);

        if (Physics.Raycast(smoothPosition, direction, out hit, checkDistance, collisionMask))
        {
            currentZoom = Mathf.Clamp(hit.distance - collisionOffset, minZoom, maxZoom);
            desiredZoom = currentZoom;
        }
    }
    #endregion

    #region Public Methods
    public void AllowCameraManagment(bool value)
    {
        isEnable = value;
    }
    public void SetCursorState(bool newState)
    {
        Cursor.visible = !newState;
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
    #endregion

    #region Callbacks
    private void OnWindAndLoseHandler()
    {
        AllowCameraManagment(false);
        SetCursorState(false);
    }
    #endregion
}