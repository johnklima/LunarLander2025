using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance;

    public Image fadeImage;
    private Color targetColor;
    private float fadeDuration;
    private bool isFading;

    #region Mono
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    #region Public Methods
    public void FadeIn(Color color, float duration)
    {
        StartCoroutine(FadeInCoroutine(color, duration));
    }
    public void FadeOut(Color color, float duration)
    {
        StartCoroutine(FadeOutCoroutine(color, duration));
    }
    public void FadeInOut(Color color, float durationIn, float durationOut, System.Action onFadeComplete = null)
    {
        StartCoroutine(FadeInOutCoroutine(color, durationIn, durationOut, onFadeComplete));
    }
    #endregion

    #region Coroutines
    private IEnumerator FadeInCoroutine(Color color, float duration)
    {
        fadeImage.color = new Color(color.r, color.g, color.b, 0);
        fadeImage.gameObject.SetActive(true);

        for (float t = 0; t <= duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            fadeImage.color = new Color(color.r, color.g, color.b, normalizedTime);
            yield return null;
        }

        fadeImage.color = color;
    }
    private IEnumerator FadeOutCoroutine(Color color, float duration)
    {
        fadeImage.color = color;
        fadeImage.gameObject.SetActive(true);

        for (float t = 0; t <= duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            fadeImage.color = new Color(color.r, color.g, color.b, 1 - normalizedTime);
            yield return null;
        }

        fadeImage.color = new Color(color.r, color.g, color.b, 0);
        fadeImage.gameObject.SetActive(false);
    }
    private IEnumerator FadeInOutCoroutine(Color color, float durationIn, float durationOut, System.Action onFadeComplete)
    {
        yield return StartCoroutine(FadeInCoroutine(color, durationIn));
        onFadeComplete?.Invoke();
        yield return StartCoroutine(FadeOutCoroutine(color, durationOut));
    }
    #endregion
}