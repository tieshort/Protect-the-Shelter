using UnityEngine;

public class Flame : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        // Debug.Log("Огонь врезался");
        if (other.TryGetComponent<BurningBehaviour>(out var target))
        {
            target.ApplyBurnStack();
            // Debug.Log("Огонь поджег");
        }
    }
}
