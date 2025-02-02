using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    private Button _playButton;
    private Button _quitButton;
    private Button _optionsButton;
    private Button _backButton;

    private void Awake()
    {
        //Getting the UI toolkit to connect with the script
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        //Linking buttions to the UI

        _playButton = root.Q<Button>("PlayButton");
        _quitButton = root.Q<Button>("QuitButton");
        _optionsButton = root.Q<Button>("OptionsButton");
        _backButton = root.Q<Button>("BackButton");

        //linking the UI to the click events
        _playButton.clicked += OnPlayButtonClick;
        _quitButton.clicked += OnQuitButtonClick;
        _optionsButton.clicked += OnOptionsButtonClick;
    }

    private void OnPlayButtonClick()
    {
        Debug.Log("Play Button Clicked!");
        SceneManager.LoadScene(1);
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