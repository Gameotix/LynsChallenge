using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class buttonPressed : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool buttonClicked;

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonClicked = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonClicked = false;
    }

}
