using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Ship thrusters and input.
/// 
/// Update 27.01.2025 - Robin Adding default values and minimum values for:
/// thrustBuildRate and  thrustDecayRate
/// 
/// /// Summary of changes made on 2025-02-02:
/// - Removed UnityEngine.InputSystem namespace and associated input handling logic.
/// - Added properties for ThrusterState with a default value of ThrusterState.On.
/// - Introduced private references to Gravity, FuelTank, and LanderController components.
/// - Added disapearDuration constant for handling particle effect disappearance.
/// - Updated Awake method to initialize Gravity, FuelTank, and LanderController references.
/// - Updated Update method to include checks for ThrusterState and PlayerState, and refactored thrust control logic.
/// - Moved particle effects control logic to a new UpdateThrusterEffects method.
/// - Added a coroutine for gradually reducing particle emission rates in DisapearEffect.
/// - Removed original thruster region and deprecated methods.
/// </summary>
public class Thrusters : MonoBehaviour
{
    #region Public Values
    public float fuelEfficiency = 1f;
    public Vector3 upDirection;

    public float currentThrust;
    public float maxThrust;
    [Min(1)] public float thrustBuildRate = 5f; //default value 5, min value 1
    [Min(1)] public float thrustDecayRate = 5f;  //default value 5, min value 1

    public ParticleSystem firePS;
    public ParticleSystem smokePS;
    public ParticleSystem bSmokePS;
    #endregion

    #region Properties
    public ThrusterState ThrusterState { get; set; } = ThrusterState.Off;
    #endregion

    #region Pivate Values
    // References
    private Gravity gravity;
    private FuelTank fuelTank;
    private LanderSmoother landerSmoother;
    private LanderController landerController;

    // Values
    private readonly float disapearDuration = 0.4f;
    private bool isThrusterOn = false;
    #endregion

    #region Mono
    private void Awake()
    {
        gravity = GetComponent<Gravity>();
        fuelTank = GetComponent<FuelTank>();
        landerSmoother = FindAnyObjectByType<LanderSmoother>();
        landerController = GetComponent<LanderController>();
    }
    void Update()
    {
        isThrusterOn = !landerSmoother.IsAligning ? InputsManager.Player.OnOffThruster.ReadValue<float>() > 0.1f : true;
        ThrusterState = isThrusterOn ? ThrusterState.On : ThrusterState.Off;

        // Thrust control
        float targetThrust = 0f;

        if (ThrusterState == ThrusterState.On && fuelTank.BurnFuel(fuelEfficiency * Time.deltaTime))
        {
            targetThrust = maxThrust;
        }

        // Particle effects control (if any)
        UpdateThrusterEffects(targetThrust);

        if (landerController.PlayerState != PlayerState.Flying) return;
        if (ThrusterState == ThrusterState.Off) return;

        // Gradual thrust adjustment
        currentThrust = Mathf.MoveTowards(currentThrust, targetThrust, (targetThrust > currentThrust ? thrustBuildRate : thrustDecayRate) * Time.deltaTime);

        // Apply thrust in the current direction of the ship.
        gravity.thrust = transform.up * currentThrust;
    }
    #endregion

    #region Internal
    void UpdateThrusterEffects(float targetThrust)
    {
        if (ThrusterState == ThrusterState.Off)
        {
            if (firePS != null)
            {
                var emission = firePS.emission;
                StartCoroutine(DisapearEffect(emission));
            }
            if (smokePS != null)
            {
                var emission = smokePS.emission;
                StartCoroutine(DisapearEffect(emission));
            }
            if (bSmokePS != null)
            {
                var emission = bSmokePS.emission;
                StartCoroutine(DisapearEffect(emission));
            }
        }
        else
        {
            if (currentThrust < targetThrust)
            {
                if (firePS != null)
                {
                    var emission = firePS.emission;
                    emission.rateOverTime = currentThrust / maxThrust * 1000f;
                }
                if (smokePS != null)
                {
                    var emission = smokePS.emission;
                    emission.rateOverTime = currentThrust / maxThrust * 100f;
                }
                if (bSmokePS != null)
                {
                    var emission = bSmokePS.emission;
                    emission.rateOverTime = currentThrust / maxThrust * 100f;
                }
            }
            else
            {
                if (firePS != null)
                {
                    var emission = firePS.emission;
                    emission.rateOverTime = currentThrust / maxThrust * 500f;
                }
                if (smokePS != null)
                {
                    var emission = smokePS.emission;
                    emission.rateOverTime = currentThrust / maxThrust * 50;
                }
                if (bSmokePS != null)
                {
                    var emission = bSmokePS.emission;
                    emission.rateOverTime = currentThrust / maxThrust * 50;
                }
            }
        }
    }
    #endregion

    #region Coroutines
    private IEnumerator DisapearEffect(ParticleSystem.EmissionModule emission)
    {
        var time = 0.0f;
        var initialRate = emission.rateOverTime.constant;

        while (time < disapearDuration)
        {
            time += Time.deltaTime;
            float t = time / disapearDuration;

            // Interpolate the rateOverTime from the initial value to 0
            emission.rateOverTime = Mathf.Lerp(initialRate, 0, t);

            yield return null;
        }

        // Ensure the emission rate is set to 0 at the end
        emission.rateOverTime = 0;
    }
    #endregion
}
