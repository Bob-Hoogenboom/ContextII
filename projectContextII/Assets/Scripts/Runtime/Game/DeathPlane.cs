using UnityEngine;
using UnityEngine.Events;

public class DeathPlane : MonoBehaviour
{
    public UnityEvent OnPlayerHit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerHit.Invoke();
        }
    }
}
