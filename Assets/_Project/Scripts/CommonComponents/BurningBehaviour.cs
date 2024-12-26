using System.Collections;
using UnityEngine;

public class BurningBehaviour : MonoBehaviour
{
    // public GameObject burnEffect;
    // private ParticleSystem burnEffectInstance;
    [SerializeField] private int burnStacks;
    private float burnCountdown;
    private bool isBurning = false;
    [Min(0)] public int maxBurnStacks = 5;
    [Min(0)] public int burnProcsPerStack = 2;
    [Min(0)] public float burnDuration = 2;

    private void Awake()
    {
        // burnEffectInstance = Instantiate(burnEffect, transform).GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        SetupAndConfigure();
    }

    private void Update()
    {
        if (isBurning)
        {
            UpdateBurnTimer();
        }
    }

    public virtual void ApplyBurnStack()
    {
        if (!isBurning)
        {
            isBurning = true;
            if (TryGetComponent<HealthSystem>(out var healthSystem))
            {
                StartCoroutine(ProcBurning(healthSystem));
            }
        }

        burnStacks = Mathf.Min(burnStacks + 1, maxBurnStacks);
        burnCountdown = burnDuration;
    }

    public virtual void UpdateBurnTimer()
    {
        if (burnCountdown > 0)
        {
            burnCountdown -= Time.deltaTime;
        }

        if (burnCountdown <= 0 && isBurning)
        {
            RemoveBurnStack();
        }
    }

    public virtual void RemoveBurnStack()
    {
        burnStacks = Mathf.Max(burnStacks - 1, 0);
        if (burnStacks == 0) { isBurning = false; }
        burnCountdown = isBurning ? burnDuration : 0;
    }

    public virtual IEnumerator ProcBurning(HealthSystem healthSystem)
    {
        float _defense = healthSystem.Defense;
        healthSystem.Defense /= 2;
        // burnEffectInstance.Play();

        while (isBurning)
        {
            float _damage = burnStacks * burnStacks / 2;
            healthSystem.TakeDamage(_damage);
            yield return new WaitForSeconds(burnDuration / burnProcsPerStack);
        }

        healthSystem.Defense = _defense;
        // burnEffectInstance.Stop();
    }

    public void SetupAndConfigure()
    {
        burnStacks = 0;
        burnCountdown = 0;
    }
}