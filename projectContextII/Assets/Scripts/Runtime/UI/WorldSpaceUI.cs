using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorldSpaceUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private TMP_Text interactTXT;
    [SerializeField]
    private Image interactIcon;
    [SerializeField]
    private GameObject uiPanel;
    [SerializeField]
    private float uiHeight = 2f;

    private Transform target;

    private void Start()
    {
        IInteractable[] interactables = FindObjectsOfType<MonoBehaviour>().OfType<IInteractable>().ToArray();
        foreach (IInteractable interactable in interactables)
        {
            interactable.OnInteractUIUpdate += UpdateUI;
            interactable.OnUIHide += HideUI;
        }
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + Vector3.up * uiHeight;
        }
    }

    private void UpdateUI(string text, Sprite icon, Transform worldPos)
    {
        interactTXT.text = text;
        interactIcon.sprite = icon; 
        target = worldPos;
        uiPanel.SetActive(true);
    }

    private void HideUI()
    {
        uiPanel.SetActive(false);
        target = null;
    }

}
