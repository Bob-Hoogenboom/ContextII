using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fiets : MonoBehaviour, IInteractable
{
    [Header("Config")]
    [SerializeField] private float respawnTimeSeconds = 5;
    [SerializeField]
    private bool fietsIsGeduwed = false;
    [SerializeField]
    private Collider boxCol;

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

    public void Interact()
    {
        FietsTyftOm();
    }

    public void InteractPopUp()
    {
        OnInteractUIUpdate?.Invoke(interactDescription, interactIcon, transform);
    }

    public void HidePopUp()
    {
        OnUIHide?.Invoke();
    }

    private void FietsTyftOm()
    {
        if (fietsIsGeduwed) return;

        GameManager.instance.miscEvents.DuwFiets();

        boxCol.enabled = false;
        transform.Rotate(-90f, 0f, 0f);
        fietsIsGeduwed = true;
        StopAllCoroutines();
        StartCoroutine(RespawnAfterTime());
    }

    private IEnumerator RespawnAfterTime()
    {
        yield return new WaitForSeconds(respawnTimeSeconds);
        boxCol.enabled = true;
        transform.Rotate(0f, 0f, 0f);
    }
}
