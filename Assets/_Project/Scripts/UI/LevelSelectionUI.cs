using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class LevelSelectionUI : MonoBehaviour
{
    [SerializeField] private Button levelButtonPref;
    [SerializeField] private GameObject levelsLayout;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private string levelNameTemplate = "Level 0";
    private readonly string pattern = @"[0-9]+";

    void Awake()
    {
        Regex regex = new(pattern);
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            var button = Instantiate(levelButtonPref, levelsLayout.transform);
            var buttonText = button.GetComponentInChildren<TMP_Text>();

            int levelId = i;
            string text = regex.Replace(levelNameTemplate, levelId.ToString());
            Debug.Log(text + ", Scene Index: " + levelId);
            buttonText.text = text;
            button.onClick.AddListener(() => sceneLoader.LoadScene(levelId));
        }
    }
}
