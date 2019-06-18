using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class HoverObject : MonoBehaviour
{
    private int _layerMask;

    private int _selectionLayerMask;

    private void Awake()
    {
        _layerMask = gameObject.layer;
        _selectionLayerMask = LayerMask.NameToLayer("Selection");
    }

    public void Select()
    {
        SetLayerMaskRecursive(gameObject, _selectionLayerMask);
    }

    public void ResetSelection()
    {
        SetLayerMaskRecursive(gameObject, _layerMask);
    }

    private void SetLayerMaskRecursive(GameObject target, int layerMask)
    {
        target.layer = layerMask;
        for (int i = 0; i < target.transform.childCount; i++)
        {
            var child = target.transform.GetChild(i);
            SetLayerMaskRecursive(child.gameObject, layerMask);
        }
    }
	
	
}
