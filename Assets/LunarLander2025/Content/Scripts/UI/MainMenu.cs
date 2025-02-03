using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private Button _playButton;
    private Button _quitButton;
    private Button _optionsButton;
    private Button _backButton;

    private void Awake()
    {
        // Getting the UI toolkit to connect with the script
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        // Linking buttions to the UI

        _playButton = root.Q<Button>("PlayButton");
        _quitButton = root.Q<Button>("QuitButton");
        _optionsButton = root.Q<Button>("OptionsButton");
        _backButton = root.Q<Button>("BackButton");

        // Linking the UI to the click events
        _playButton.clicked += OnPlayButtonClick;
        _quitButton.clicked += OnQuitButtonClick;
        _optionsButton.clicked += OnOptionsButtonClick;
    }

    private void OnPlayButtonClick()
    {
        Debug.Log("Play Button Clicked!");
        FadeManager.Instance.FadeInOut(Color.black, 1f, 1f, () =>
        {
            SceneManager.LoadScene(1);
        });
    }

    private void OnOptionsButtonClick()
    {
        Debug.Log("You have clicked Options");
    }

    private void OnQuitButtonClick()
    {
        Debug.Log("You have clicked Quit");
        Application.Quit();
    }
}