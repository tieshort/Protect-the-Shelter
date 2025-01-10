using UnityEngine;
using UnityEngine.Localization.Components;

public class LevelNameValues : MonoBehaviour
{
    public int number = 0;

    public void SetNumber(int num)
    {
        if (TryGetComponent<LocalizeStringEvent>(out var localize))
        {
            number = num;
            localize.RefreshString();
        }
    }
}
