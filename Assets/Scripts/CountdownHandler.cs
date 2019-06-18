using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownHandler : MonoBehaviour {

    [SerializeField]
    private TextMeshProUGUI _text;
    [SerializeField]
    private string _prefixText = "";
    [SerializeField]
    private string _suffixText = "";

    public void OpenPreview(float time)
    {
        gameObject.SetActive(true);
        _text.text = _prefixText + time.ToString() + _suffixText;
    }

    public void UpdatePreview(float time)
    {
        _text.text = _prefixText + time.ToString() + _suffixText;
    }


    public void ClosePreview()
    {
        gameObject.SetActive(false);
    }
}
