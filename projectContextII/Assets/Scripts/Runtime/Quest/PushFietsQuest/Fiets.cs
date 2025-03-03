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
