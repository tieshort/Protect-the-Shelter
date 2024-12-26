using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private LoadingScreen loading;

    public void LoadStartScene()
    {
        loading.Load(0, true);
    }

    public void LoadNext()
    {
        int index = GetCurrentSceneIndex() + 1;

        if (index < SceneManager.sceneCountInBuildSettings)
        {
            LoadScene(index);
        }

    }

    public void LoadPrevious()
    {
        int index = GetCurrentSceneIndex() - 1;

        if (index > 0)
        {
            LoadScene(index);
        }
        else if (index == 0)
        {
            // if there's some extra logic in menu loading func
            LoadStartScene();
        }
    }

    public void LoadScene(int index)
    {
        loading.Load(index);
    }

    public void LoadScene(string name)
    {
        loading.Load(name);
    }

    public void ReloadCurrentScene()
    {
        loading.Load(GetCurrentSceneIndex(), true);
    }

    private int GetCurrentSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
}
