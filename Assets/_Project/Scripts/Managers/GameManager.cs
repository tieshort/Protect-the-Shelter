using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Transform PlayerBase;
    public Transform EffectsCollector;
    public int PlayerHealth = 20;
    public int PlayerMoney = 50;

    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject loseUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
        if (PlayerBase == null)
        {
            PlayerBase = GameObject.FindGameObjectWithTag("PlayerBase").transform;
        }
    }

    private void OnEnable()
    {
        winUI.SetActive(false);
        loseUI.SetActive(false);
    }

    private void Update()
    {
        if (PlayerHealth <= 0)
        {
            LoseLevel();
        }
    }

    public static void WinLevel()
    {
        Time.timeScale = 0;
        Instance.winUI.SetActive(true);
        TimePauseUI.pausingEnabled = false;
        CameraController.CameraMovementEnabled = false;
    }

    public static void LoseLevel()
    {
        Time.timeScale = 0;
        Instance.loseUI.SetActive(true);
        TimePauseUI.pausingEnabled = false;
        CameraController.CameraMovementEnabled = false;
    }

    private GameManager(){}
}
