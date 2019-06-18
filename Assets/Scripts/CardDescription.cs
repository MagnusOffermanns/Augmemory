using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[System.Serializable]
public class CardDescription
{
    [SerializeField]
    private string _cardName;
    [SerializeField]
    private GameObject _gfx;
    [SerializeField]
    private Vector3 _customRotation;


    public string CardName { get { return _cardName; } }

    public GameObject GFX { get { return _gfx; } }

    public Vector3 CustomRotation{ get { return _customRotation; } }
}

