using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionUI : MonoBehaviour
{
    [SerializeField] private Button levelButtonPref;
    [SerializeField] private GameObject levelsLayout;
    [SerializeField] private SceneLoader sceneLoader;

    private void Start()
    {
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            var button = Instantiate(levelButtonPref, levelsLayout.transform);
            var values = button.GetComponentInChildren<LevelNameValues>();

            int levelId = i;
            values.SetNumber(levelId);

            button.onClick.AddListener(() => sceneLoader.LoadScene(levelId));
        }
    }
}
