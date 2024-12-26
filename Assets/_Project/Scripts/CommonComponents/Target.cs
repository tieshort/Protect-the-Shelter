using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    public TargetBlueprint blueprint;
    protected ParticleSystem hitEffectInstance;
    protected GameObject unitInfoUI;
    protected Slider healthBar;
    [SerializeField] protected float health;
    [SerializeField] protected float defense;
    [SerializeField] protected int burnStacks;
    protected float burnCountdown;
    [SerializeField] protected int slowStacks;
    protected float slowCountdown;
    protected bool isBurning = false;
    [HideInInspector] public NavMeshAgent agent;

    /*
    
    Движение пока что простое - есть агент, есть цель, идем к этой цели
    также неплохо бы напоминать о цели периодически, но тут как пойдет

    */


    /*

    Получение урона и смерть, все стандартно
    урон высчитывается с учетом защиты
    формула сопротивления : def / (def + const)

    */

    public virtual void TakeDamage(float damage)
    {
        health -= damage * (1 - defense / (defense + 50));
        hitEffectInstance.transform.position = transform.position;
        hitEffectInstance.Play();
        unitInfoUI.SetActive(true);
        healthBar.value = health;
    }

    public virtual void Die()
    {
        Destroy(hitEffectInstance.gameObject);
        Destroy(gameObject);
        return;
    }

    /*

    Процесс горения
    идея: при попадании огня накладывается стак, стаки должны накладываться с определенной частотой
    при наличии стаков IsBurning возвращает true и соответственно должен прокать урон огнем
    при горении защита обрезается в два раза, а прок стаков наносит урон, равный квадрату количества стаков
    проки статуса зависят от длительности стака и количества проков за стак
    счетчик горения должен уменьшаться каждый кадр, при уменьшении счетчика до <= 0 стак убирается и счетчик должен обновиться
    если накладывается новый стак, счетчик обновляется

    */

    public virtual void ApplyBurnStack()
    {
        if (!isBurning)
        {
            isBurning = true;
            StartCoroutine(ProcBurning());
        }

        burnStacks = Mathf.Min(burnStacks + 1, blueprint.maxBurnStacks);
        burnCountdown = blueprint.burnDuration;
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
        burnCountdown = isBurning ? blueprint.burnDuration : 0;
    }

    public virtual IEnumerator ProcBurning()
    {
        float _defense = defense;
        defense /= 2;

        while (isBurning)
        {
            float _damage = burnStacks * burnStacks / 2;
            TakeDamage(_damage);
            yield return new WaitForSeconds(blueprint.burnDuration / blueprint.burnProcsPerStack);
        }

        defense = _defense;
    }

    /*

    Методы unity
    
    */

    protected virtual void Awake()
    {
        unitInfoUI = Instantiate(blueprint.UnitInfoUI, transform.position, transform.rotation, transform);
        healthBar = unitInfoUI.transform.Find("HealthBar").GetComponentInChildren<Slider>();
        hitEffectInstance = Instantiate(blueprint.hitEffect, transform.position, Quaternion.identity, GameManager.Instance.EffectsCollector).GetComponent<ParticleSystem>();

        agent = GetComponent<NavMeshAgent>();
    }
}