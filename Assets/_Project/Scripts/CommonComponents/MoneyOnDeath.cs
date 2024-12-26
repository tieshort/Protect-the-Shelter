using UnityEngine;

public class MoneyOnDeath : MonoBehaviour
{
    [SerializeField] private int value;

    public static implicit operator int(MoneyOnDeath money) => money.value;
}
