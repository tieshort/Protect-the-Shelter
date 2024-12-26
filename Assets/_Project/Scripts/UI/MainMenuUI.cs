using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject levelSelectionUI;
    [SerializeField] private GameObject settingsUI;
    [SerializeField] private GameObject creditsUI;
    private GameObject currentUI;

    private void OnEnable()
    {
        CloseAll();
        currentUI = mainMenuUI;
        currentUI.SetActive(true);
    }

    public void ShowMainMenu()
    {
        SetUI(mainMenuUI);
    }

    public void ShowSettings()
    {
        SetUI(settingsUI);
    }

    public void ShowLevelSelector()
    {
        SetUI(levelSelectionUI);
    }

    public void ShowCredits()
    {
        SetUI(creditsUI);
    }

    public void ExitGame()
    {
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    private void CloseAll()
    {
        mainMenuUI.SetActive(false);
        levelSelectionUI.SetActive(false);
        settingsUI.SetActive(false);
        creditsUI.SetActive(false);
    }

    private void SetUI(GameObject UI)
    {
        currentUI.SetActive(false);
        currentUI = UI;
        currentUI.SetActive(true);
    }
}
