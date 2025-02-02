using UnityEngine;
using System;
using System.Collections;

public class LanderSmoother : MonoBehaviour
{
    #region Public Values
    [Header("References")]
    public GameObject groundSmokePrefab;
    public GameObject explosionPrefab;
    public Transform explosionRef;

    [Header("Collision Check")]
    public LayerMask collisionLayers;
    public Transform centerPoint;
    public Vector3 size;
    public Color boxColor = Color.red;

    [Header("Aligment Values")]
    public float alignmentSpeed = 0.1f;
    #endregion

    #region Actions
    private Action OnLandedSuccess;
    #endregion

    #region Private Values
    // References
    private LanderController landerController;
    private Thrusters thrusters;
    private Gravity gravity;

    // Values
    private Transform targetCenter;
    private bool isAligning = false;
    private bool collisionHandled = false;
    private Vector3 landingPoint;
    private ParticleSystem internalSmoke;
    private ParticleSystem externalSmoke;
    private readonly float disapearDuration = 0.4f;
    #endregion

    #region Mono
    private void Awake()
    {
        landerController = transform.parent.GetComponent<LanderController>();
        thrusters = transform.parent.GetComponent<Thrusters>();
        gravity = transform.parent.GetComponent<Gravity>();
    }
    private void OnEnable()
    {
        OnLandedSuccess += OnLandSuccess;
    }
    private void OnDisable()
    {
        OnLandedSuccess -= OnLandSuccess;
    }
    void FixedUpdate()
    {
        if (!collisionHandled) HandleColliderCollisions();
        if (isAligning) AlignWithTarget();
    }
    #endregion

    #region Internal
    private void HandleColliderCollisions()
    {
        Collider[] colliders = Physics.OverlapBox(centerPoint.position, size / 2, transform.rotation, collisionLayers, QueryTriggerInteraction.Ignore);

        foreach (Collider collider in colliders)
        {
            GameObject hitedObject = collider.gameObject;

            if (hitedObject.CompareTag("Landing Platform"))
            {
                if (targetCenter == null)
                {
                    targetCenter = hitedObject.transform.GetChild(0);

                    // Assign the point where you land
                    landingPoint = targetCenter.position;

                    InstantiateGoundSmokeVFX();

                    landerController.PlayerState = PlayerState.Grounded;
                    gravity.ResetValues();
                    isAligning = true;
                    collisionHandled = true; // Marking that a valid collision has been handled
                }
            }
            else if (hitedObject.CompareTag("Planet"))
            {
                if (targetCenter == null)
                {
                    bool hasPointGenerator = hitedObject.TryGetComponent<PointsGenerator>(out var pointsGenerator);

                    if (hasPointGenerator)
                    {
                        targetCenter = GenerateNewTarget();
                        Vector3 closestPoint = GetClosestPoint(pointsGenerator, transform.position);
                        targetCenter.position = closestPoint;

                        // Verify that the pointsGenerator is not null before using it.
                        if (pointsGenerator != null)
                        {
                            // Assign the point where you land
                            landingPoint = closestPoint;

                            InstantiateGoundSmokeVFX();

                            // Obtain the normal at the nearest point
                            Vector3 normal = pointsGenerator.GetNormalAtPoint(closestPoint);

                            // Orient the rotation of the targetCenter to match the normal rotation.
                            targetCenter.rotation = Quaternion.FromToRotation(Vector3.up, normal);

                            landerController.PlayerState = PlayerState.Grounded;
                            gravity.ResetValues();
                            isAligning = true;
                            collisionHandled = true; // Marking that a valid collision has been handled
                        }
                        else
                        {
                            Debug.LogError("PointsGenerator is null");
                        }
                    }
                }
            }
            else if (hitedObject.CompareTag("Obstacle"))
            {
                Debug.Log("Destroy");
                InstantiateExplosionEffect();
                transform.root.gameObject.SetActive(false);
                OrbitCamera.Instance.AllowCameraManagment(false);
                OrbitCamera.Instance.SetCursorState(false);
                LevelManager.Instance.SetGameAsLose();
            }
        }
    }
    private void AlignWithTarget()
    {
        if (targetCenter == null) return;

        // Smoothing position and rotation to targetCenter
        transform.parent.SetPositionAndRotation(
            Vector3.Lerp(transform.parent.position, targetCenter.position, alignmentSpeed * Time.deltaTime), 
            Quaternion.Lerp(transform.parent.rotation, targetCenter.rotation, alignmentSpeed * Time.deltaTime));

        // Check whether the alignment has been completed
        if (Vector3.Distance(transform.parent.position, targetCenter.position) < 0.01f &&
            Quaternion.Angle(transform.parent.rotation, targetCenter.rotation) < 0.1f)
        {
            isAligning = false;
            thrusters.ThrusterState = ThrusterState.Off;
            OnLandedSuccess?.Invoke();

            var winPoint = LevelInitializer.Instance.WinPoint;
            var finalScore = ScoreManager.Instance.CalculateScore(winPoint, landingPoint);

            WinAndLose.Instance.FinalScore = finalScore;
            LevelManager.Instance.SetGameAsWin();
        }
    }
    private Transform GenerateNewTarget()
    {
        GameObject newObject = Instantiate(new GameObject("Landing Target"));
        return newObject.transform;
    }
    private Vector3 GetClosestPoint(PointsGenerator pointsGenerator, Vector3 targetPosition)
    {
        Vector3 closestPoint = targetPosition;
        float minDistance = Mathf.Infinity;

        foreach (Vector3 point in pointsGenerator.SurfacePoints)
        {
            float distance = Vector3.Distance(targetPosition, point);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestPoint = point;
            }
        }

        return closestPoint;
    }
    private void InstantiateGoundSmokeVFX()
    {
        var newVFX = Instantiate(groundSmokePrefab, landingPoint, Quaternion.identity);
        internalSmoke = newVFX.GetComponent<ParticleSystem>();
        externalSmoke = newVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
        newVFX.transform.up = transform.up;

        FMODManager.Instance.StopConstant2DSound("SFX/ShipEngine");
        FMODManager.Instance.PlayConstant2DSound("SFX/LandingThrust");
    }
    private void InstantiateExplosionEffect()
    {
        FMODManager.Instance.PlayOneShot2DSound("SFX/Explosion");
        Instantiate(explosionPrefab, explosionRef.position, Quaternion.identity);
    }
    #endregion

    #region Callbacks
    private void OnLandSuccess() => StartCoroutine(DisapearSmoke());
    #endregion

    #region Coroutines
    private IEnumerator DisapearSmoke()
    {
        var time = 0.0f;

        var internalEmission = internalSmoke.emission;
        var externalEmission = externalSmoke.emission;

        var initialInternalRate = internalEmission.rateOverTime.constant;
        var initialExternalRate = externalEmission.rateOverTime.constant;

        while (time < disapearDuration)
        {
            time += Time.deltaTime;
            float t = time / disapearDuration;

            // Interpolate the rateOverTime from the initial value to 0
            internalEmission.rateOverTime = Mathf.Lerp(initialInternalRate, 0, t);
            externalEmission.rateOverTime = Mathf.Lerp(initialExternalRate, 0, t);

            yield return null;
        }

        // Ensure the emission rate is set to 0 at the end
        internalEmission.rateOverTime = 0;
        externalEmission.rateOverTime = 0;

        FMODManager.Instance.StopConstant2DSound("SFX/LandingThrust");
        Destroy(internalSmoke.gameObject);
    }
    #endregion

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        if (centerPoint != null)
        {
            Gizmos.color = boxColor;
            Gizmos.matrix = Matrix4x4.TRS(centerPoint.position, transform.rotation, Vector3.one);
            Gizmos.DrawCube(Vector3.zero, size);
        }
    }
#endif
}
