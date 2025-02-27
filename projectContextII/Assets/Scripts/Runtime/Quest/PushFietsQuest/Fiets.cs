using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fiets : MonoBehaviour, IInteractable
{
    [SerializeField]
    private bool fietsIsGeduwed = false;
    [SerializeField]
    private Collider boxCol;

    public InteractType interactType => InteractType.DEFAULT;

    public void Interact()
    {
        FietsTyftOm();
    }

    private void FietsTyftOm()
    {
        if (fietsIsGeduwed) return;

        GameManager.instance.miscEvents.DuwFiets();

        boxCol.enabled = false;
        transform.Rotate(-90f, 0f, 0f);
        fietsIsGeduwed = true;
    }
}
