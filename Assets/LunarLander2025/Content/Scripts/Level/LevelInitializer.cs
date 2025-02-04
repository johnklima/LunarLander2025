using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
    #region Public Fields
    [Header("Needed References")]
    public Transform planetHolder;
    public Material skyMaterial;

    [Header("Init Values")]
    public string keySearch = "WinPoint";
    #endregion

    #region Static Values
    private static LevelInitializer _instance;
    public static LevelInitializer Instance
    {
        get
        {
            // If the instance is null, an attempt is made to find an existing one.
            if (_instance == null)
            {
                _instance = FindObjectOfType<LevelInitializer>();

                // If not found, an exception is thrown
                if (_instance == null)
                {
                    throw new System.Exception("No instances of LevelInitializer were found in the scene.");
                }
            }
            return _instance;
        }
    }
    #endregion

    #region Properties
    public Vector3 WinPoint { get; private set; }
    #endregion

    #region Private Fields
    private LevelDataSO levelData;
    private FuelTank shipFuelTank;
    #endregion

    private void Awake() => Init();
    private void Init()
    {
        // Obtain the necessary data to configure the level
        levelData = Resources.Load<LevelDataSO>("SOAssets/LevelsData");
        var curretData = levelData.GetData(SceneManager.GetActiveScene().buildIndex - 1);

        // Configure the maximum fuel of the ship for this level
        shipFuelTank = FindAnyObjectByType<FuelTank>();
        shipFuelTank.maxFuel = curretData.shipMaxFuel;

        // Generation of the planet
        GameObject planet = Instantiate(
            curretData.planetPrefab,
            planetHolder.transform.position, 
            curretData.planetPrefab.transform.rotation);

        planet.transform.SetParent(planetHolder);

        GameObject pad = FindInHierarchy(planet, keySearch);
        WinPoint = pad.transform.position;

        // Skybox configuration
        skyMaterial.SetFloat("_ZenithBlend", curretData.zenitBlend);
        skyMaterial.SetFloat("_NadirBlend", curretData.nadirBlend);
        skyMaterial.SetFloat("_HorizonBlend", curretData.horizontBlend);
        skyMaterial.SetColor("_SkyColor", curretData.skyColor);
        skyMaterial.SetColor("_GroundColor", curretData.groundColor);
        skyMaterial.SetColor("_HorizonColor", curretData.horizontColor);
        skyMaterial.SetFloat("_StarHeight", curretData.starHeight);
        skyMaterial.SetFloat("_StarPower", curretData.starPower);
        skyMaterial.SetFloat("_StarIntesity", curretData.starIntensity);

        // Sound initialization
        //FMODManager.Instance.PlayConstant2DSound("Music/BackgroundEnviroment");
        //FMODManager.Instance.PlayConstant2DSound("SFX/ShipEngine");
        //FMODManager.Instance.PlayConstant2DSound("SFX/ShipAlerts");
    }
    private GameObject FindInHierarchy(GameObject parent, string name)
    {
        // If the current object has the name we are looking for, we return it.
        if (parent.name == name)
        {
            return parent;
        }

        // We loop through all the children of the current object
        foreach (Transform child in parent.transform)
        {
            // We recursively call the function in each child
            GameObject result = FindInHierarchy(child.gameObject, name);
            if (result != null)
            {
                return result;
            }
        }

        // If we do not find the object, we return null
        return null;
    }
}
