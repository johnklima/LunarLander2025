using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    //Main Menu screen buttons

    private Button _playButton;
    private Button _quitButton;
    private Button _optionsButton;
    private Button _levelsButton;

    //Level selection buttons
    private Button _backButton;
    private Button _level1Button;
    private Button _level2Button;
    private Button _level3Button;
    private Button _optionsbackButton;

    //Getting the visual element to have controll over them
    private VisualElement _MainMenu;
    private VisualElement _LevelSelection;
    private VisualElement _OptionMenu;

    private void Awake()
    {
        //Getting the UI toolkit to connect with the script
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        _MainMenu = root.Q<VisualElement>("Background");
        _LevelSelection = root.Q<VisualElement>("LevelSelect");
        _OptionMenu = root.Q<VisualElement>("OptionsMenu");

        //Linking buttions to the UI
        _playButton = root.Q<Button>("PlayButton");
        _quitButton = root.Q<Button>("QuitButton");
        _optionsButton = root.Q<Button>("OptionsButton");
        _levelsButton = root.Q<Button>("LevelsButton");

        //level selection buttons and options
        _level2Button = root.Q<Button>("Level2");
        _level1Button = root.Q<Button>("Level1");
        _level3Button = root.Q<Button>("Level3");
        _optionsbackButton = root.Q<Button>("OptionsBackButton");
        _backButton = root.Q<Button>("BackButton");

        //linking the UI to the click events
        _playButton.clicked += OnPlayButtonClick;
        _quitButton.clicked += OnQuitButtonClick;
        _optionsButton.clicked += OnOptionsButtonClick;
        _levelsButton.clicked += OnlevelsButtonClick;

        //click event for level select
        _level3Button.clicked += OnLevel3ButtonClick;
        _level2Button.clicked += OnLevel2ButtonClick;
        _level1Button.clicked += OnLevel1ButtonClick;
        _backButton.clicked += OnBackButtonClick;

        _optionsbackButton.clicked += OnBackOptionsButtonClick;

        //hidde and only shows the Main menu UI
        _LevelSelection.style.display = DisplayStyle.None;
        _OptionMenu.style.display = DisplayStyle.None;
        _MainMenu.style.display = DisplayStyle.Flex;

    }

    private void OnPlayButtonClick()
    {
        Debug.Log("Play Button Clicked!");
        FadeManager.Instance.FadeInOut(Color.black, 1f, 1f, () =>
        {
            SceneManager.LoadScene(1);
        });
    }

    private void OnQuitButtonClick()
    {
        Debug.Log("You have clicked Quit");
        Application.Quit();
    }

    //Options click event 
    private void OnOptionsButtonClick()
    {
        Debug.Log("You have clicked Options");
        _OptionMenu.style.display = DisplayStyle.Flex;
        _MainMenu.style.display = DisplayStyle.None;
    }

    private void OnBackOptionsButtonClick()
    {
        _OptionMenu.style.display = DisplayStyle.None;
        _MainMenu.style.display = DisplayStyle.Flex;
    }


    //Level Menu click event what happens
    private void OnlevelsButtonClick()
    {
        _LevelSelection.style.display = DisplayStyle.Flex;
        _MainMenu.style.display = DisplayStyle.None;
    }

    private void OnBackButtonClick()
    {
        _LevelSelection.style.display = DisplayStyle.None;
        _MainMenu.style.display = DisplayStyle.Flex;
    }

    private void OnLevel1ButtonClick()
    {
        Debug.Log("Level 1 loading");
        SceneManager.LoadScene(1);
    }

    private void OnLevel2ButtonClick()
    {
        Debug.Log("Level 2 loading");
        SceneManager.LoadScene(2);
    }

    private void OnLevel3ButtonClick()
    {
        Debug.Log("Level 3 loading");
        SceneManager.LoadScene(3);
    }


}