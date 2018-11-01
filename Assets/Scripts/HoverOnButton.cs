using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // Required when using Event data. Use this to detect mouseover, etc. on UI elements.
using UnityEngine.UI;

public class HoverOnButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler // Required for OnPointer methods
{
    public Image HoverImage;
    public Image MouseDownImage;

    void Awake()
    {
        HoverImage.enabled = false;
        MouseDownImage.enabled = false;
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        HoverImage.enabled = true;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        HoverImage.enabled = false;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        MouseDownImage.enabled = true;
        HoverImage.enabled = false;
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        MouseDownImage.enabled = false;
    }
}
