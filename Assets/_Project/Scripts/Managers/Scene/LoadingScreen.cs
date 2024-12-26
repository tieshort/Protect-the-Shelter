using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Slider progressBar;
    [SerializeField] private TMP_Text progressText;

    private void Awake()
    {
        progressBar.minValue = 0;
        progressBar.maxValue = 1;
    }

    private void Start()
    {
        canvasGroup.alpha = 0;
    }

    public void Load(int scene, bool allowSceneActivation = false)
    {
        var loading = SceneManager.LoadSceneAsync(scene);
        StartCoroutine(LoadCoroutine(loading, allowSceneActivation));
    }

    public void Load(string scene, bool allowSceneActivation = false)
    {
        var loading = SceneManager.LoadSceneAsync(scene);
        StartCoroutine(LoadCoroutine(loading, allowSceneActivation));
    }

    private IEnumerator LoadCoroutine(AsyncOperation Loading, bool allowSceneActivation)
    {
        progressText.text = "Loading in progress...";
        // StartCoroutine(FadeLoadingScreen(1, 0.1f));
        canvasGroup.alpha = 1;
        Loading.allowSceneActivation = allowSceneActivation;
        float progress;

        while(!Loading.isDone)
        {
            progress = Loading.progress;
            if (!allowSceneActivation && progress >= 0.9f)
            {
                progress = 1;
                progressText.text = "Loading complete. Press any key to continue.";
            }
            progressBar.value = progress;

            if (progress == 1 && Input.anyKeyDown)
            {
                break;
            }
            yield return null;
        }

        // yield return StartCoroutine(FadeLoadingScreen(0, 1f));
        Loading.allowSceneActivation = true;
        // canvasGroup.alpha = 0;
    }

    private IEnumerator FadeLoadingScreen(float targetValue, float duration)
    {
        float startValue = canvasGroup.alpha;
        float time = 0;

        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startValue, targetValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = targetValue;
    }
}
