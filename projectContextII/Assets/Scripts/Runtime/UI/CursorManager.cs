using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// Source: https://www.youtube.com/watch?v=5bSyIuMEjXE
/// </summary>
 

public enum CursorModes
{
    DEFAULT,
    HOVER
}

public class CursorManager : MonoBehaviour
{
    public static CursorManager instance { get; private set; }

    [SerializeField]
    private Texture2D cursorDefault;
    [SerializeField]
    private Texture2D cursorHover;

    [SerializeField]
    private Vector2 cursorHotspot = Vector2.zero;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Cursor.SetCursor(cursorDefault, cursorHotspot, CursorMode.Auto);
    }

    public void SetToMode(CursorModes cursorMode)
    {
        switch (cursorMode)
        {
            case CursorModes.DEFAULT:
                Cursor.SetCursor(cursorDefault, cursorHotspot, CursorMode.Auto);
                break;
            case CursorModes.HOVER:
                Cursor.SetCursor(cursorHover, cursorHotspot, CursorMode.Auto);
                break;
            default:
                Cursor.SetCursor(cursorDefault, cursorHotspot, CursorMode.Auto);
                break;

        }
    }
}
