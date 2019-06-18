using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using HoloToolkit.Unity.InputModule;

public class MemoryBlock : MonoBehaviour, IInputClickHandler
{

    public int matchIndex; //Assigned matching index for the block. This varible handles matching pairs (if matchIndex is equal)
    public Color wrongColor = Color.red; //Material color for a wrong match
    public Color correctColor = Color.green; //Material color for a right match
    public Color defaultColor; //Default color of the material to reset the colors
    public Renderer rd; //Renderer component of the block. Used to change colors for matches/not matches
    public bool active; //Shows if this block is active. A block is not active once it has been selected by the user. This means a block can not be reselected
    private Animator anim; //Animator component of the block. Used to play animations for showing/hiding the block
    private bool move = false; //Used to decide whether a block should move backwards or not. Used to make the dissapearing effect after a matching pair has been found
    private float accelerationRate = 0.015f; //The acceleration of the block once it moves backwards. 
    private float speed = 0; //The current speed of the block when moving backwards and dissapearing

    public GameObject[] objectList; //List of all the objects that can be possibly displayed in on a block. Each object is selected with the matching index. 
    public GameObject memoryObject; //The position of the object of the block e.g. apple. 
    public Vector3[] customRotation; //The custom rotation of each single object from the object list. 

    public string[] nameList; //The name list of the objects displayed. This list and the customRotation list have to have the same 
    public TextMeshProUGUI nameTag; //Reference to the TextMesh obejct to set the current text of the object

    // Start is called before the first frame update
    /// <summary>
    /// In this function we instantiate the anim variable to the attached animator of the gameobject
    /// we get the current material color and set it as a default color and we set the active mode to true which means
    /// the object can be selected.
    /// </summary>
    void Start()
    {
        anim = GetComponent<Animator>();
        defaultColor = rd.material.color;
        active = true;
    }
    /// <summary>
    /// We wait to receive the move trigger which is set when an object should be destroyed. 
    /// Once we can move we increase the speed over time each update and move the object to the back.
    /// </summary>
    void Update() { //update function only used to animate that the Memoryblocks fly away when two correct matches are found
        if (move) { // when the move flag is true the Memory block starts to accelerate
            speed = speed + accelerationRate; //In this line the speed in this loop is calculated
            this.transform.Translate(new Vector3(0, 0, speed)); // here the this Memoryblock is Translated into z direction (normally start position of the camera)

        }
    }
    /// <summary>
    ///This function is used to set the matchindex to an object and with that to
    ///also update all the necessary information to it. This necessary information would be to 
    ///instanciate the matching object to the matchIndex, to rotate the object with its custom rotation
    ///and to set the name to the current block
    /// </summary>
    /// <param name="i"></param>
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

    /// <summary>
    /// Starts the current selected coroutine
    /// </summary>
    public void selected()
    {
        StartCoroutine(selectRoutine());
    }
    /// <summary>
    /// Starts the current deselected coroutine
    /// </summary>
    public void deselect()
    {
        StartCoroutine(deselcetRoutine());       
    }
    /// <summary>
    /// Starts the current destroy coroutine
    /// </summary>
    public void destroy()
    {
        StartCoroutine(destroyRoutine());
    }
    /// <summary>
    /// Is used to set thhe color c to the selected renderer of the object.
    /// </summary>
    /// <param name="c"></param>
    public void setColor(Color c)
    {
        rd.material.color = c;
    }
    /// <summary>
    /// Resets the color of the selected renderer component to the default color.
    /// </summary>
    public void resetColor()
    {
        rd.material.color = defaultColor;
    }
    /// <summary>
    ///The coroutine times the deselection evenets and effects of the block.
    ///This means it waits for 1 second, sets the color to not matching, waits another while
    ///and then starts the hide animation from the animator. Also it sets active to true which means
    ///the block can be activated again
    /// </summary>
    /// <returns></returns>
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
    /// <summary>
    ///This coroutine sets the active to false, which means we can not select the object anymore.
    ///It also plays the animation to show the object. The reason why this is a coroutine is to 
    ///later add timed events to selecting objects.
    /// </summary>
    /// <returns></returns>
    IEnumerator selectRoutine()
    {
        active = false;
        anim.SetTrigger("show");
        yield return null;
    }
    /// <summary>
    ///Coroutine to destroy (match was correct) a card. Similar to the deselect routine we
    ///wait a second set the color to the matchign correct color and set the move variable to true which 
    ///starts moving the object to the back.
    /// </summary>
    /// <returns></returns>
    IEnumerator destroyRoutine()
    {
        yield return new WaitForSecondsRealtime(1f);
        setColor(correctColor);
        move = true; //starts the movement of this Memory block
        yield return new WaitForSecondsRealtime(1f);
        speed = 0; // set the speed back to zero and stop the movement thereby
        move = false; // toggle the movement flag
        Destroy(gameObject);
        yield return null;
    }
    /// <summary>
    /// In.built HoloLense function to handle click events on the object.
    /// The function accesses the MemoryGameHandler and sets itself as the next block.
    /// From there everything is handled by the handler.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnInputClicked(InputClickedEventData eventData)
    {
        MemoryGameHandler.Instance.setNextBlock(this);
    }
}
