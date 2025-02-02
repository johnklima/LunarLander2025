using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Warning : MonoBehaviour
{
    private UIDocument warningScreen;
    public VisualElement warningText;
    public bool warningTextActive = true;

    private void OnEnable()
    {
        warningScreen = GetComponent<UIDocument>();
        warningText = warningScreen.rootVisualElement.Q("WarningText") as VisualElement;
    }

      // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //sets the warning text visibility
        if (warningTextActive) {
            warningText.style.display = DisplayStyle.Flex;
        } else {
            warningText.style.display = DisplayStyle.None;
        }
    }
}
