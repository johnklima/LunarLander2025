using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.Rendering;
using UnityEngine;
using System;

/// <summary>
/// Controll pause UI
/// 
/// Summary of changes made on 2025-02-02:
/// - Renamed class from PauseMenuscript to Pause.
/// - Added public field for Volume to handle blur effects.
/// - Introduced static Instance property for singleton pattern implementation.
/// - Added private field for DepthOfField to manage depth of field effects.
/// - Added Actions for OnPause and OnResume events.
/// - Renamed and refactored boolean field for game pause state to isGamePause.
/// - Updated input system to use InputsManager for handling pause input.
/// - Added OnEnable and OnDisable methods for input and event handling.
/// - Refactored Awake method to initialize UI elements and set initial states.
/// - Added PauseHandler method to manage game pause and resume logic, including blur effect handling.
/// - Updated OnContinueButtonClick, OnOptionsButtonClick, OnMainMenuButtonClick, and OnQuitButtonclick methods for new logic.
/// - Added OnWinAndLoseHandler method to disable pause input on win or lose.
/// - Removed deprecated EscPressed field and related logic.
/// </summary>
public class Pause : MonoBehaviour
{
    #region Public Fields
    public Volume blurVolume;
    #endregion

    #region Static Values
    private static Pause _instance;
    public static Pause Instance
    {
        get
        {
            // Si la instancia es nula, se intenta encontrar una existente
            if (_instance == null)
            {
                _instance = FindObjectOfType<Pause>();

                // Si no se encuentra, se lanza una excepción
                if (_instance == null)
                {
                    throw new System.Exception("No se encontró ninguna instancia de PauseManager en la escena.");
                }
            }
            return _instance;
        }
    }
    #endregion

    #region Private Fields
    private DepthOfField depthOfField;
    #endregion

    #region Actions
    public Action OnPause;
    public Action OnResume;
    #endregion

    #region Private Values
    // References
    private VisualElement _PauseUI;
    private Button _continueButton;
    private Button _mainmenuButton;
    private Button _quitButton;
    private Button _optionsButton;

    // Values
    private bool isGamePause = false;
    #endregion

    #region Mono
    private void OnEnable()
    {
        InputsManager.Player.Pause.Enable();
        InputsManager.Player.Pause.performed += PauseHandler;
        LevelManager.Instance.OnWin += OnWinAndLoseHanlder;
        LevelManager.Instance.OnLose += OnWinAndLoseHanlder;
    }
    private void OnDisable()
    {
        InputsManager.Player.Pause.performed -= PauseHandler;
    }
    void Awake ()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        _PauseUI = root.Q<VisualElement>("PauseFrame");
       
        _continueButton = root.Q<Button>("ContinueButton");
        _mainmenuButton = root.Q<Button>("MainMenuButton");
        _quitButton = root.Q<Button>("QuitButton");
        _optionsButton = root.Q<Button>("OptionsButton");

        _PauseUI.style.display = DisplayStyle.None;
        _continueButton.clicked += OnContinueButtonClick;
        _mainmenuButton.clicked += OnMainMenuButtonClick;
        _quitButton.clicked += OnQuitButtonclick;
        _optionsButton.clicked += OnOptionsButtonClick;
    }
    #endregion

    #region Internal
    private void PauseHandler(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        isGamePause = !isGamePause;

        if (isGamePause)
        {
            _PauseUI.style.display = DisplayStyle.Flex;
            OrbitCamera.Instance.AllowCameraManagment(false);
            OrbitCamera.Instance.SetCursorState(false);
            if (blurVolume.profile.TryGet<DepthOfField>(out depthOfField)) depthOfField.active = true;

            Time.timeScale = 0;
            OnPause?.Invoke();
            Debug.Log("Pause");
        }
        else if (!isGamePause)
        {
            _PauseUI.style.display = DisplayStyle.None;
            OrbitCamera.Instance.AllowCameraManagment(true);
            OrbitCamera.Instance.SetCursorState(true);
            if (blurVolume.profile.TryGet<DepthOfField>(out depthOfField)) depthOfField.active = false;

            Time.timeScale = 1;
            OnResume?.Invoke();
            Debug.Log("Resume");
        }
    }
    private void OnContinueButtonClick()
    {
        isGamePause = !isGamePause;
        _PauseUI.style.display = DisplayStyle.None;
        OrbitCamera.Instance.AllowCameraManagment(true);
        OrbitCamera.Instance.SetCursorState(true);

        Time.timeScale = 1;
        Debug.Log("Continue");
    }
    private void OnOptionsButtonClick()
    {
        Debug.Log("Open options");
    }
    private void OnMainMenuButtonClick()
    {
        Time.timeScale = 1;
        FadeManager.Instance.FadeInOut(Color.black, 1f, 1f, () =>
        {
            FMODManager.Instance.StopAllConstant2DSound();
            SceneManager.LoadScene(0);
        });

        Debug.Log("Back to main menu");
    }
    private void OnQuitButtonclick()
    {
        Debug.Log("You have clicked Quit");
        Application.Quit();
    }
    #endregion

    #region Callbacks
    private void OnWinAndLoseHanlder()
    {
        InputsManager.Player.Pause.Disable();
    }
    #endregion
}
