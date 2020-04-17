using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TextTransition : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public static Color pressed = new Color32(252, 35, 10, 255), highlight = new Color32(212, 207, 191, 255), normal = new Color32(102, 102, 102, 255);

    public Text text;

    private void Start()
    {
        text.color = normal;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = highlight;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = normal;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        text.color = pressed;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        text.color = normal;
    }
}
