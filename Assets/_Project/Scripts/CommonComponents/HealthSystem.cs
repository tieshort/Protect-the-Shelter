using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public GameObject hitEffect;
    private ParticleSystem hitEffectInstance;
    public GameObject UnitInfoUI;
    private GameObject unitInfoUI;
    private Slider healthBar;
    [SerializeField] private float maxHP;
    [SerializeField] private float def;
    [SerializeField] private float health;
    [HideInInspector] public float Defense;

    private void Awake()
    {
        unitInfoUI = Instantiate(UnitInfoUI, transform.position, transform.rotation, transform);
        healthBar = unitInfoUI.transform.Find("HealthBar").GetComponentInChildren<Slider>();
        hitEffectInstance = Instantiate(hitEffect, transform.position, Quaternion.identity, GameManager.Instance.EffectsCollector).GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        SetupAndConfigure(WaveSpawner.WaveNumber);
    }

    private void Update()
    {
        if (health <= 0)
        {
            if (TryGetComponent<MoneyOnDeath>(out var money))
            {
                GameManager.Instance.PlayerMoney += money;
            }
            Die();
        }
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage * (1 - Defense / (Defense + 50));
        hitEffectInstance.transform.position = transform.position;
        hitEffectInstance.Play();
        unitInfoUI.SetActive(true);
        healthBar.value = health;
    }

    public virtual void Die()
    {
        Destroy(hitEffectInstance.gameObject);
        Destroy(gameObject);
        GlobalEventManager.SendEnemyKilled(transform);
        return;
    }

    private void SetupAndConfigure(int waveNumber)
    {
        float statsMultiplier = 1 + waveNumber / 3f;
        health = maxHP * statsMultiplier;
        Defense = def * statsMultiplier;
        healthBar.maxValue = health;
        healthBar.value = health;
        unitInfoUI.SetActive(false);
    }
}