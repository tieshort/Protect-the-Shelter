using UnityEngine;

public class TimePauseUI : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
    private bool paused = false;
    public static bool pausingEnabled;


    private void OnEnable()
    {
        pausingEnabled = true;
        pauseUI.SetActive(false);
        Time.timeScale = 0; 
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pausingEnabled)
        {
            TogglePause();
        }

        if (!paused)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Time.timeScale = Time.timeScale == 0 ? 1 : 0;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Time.timeScale = 1f;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Time.timeScale = 2f;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Time.timeScale = 3f;
            }
        }
    }

    public void TogglePause()
    {
        if (paused)
        {
            Time.timeScale = 1f;
            pauseUI.SetActive(false);
            CameraController.CameraMovementEnabled = true;
        }
        else
        {
            Time.timeScale = 0f;
            pauseUI.SetActive(true);
            CameraController.CameraMovementEnabled = false;
        }
        paused = !paused;
    }
}
