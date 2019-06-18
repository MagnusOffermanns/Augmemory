using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
/// <summary>
/// Class for handling hololens onclick event and redirect it to a unity event so that the action can be send in the editor
/// </summary>
public class HandleClickEvent : MonoBehaviour, IInputClickHandler
{
    public UnityEngine.Events.UnityEvent onButtonClicked;


    public void OnInputClicked(InputClickedEventData eventData)
    {
        onButtonClicked.Invoke();
    }
}
