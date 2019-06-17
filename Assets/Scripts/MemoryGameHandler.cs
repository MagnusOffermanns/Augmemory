using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MemoryGameHandler : MonoBehaviour
{
    public MemoryBlock selected1;
    public MemoryBlock selected2;

    public int turns;
    public int matchedPairs;

    public TextMeshProUGUI matchesUI;
    public TextMeshProUGUI turnsUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObj = hit.transform.gameObject;
                if (clickedObj.tag == "Block" && clickedObj.GetComponent<MemoryBlock>().active)
                {

                    if (selected1 == null)
                    {
                        selected1 = clickedObj.GetComponent<MemoryBlock>();
                        selected1.selected();
                    }
                    else if(selected2 == null)
                    {
                        turns++;
                        turnsUI.text = "Turns: " + turns;
                        selected2 = clickedObj.GetComponent<MemoryBlock>();
                        selected2.selected();
                        if (selected1.matchIndex == selected2.matchIndex)
                        {
                            //print("Match pair: " + selected1.matchIndex);
                            StartCoroutine(deleteBlocks());
                            matchedPairs++;
                            matchesUI.text = "Matches: " + matchedPairs;
                        }
                        else
                        {
                            StartCoroutine(resetBlocks());
                        }
                    }
                }
            }
            else
            {
                Debug.Log("No hit");
            }
        }
    }

    public void setNextBlock(GameObject clickedObj)
    {
       
                if (clickedObj.tag == "Block" && clickedObj.GetComponent<MemoryBlock>().active)
                {

                    if (selected1 == null)
                    {
                        selected1 = clickedObj.GetComponent<MemoryBlock>();
                        selected1.selected();
                    }
                    else if (selected2 == null)
                    {
                        turns++;
                        turnsUI.text = "Turns: " + turns;
                        selected2 = clickedObj.GetComponent<MemoryBlock>();
                        selected2.selected();
                        if (selected1.matchIndex == selected2.matchIndex)
                        {
                            //print("Match pair: " + selected1.matchIndex);
                            StartCoroutine(deleteBlocks());
                            matchedPairs++;
                            matchesUI.text = "Matches: " + matchedPairs;
                        }
                        else
                        {
                            StartCoroutine(resetBlocks());
                        }
                    }
                }
    }


    IEnumerator deleteBlocks()
    {
        selected1.destroy();
        selected2.destroy();
        yield return new WaitForSecondsRealtime(1f);
        selected1 = null;
        selected2 = null;
    }

    IEnumerator resetBlocks()
    {
        selected1.deselect();
        selected2.deselect();
        yield return new WaitForSecondsRealtime(1f);
        selected1 = null;
        selected2 = null;
    }

    public void restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
