using UnityEngine;

public class ClimbTrigger : MonoBehaviour, IInteractable
{
    [Header("Interface")]
    [SerializeField]
    private string interactDescription = "";
    [SerializeField]
    private Sprite interactIcon;

    public InteractType interactType => InteractType.CLIMBABLE;
    public event IInteractable.InteractUI OnInteractUIUpdate;
    public event IInteractable.UIHideEvent OnUIHide;
    public string InteractDescription => interactDescription;
    public Sprite InteractIcon => interactIcon;


    public Collider ladderTop;
    public Transform climbLanding;

    public void Interact()
    {

    }

    public void InteractPopUp()
    {
        OnInteractUIUpdate?.Invoke(interactDescription, interactIcon, transform);
    }

    public void HidePopUp()
    {
        OnUIHide?.Invoke();
    }
}
