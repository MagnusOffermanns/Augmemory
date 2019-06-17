using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class ResetScript : MonoBehaviour, IInputClickHandler {

    public void OnInputClicked(InputClickedEventData eventData)
    {
        MemoryGameHandler.Instance.restart();
        //GameObject.FindGameObjectWithTag("MemoryGameHandler").GetComponent<MemoryGameHandler>().restart();
        Debug.Log("clicked");
    }
}
