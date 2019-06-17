using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MemoryBlock : MonoBehaviour
{

    public int matchIndex;
    public Color wrongColor = Color.red;
    public Color correctColor = Color.green;
    public Color defaultColor;
    public Renderer rd;
    public bool active;
    private Animator anim;

    public GameObject[] objectList;
    public GameObject memoryObject;
    public Vector3[] customRotation;

    public string[] nameList;
    public TextMeshProUGUI nameTag;

    //public Sprite[] pictureList;
    //public Image img;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        defaultColor = rd.material.color;
        active = true;
    }

    public void setMatchIndex(int i)
    {
        matchIndex = i;
        GameObject o =  Instantiate(objectList[i], memoryObject.transform.position, Quaternion.identity);

        o.transform.parent = memoryObject.transform;

        o.transform.localScale = new Vector3(1, 1, 1);
        o.transform.eulerAngles = new Vector3(
            o.transform.eulerAngles.x + customRotation[i].x,
            o.transform.eulerAngles.y + customRotation[i].y,
            o.transform.eulerAngles.z + customRotation[i].z
        );

        nameTag.text = nameList[i];
    }

    public void selected()
    {
        StartCoroutine(selectRoutine());
    }

    public void deselect()
    {
        StartCoroutine(deselcetRoutine());       
    }

    public void destroy()
    {
        StartCoroutine(destroyRoutine());
    }

    public void setColor(Color c)
    {
        rd.material.color = c;
    }

    public void resetColor()
    {
        rd.material.color = defaultColor;
    }

    IEnumerator deselcetRoutine()
    {
        yield return new WaitForSecondsRealtime(1f);
        setColor(wrongColor);
        yield return new WaitForSecondsRealtime(0.5f);
        anim.SetTrigger("hide");
        resetColor();
        active = true;
        yield return new WaitForSecondsRealtime(0.5f);
    }

    IEnumerator selectRoutine()
    {
        active = false;
        anim.SetTrigger("show");
        yield return null;
    }

    IEnumerator destroyRoutine()
    {
        yield return new WaitForSecondsRealtime(1f);
        setColor(correctColor);
        yield return new WaitForSecondsRealtime(1f);
        Destroy(gameObject);
        yield return null;
    }
}
