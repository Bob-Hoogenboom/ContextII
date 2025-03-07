using UnityEngine;
using UnityEngine.EventSystems;

public class CursorHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData data)
    {
        CursorManager.instance.SetToMode(CursorModes.HOVER);
    }

    public void OnPointerExit(PointerEventData data)
    {
        CursorManager.instance.SetToMode(CursorModes.DEFAULT);
    }
}
