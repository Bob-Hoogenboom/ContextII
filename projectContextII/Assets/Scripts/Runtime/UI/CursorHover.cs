using UnityEngine;
using UnityEngine.EventSystems;

public class CursorHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData data)
    {
        if (CursorManager.instance == null) return;
        CursorManager.instance.SetToMode(CursorModes.HOVER);
    }

    public void OnPointerExit(PointerEventData data)
    {
        if (CursorManager.instance == null) return;
        CursorManager.instance.SetToMode(CursorModes.DEFAULT);
    }
}
