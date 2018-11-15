using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // Required when using Event data. Use this to detect mouseover, etc. on UI elements.
using UnityEngine.UI;

public class MouseHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler // Required for OnPointer methods
{
    public Texture2D CursorPoint;
    public Texture2D CursorClick;

    public void OnPointerDown(PointerEventData eventData)
    {
        Cursor.SetCursor(CursorClick, Vector2.zero, CursorMode.Auto);
    }
     
    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(CursorPoint, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Cursor.SetCursor(CursorPoint, Vector2.zero, CursorMode.Auto);
    }
}
