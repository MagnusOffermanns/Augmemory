using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Purpose of this component is to check if an object with the Component HoverObject is in the middle of the selection.
/// It switches the layer of the object that is hit by a raycast that is outgoing from the camera middlepoint
/// </summary>
public class OutlineManager : MonoBehaviour {

    // Reference to the current main camera
    [SerializeField]
    private Camera _camera;

    // current Target
    private Collider _currentTarget;
	
	// Update is called once per frame
	void Update () {
        // create a craycast from the current middle point of the camera
        Ray raycastHit = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit info;
        // check if we hit something
        if(Physics.Raycast(raycastHit, out info))
        {
            // check if the found target has also the HoverObject component attached
            if(info.collider.GetComponent<HoverObject>())
            {
                // if the current target is not the target found by the raycast then select a new target
                if (_currentTarget != info.collider)
                {
                    // Check if there is a current target
                    if (_currentTarget != null)
                    {
                        // Deselect the current target and put it onto its original layer
                        _currentTarget.GetComponent<HoverObject>().ResetSelection();
                    }
                    // Select the new target and save its value into the field _currentTarget
                    var newTarget = info.collider.GetComponent<HoverObject>();
                    newTarget.Select();
                    _currentTarget = info.collider;
                }
            }
        }
        // if we did not hit anything. Check if there is currently an object selected 
        else if (_currentTarget != null)
        {
            // Deselect the current target and put it onto its original layer
            _currentTarget.GetComponent<HoverObject>().ResetSelection();
            _currentTarget = null;
        }

	}
}
