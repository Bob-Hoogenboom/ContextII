using UnityEngine;


public class PushableTrigger : MonoBehaviour, IInteractable
{

    [Header("Interface")]
    [SerializeField] 
    private string interactDescription = "";
    [SerializeField]
    private Sprite interactIcon;

    public InteractType interactType => InteractType.PUSHABLE;
    public event IInteractable.InteractUI OnInteractUIUpdate;
    public event IInteractable.UIHideEvent OnUIHide;
    public string InteractDescription => interactDescription;
    public Sprite InteractIcon => interactIcon;


    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

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
