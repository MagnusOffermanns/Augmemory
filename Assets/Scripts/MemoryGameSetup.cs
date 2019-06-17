using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGameSetup : MonoBehaviour
{
    public Transform startingPos;
    public int cardRows;
    public int cardColumns;
    public GameObject card;
    public float offset = 100f;
    public Transform parent;

    public List<MemoryBlock> blocksWithOutNumbers;
    
    // Start is called before the first frame update
    void Start()
    {
        Vector3 currPos = startingPos.position;
        for(int i = 0; i < cardColumns; i++)
        {
            for(int j = 0; j < cardRows; j++)
            {
                GameObject obj = Instantiate(card, currPos, Quaternion.identity);
                obj.transform.parent = parent;
                blocksWithOutNumbers.Add(obj.GetComponent<MemoryBlock>());
                currPos.x += offset;
            }
            currPos.x = startingPos.position.x;
            currPos.y += offset;
        }


        //GIVE RANDOM INDEX TO BLOCK
        int currentIndex = -1;
        int matchingBlocks = 2;
        int blockNum = blocksWithOutNumbers.Count;

        for (int i = 0; i < blockNum; i++)
        {
            if(i % matchingBlocks == 0)
            {
                currentIndex++;
            }
            int blockToModify = Random.Range(0, blocksWithOutNumbers.Count);
            blocksWithOutNumbers[blockToModify].setMatchIndex(currentIndex);
            blocksWithOutNumbers.RemoveAt(blockToModify);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
