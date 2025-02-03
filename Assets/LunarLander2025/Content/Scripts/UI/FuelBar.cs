using UnityEngine.UIElements;
using UnityEngine;

public class FuelBar : MonoBehaviour
{
    public float fuelLevel = 100;

    #region Private Fields
    private VisualElement root;
    private VisualElement fuelBarElement;
    private ProgressBar _fuelBar;
    private FuelTank fuelTank;
    #endregion

    #region Mono
    void Awake()
    {
        // Get references
        fuelTank = FindAnyObjectByType<FuelTank>();

        // Connecting and getting the root to UI Toolkit document 
        root = GetComponent<UIDocument>().rootVisualElement;

        _fuelBar = root.Q<ProgressBar>("Fuelbar");
        fuelBarElement = root.Q<VisualElement>("ParentFuel");
    }
    private void Start()
    {
        fuelLevel = fuelTank.maxFuel;
        _fuelBar.highValue = fuelLevel;
    }
    private void OnEnable()
    {
        Pause.Instance.OnPause += OnPauseHandler;
        Pause.Instance.OnResume += OnResumeHandler;
        LevelManager.Instance.OnWin += OnWinAndLoseHandler;
        LevelManager.Instance.OnLose += OnWinAndLoseHandler;
    }
    private void OnDisable()
    {
        Pause.Instance.OnPause -= OnPauseHandler;
        Pause.Instance.OnResume = OnResumeHandler;
    }
    #endregion

    #region Internal
    public void UpdateFuel(float value)
    {
        // Linking value from Uitoolkit
        _fuelBar.value = value;

        // If fuel is less then 25 it will turn red if not it will be normal green
        if (value <= (fuelLevel / 4))
        {
            _fuelBar.AddToClassList("low");
        }

        else
        {
            _fuelBar.AddToClassList("normal");
        }
    }
    #endregion

    #region Callbaks
    private void OnPauseHandler()
    {       
        fuelBarElement.style.display = DisplayStyle.None;
    }
    private void OnResumeHandler()
    {
        fuelBarElement.style.display = DisplayStyle.Flex;
    }
    private void OnWinAndLoseHandler()
    {
        fuelBarElement.style.display = DisplayStyle.None;
    }
    #endregion
}
