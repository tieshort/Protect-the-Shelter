using UnityEngine;

public class ApplyDDOL : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
