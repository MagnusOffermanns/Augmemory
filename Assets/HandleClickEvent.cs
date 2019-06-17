using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class HandleClickEvent : MonoBehaviour, IInputClickHandler
{
    public UnityEngine.Events.UnityEvent onButtonClicked;


    public void OnInputClicked(InputClickedEventData eventData)
    {
        onButtonClicked.Invoke();
    }
}
