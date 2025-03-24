using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouguet : MonoBehaviour, IInteractable
{
    [Header("ObjectConfig")]
    [SerializeField]
    private float respawnTime = 5f;
    [SerializeField]
    private bool isPickedUp = false;
    [SerializeField]
    private Collider col;
    [SerializeField]
    private GameObject flowerGRFX;

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
        FlowerTouched();
    }

    public void InteractPopUp()
    {
        OnInteractUIUpdate?.Invoke(interactDescription, interactIcon, transform);
    }

    public void HidePopUp()
    {
        OnUIHide?.Invoke();
    }

    private void FlowerTouched()
    {
        if (isPickedUp) return;

        GameManager.instance.miscEvents.FlowerGet();

        col.enabled = false;
        flowerGRFX.SetActive(false);
        StopAllCoroutines();
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTime);
        col.enabled = true;
        flowerGRFX.SetActive(true);
    }
}
