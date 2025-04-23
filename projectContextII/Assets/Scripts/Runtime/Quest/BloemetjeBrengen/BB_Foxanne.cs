using UnityEngine;

public class BB_Foxanne : QuestStep, IInteractable
{
    public Dialogue dialogue;

    [Header("Interface")]
    [SerializeField]
    private string interactDescription = "";
    [SerializeField]
    private Sprite interactIcon;

    public InteractType interactType => InteractType.DEFAULT;
    public event IInteractable.InteractUI OnInteractUIUpdate;
    public event IInteractable.UIHideEvent OnUIHide;
    public string InteractDescription => interactDescription;
    public Sprite InteractIcon => interactIcon;

    private bool _bloemetjeGebracht = false;

    public void BloemetjeGeven()
    {
        if (_bloemetjeGebracht) return;

        UpdateState();

        FinishedQuestStep();

        _bloemetjeGebracht = true;
    }

    private void UpdateState()
    {
        string state = _bloemetjeGebracht.ToString();
        ChangeState(state);
    }

    protected override void SetQuestStepState(string state)
    {
        this._bloemetjeGebracht = System.Boolean.Parse(state);
        UpdateState();
    }

    public void Interact()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
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
