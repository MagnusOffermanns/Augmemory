using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] _menus;

    private GameObject _currentMenu;

	// Use this for initialization
	void Start () {
        foreach (var menu in _menus)
        {
            menu.SetActive(false);
        }
        _currentMenu = _menus[0];
        _currentMenu.SetActive(true);
	}
	

    public void SetMenu(int index)
    {
        _currentMenu.SetActive(false);
        _currentMenu = _menus[index];
        _currentMenu.SetActive(true);
    }
}
