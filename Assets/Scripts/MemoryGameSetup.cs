using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGameSetup : MonoBehaviour
{
    public Transform startingPos;
    public int cardRows;
    public int cardColumns;
    public GameObject card;
    public GameObject resetButton;
    public float offset = 100f;
    public Transform parent;
    public AudioSource playSource; //audio source responsible to play the music while playing the game

    public List<MemoryBlock> blocksWithOutNumbers;

    private List<MemoryBlock> _blocks;

    private static MemoryGameSetup _instance;

    public List<MemoryBlock> Blocks
    {
        get { return _blocks; }
    }

    public static MemoryGameSetup Instance{
        get { return _instance; }
    }


    private void Awake()
    {
        _instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        Vector3 currPos = startingPos.position;

        playSource.Play();  // starts the audio playback of the music that is heard while playing the game  
        _blocks = new List<MemoryBlock>();
        blocksWithOutNumbers = new List<MemoryBlock>();
        for (int i = 0; i < cardColumns; i++)
        {
            for (int j = 0; j < cardRows; j++)
            {
                GameObject obj = Instantiate(card, currPos, Quaternion.identity);
                obj.transform.parent = parent;
                blocksWithOutNumbers.Add(obj.GetComponent<MemoryBlock>());
                currPos.x += offset;
                _blocks.Add(obj.GetComponent<MemoryBlock>());
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

    public void RestartGame()
    {
        foreach (var block in _blocks)
        {
            
            if(block != null)
            {
                Destroy(block.gameObject);
            }
            
        }
        _blocks = null;
        StartGame();
    }
}
