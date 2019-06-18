using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayModeTestHelper : MonoBehaviour {

    private static PlayModeTestHelper _instance;

    [SerializeField] private GameObject[] _objects;

    public static PlayModeTestHelper Instance
    {
        get { return _instance; }
    }

    public GameObject[] Objects
    {
        get { return _objects; }
    }

    private void Awake()
    {
        _instance = this;
    }

}
