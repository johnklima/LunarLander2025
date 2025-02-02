using UnityEngine;

//TheWalkingButterfly
public class OrbitCamera : MonoBehaviour
{
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

    //small update
    private static OrbitCamera _instance;
    public static OrbitCamera Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<OrbitCamera>();

                if (_instance == null)
                {
                    throw new System.Exception("No instance of TPCameraManager was found in the scene.");
                }
            }
            return _instance;
        }
    }

    private float currentYaw;
    private float currentPitch;
    private float currentZoom;
    private float desiredZoom;
    private Vector3 smoothPosition;
    private float currentZoomVelocity;

    void Start()
    {
        Vector3 initialDirection = transform.position - target.position;
        currentYaw = Vector3.SignedAngle(Vector3.forward, initialDirection, Vector3.up);
        currentPitch = Vector3.SignedAngle(Vector3.forward, initialDirection, transform.right);
        currentZoom = initialDirection.magnitude;
        desiredZoom = currentZoom;
        smoothPosition = target.position + targetOffset;
    }

    void Update()
    {
        HandleInput();
        UpdateZoom();
    }

    void LateUpdate()
    {
        if (target == null) return;

        UpdatePosition();
        HandleCollision();
        UpdateRotation();
    }

    void HandleInput()
    {
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y") * (invertVertical ? 1 : -1);

            currentYaw += mouseX * orbitSpeed;
            currentPitch = Mathf.Clamp(
                currentPitch + mouseY * orbitSpeed,
                minVerticalAngle,
                maxVerticalAngle
            );
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
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
}