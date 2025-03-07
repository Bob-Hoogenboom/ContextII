using UnityEngine;

public class ClimbTrigger : MonoBehaviour, IInteractable
{
    public InteractType interactType => InteractType.CLIMBABLE;

    public Collider ladderTop;
    public Transform climbLanding;

    public void Interact()
    {

    }
}
