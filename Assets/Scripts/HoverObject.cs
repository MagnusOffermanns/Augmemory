using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class HoverObject : MonoBehaviour
{
    private int _layerMask; // the layer mask of the gameobject this component is attached to

    private int _selectionLayerMask; // the layer mask for the Selection Layer

    
    private void Awake()
    {
        // Set layer Masks
        _layerMask = gameObject.layer;
        _selectionLayerMask = LayerMask.NameToLayer("Selection");
    }

    /// <summary>
    /// Update the layer of this object to Selection
    /// </summary>
    public void Select()
    {
        SetLayerMaskRecursive(gameObject, _selectionLayerMask);
    }

    /// <summary>
    /// Move the object back to its original layer
    /// </summary>
    public void ResetSelection()
    {
        SetLayerMaskRecursive(gameObject, _layerMask);
    }

    /// <summary>
    /// Apply the layer for target and all of his childrens
    /// </summary>
    /// <param name="target"></param>
    /// <param name="layerMask"></param>
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
