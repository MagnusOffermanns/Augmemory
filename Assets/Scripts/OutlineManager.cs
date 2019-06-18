using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineManager : MonoBehaviour {


    [SerializeField]
    private Camera _camera;

    private int _targetLayerMask;
    private int _selectionLayerMask;

    private Collider _currentTarget;
	
	// Update is called once per frame
	void Update () {

        Ray raycastHit = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit info;
        if(Physics.Raycast(raycastHit, out info))
        {
            if(info.collider.GetComponent<HoverObject>())
            {
                if (_currentTarget != info.collider)
                {
                    if (_currentTarget != null)
                    {
                        _currentTarget.GetComponent<HoverObject>().ResetSelection();
                    }

                    var newTarget = info.collider.GetComponent<HoverObject>();
                    newTarget.Select();
                    _currentTarget = info.collider;
                }
            }
        }
        else if (_currentTarget != null)
        {
            _currentTarget.GetComponent<HoverObject>().ResetSelection();
            _currentTarget = null;
        }

	}
}
