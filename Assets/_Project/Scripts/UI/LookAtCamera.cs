using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    // private Transform cam;

    // private void Awake()
    // {
    //     cam = Camera.main.transform;
    // }

    private void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;
    }
}
