using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] private TMP_Text lives;
    private int maxLives;
    [SerializeField] private TMP_Text money;
    [SerializeField] private List<ShopItem> shopItemsList = new();
    [SerializeField] private HorizontalLayoutGroup btnsLayout;
    [SerializeField] private Button buttonPref;
    [SerializeField] private TMP_Text timeScaleText;
    [SerializeField] private TMP_Text waveNumberText;

    private void Start()
    {
        foreach (var item in shopItemsList)
        {
            Button button = Instantiate(buttonPref, btnsLayout.transform);
            var text = button.GetComponentInChildren<TMP_Text>();
            text.text = $"{item.ItemName}\n{item.Price}";
            button.onClick.AddListener(() => BuildManager.Instance.SetObjectToBuild(item));
        }
        maxLives = GameManager.Instance.PlayerHealth;
    }

    private void LateUpdate()
    {
        lives.text = $"{GameManager.Instance.PlayerHealth}/{maxLives} HP";
        money.text = $"{GameManager.Instance.PlayerMoney} $";
        timeScaleText.text = $"Game speed: {Time.timeScale}x";
        waveNumberText.text = $"Wave: {WaveSpawner.WaveNumber}/{WaveSpawner.MaxWaves}";
    }
}
