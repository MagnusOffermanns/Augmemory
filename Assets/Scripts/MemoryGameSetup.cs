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

    private List<MemoryBlock> _blocks;

    private static MemoryGameSetup _instance;

    public List<MemoryBlock> Blocks
    {
        get { return _blocks; }
    }

    public static MemoryGameSetup Instance
    {
        get { return _instance; }
    }


    public void Awake()
    {
        _instance = this;
    }

    public void CreateGameArea()
    {
        ClearPlayField();
        _blocks = new List<MemoryBlock>();
        Vector3 currPos = startingPos.position;
        List<MemoryBlock> blocksWithOutNumbers = new List<MemoryBlock>();
        for (int i = 0; i < cardColumns; i++)
        {
            for (int j = 0; j < cardRows; j++)
            {
                GameObject obj = Instantiate(card, currPos, Quaternion.identity);
                obj.transform.parent = parent;
                var memBlock = obj.GetComponent<MemoryBlock>();
                blocksWithOutNumbers.Add(memBlock);
                currPos.x += offset;
                _blocks.Add(memBlock);
#if UNITY_EDITOR
                /*if(!Application.isPlaying)
                {
                    memBlock.Start();
                }*/
#endif
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
            if (i % matchingBlocks == 0)
            {
                currentIndex++;
            }
            int blockToModify = Random.Range(0, blocksWithOutNumbers.Count);
            blocksWithOutNumbers[blockToModify].setMatchIndex(currentIndex);
            blocksWithOutNumbers.RemoveAt(blockToModify);
        }
    }

    private void ClearPlayField()
    {
        if (_blocks != null)
        {
            foreach (var block in _blocks)
            {

                if (block != null)
                {
#if UNITY_EDITOR
                    if (Application.isPlaying)
                    {
                        Destroy(block.gameObject);
                    }
                    else
                    {
                        DestroyImmediate(block.gameObject);
                    }
#else
                    Destroy(block.gameObject);
#endif
                }

            }
        }
    }

    public void ReCreateGameArea()
    {
        CreateGameArea();
    }

    public void ClearGameArea()
    {
        ClearPlayField();
    }
}
