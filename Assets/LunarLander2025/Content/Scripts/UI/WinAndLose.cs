using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.Rendering;
using UnityEngine;

public class WinAndLose : MonoBehaviour
{
    #region Public Fields
    [Header("Blur Effect")]
    public Volume blurVolume;

    [Header("Text")]
    public Sprite winText;
    public Sprite loseText;

    [Header("Stars")]
    public Sprite emptyStar;
    public Sprite halfStar;
    public Sprite fullStar;
    #endregion

    #region Static Values
    private static WinAndLose _instance;
    public static WinAndLose Instance
    {
        get
        {
            // If the instance is null, an attempt is made to find an existing one.
            if (_instance == null)
            {
                _instance = FindObjectOfType<WinAndLose>();

                // If not found, an exception is thrown
                if (_instance == null)
                {
                    throw new System.Exception("No instance of WinAndLose was found in the scene.");
                }
            }
            return _instance;
        }
    }
    #endregion

    #region Private Fields
    private VisualElement root;
    private VisualElement visualRoot;
    private VisualElement starA;
    private VisualElement starB;
    private VisualElement starC;
    private VisualElement textToShow;
    private Button retrayButton;
    private Button mainMenuButton;
    private Button nextLevelButton;
    private DepthOfField depthOfField;
    #endregion

    public float FinalScore { get; set; }

    #region Mono
    void Awake()
    {
        // Connect and get the UI Toolkit document root
        root = GetComponent<UIDocument>().rootVisualElement;

        visualRoot = root.Q<VisualElement>("root");
        textToShow = root.Q<VisualElement>("text");
        starA = root.Q<VisualElement>("star_a");
        starB = root.Q<VisualElement>("star_b");
        starC = root.Q<VisualElement>("star_c");
        retrayButton = root.Q<Button>("retray_button");
        mainMenuButton = root.Q<Button>("mainmenu_button");
        nextLevelButton = root.Q<Button>("nextlevel_button");
    }

    void OnEnable()
    {
        retrayButton.clickable.clicked += OnRetryButtonClick;
        mainMenuButton.clickable.clicked += OnMainMenuButtonClick;
        nextLevelButton.clickable.clicked += OnNextLevelButtonClick;

        LevelManager.Instance.OnWin += OnWinHandler;
        LevelManager.Instance.OnLose += OnLoseHandler;
    }

    private void OnDisable()
    {
        retrayButton.clickable.clicked -= OnRetryButtonClick;
        mainMenuButton.clickable.clicked -= OnMainMenuButtonClick;
        nextLevelButton.clickable.clicked -= OnNextLevelButtonClick;
    }
    #endregion

    #region Internal
    private void DisplayElement()
    {
        visualRoot.style.display = DisplayStyle.Flex;
        if (blurVolume.profile.TryGet<DepthOfField>(out depthOfField)) depthOfField.active = true;
    }
    #endregion

    #region Callbacks
    private void OnWinHandler()
    {
        DisplayElement();
        textToShow.style.backgroundImage = new StyleBackground(winText);
        SetStarsFromScore(FinalScore);
        retrayButton.style.display = DisplayStyle.Flex;
        nextLevelButton.style.display = DisplayStyle.Flex;
    }

    private void OnLoseHandler()
    {
        DisplayElement();
        textToShow.style.backgroundImage = new StyleBackground(loseText);
        retrayButton.style.display = DisplayStyle.Flex;
    }

    private void OnMainMenuButtonClick()
    {
        Debug.Log("Back to main menu");
        FadeManager.Instance.FadeInOut(Color.black, 1f, 1f, () =>
        {
            //FMODManager.Instance.StopAllConstant2DSound();
            SceneManager.LoadScene(0);
        });
    }

    private void OnRetryButtonClick()
    {
        Debug.Log("Retry this level");
        FadeManager.Instance.FadeInOut(Color.black, 1f, 1f, () =>
        {
            var currentSceneID = SceneManager.GetActiveScene().buildIndex;
            //FMODManager.Instance.StopAllConstant2DSound();
            SceneManager.LoadScene(currentSceneID);
        });
    }

    private void OnNextLevelButtonClick()
    {
        Debug.Log("Go to next Level");
        FadeManager.Instance.FadeInOut(Color.black, 1f, 1f, () =>
        {
            var nextSceneID = SceneManager.GetActiveScene().buildIndex + 1;
            //FMODManager.Instance.StopAllConstant2DSound();
            SceneManager.LoadScene(nextSceneID);
        });
    }

    private void SetStarsFromScore(float score)
    {
        // Log the final score for debugging purposes
        Debug.Log($"Score: {score}");

        // Determine the number of full stars and half stars based on the score
        int fullStars = Mathf.FloorToInt(score); // Number of full stars
        bool hasHalfStar = (score - fullStars) >= 0.5f; // Check if there's a half star

        // Assign the sprites based on the score
        starA.style.backgroundImage = new StyleBackground(emptyStar);
        starB.style.backgroundImage = new StyleBackground(emptyStar);
        starC.style.backgroundImage = new StyleBackground(emptyStar);

        if (fullStars >= 1)
        {
            starA.style.backgroundImage = new StyleBackground(fullStar);
        }
        if (fullStars >= 2)
        {
            starB.style.backgroundImage = new StyleBackground(fullStar);
        }
        if (fullStars == 3)
        {
            starC.style.backgroundImage = new StyleBackground(fullStar);
        }
        else if (hasHalfStar)
        {
            switch (fullStars)
            {
                case 0:
                    starA.style.backgroundImage = new StyleBackground(halfStar);
                    break;
                case 1:
                    starB.style.backgroundImage = new StyleBackground(halfStar);
                    break;
                case 2:
                    starC.style.backgroundImage = new StyleBackground(halfStar);
                    break;
            }
        }
    }
    #endregion
}