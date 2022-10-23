using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Button_Speech : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent OnButtonPressed;
    public UnityEvent OnButtonRealeased;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnButtonPressed?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnButtonRealeased?.Invoke();
    }
}
