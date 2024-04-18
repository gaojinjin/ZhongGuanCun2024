using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RecordEvent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public MainManager mainManager;

    public void OnPointerDown(PointerEventData eventData)
    {
        mainManager.HasBeenLongPressed = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mainManager.HasBeenLongPressed = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        mainManager.HasBeenLongPressed = false;
    }

    
}
