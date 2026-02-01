using UnityEngine;

public class KillZ : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Abilities ab = other.gameObject.GetComponent<Abilities>();
        if (ab != null)
        {
            ab.TakeDamage();
        }
    }
}
