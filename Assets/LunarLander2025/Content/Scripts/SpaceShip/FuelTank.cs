using UnityEngine;

/// <summary>
/// Read fuel and Burn fuel.
/// 
/// Summary of changes made on 2025-02-02:
/// - Added public FuelBar field to handle UI updates.
/// - Introduced private Thrusters reference for managing thruster state.
/// - Updated Awake method to initialize Thrusters reference.
/// - Removed default initialization of maxFuel in Start method.
/// - Enhanced BurnFuel method to update FuelBar UI and handle game state changes when fuel is depleted.
/// - Removed deprecated FuelUpgrade method.
/// </summary>
public class FuelTank : MonoBehaviour
{
    #region Public Fields
    public float maxFuel;
    public float currentFuel;
    public FuelBar fuelBarUI;
    #endregion

    #region Private Fields
    private Thrusters thrusters;
    #endregion

    #region Mono
    private void Awake()
    {
        thrusters = GetComponent<Thrusters>();
    }
    void Start()
    {
        currentFuel = maxFuel;      
    }
    #endregion

    #region Internal
    public bool BurnFuel(float burnRate)
    {  
        if (currentFuel > 0)
        {
            currentFuel -= burnRate;
            fuelBarUI.UpdateFuel(currentFuel);
            return true;   
        }
        else
        {
            thrusters.ThrusterState = ThrusterState.Off;
            OrbitCamera.Instance.AllowCameraManagment(false);
            OrbitCamera.Instance.SetCursorState(false);
            LevelManager.Instance.SetGameAsLose();
            return false;
        }
    }
    #endregion
}
