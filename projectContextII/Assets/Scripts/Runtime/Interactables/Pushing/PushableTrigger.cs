using UnityEngine;


public class PushableTrigger : MonoBehaviour, IInteractable
{
    public InteractType interactType => InteractType.PUSHABLE;

    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Interact()
    {

    }
}
