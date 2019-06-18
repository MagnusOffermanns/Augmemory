using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGameSetup : MonoBehaviour
{
    [SerializeField]
    private Transform _startingPos; // lower Left Corner position of the playfield
    [SerializeField]
    private int _cardRows; // How much rows of cards are there
    [SerializeField]
    private int _cardColumns; // How much columns are there
    [SerializeField]
    private GameObject _card; // Prefab of the MemoryBlock GameObject
    [SerializeField]
    private float _offset = 100f; // Distance between blocks transform positions
    [SerializeField]
    private Transform _parent; // Parenttransform for the blocks

    private List<MemoryBlock> _blocks;

    private static MemoryGameSetup _instance;

    public Transform StartingPos
    {
        get { return _startingPos; }
        set { _startingPos = value; }
    }

    public int CardRows
    {
        get { return _cardRows; }
        set { _cardRows = value; }
    }

    public int CardColumns
    {
        get { return _cardColumns; }
        set { _cardColumns = value; }
    }

    public GameObject Card
    {
        get { return _card; }
        set { _card = value; }
    }

    public float Offset
    {
        get { return _offset; }
        set { _offset = value; }
    }

    public Transform Parent
    {
        get { return _parent; }
        set { _parent = value; }
    }

    public static MemoryGameSetup Instance
    {
        get { return _instance; }
    }

    public List<MemoryBlock> Blocks
    {
        get { return _blocks; }
    }

   


    public void Awake()
    {
        _instance = this;
    }

    /// <summary>
    /// Creates the memory blocks and assigned random values to them
    /// </summary>
    public void CreateGameArea()
    {
        ClearPlayField();
        _blocks = new List<MemoryBlock>();
        Vector3 currPos = _startingPos.position;
        List<MemoryBlock> blocksWithOutNumbers = new List<MemoryBlock>();
        for (int i = 0; i < _cardColumns; i++)
        {
            for (int j = 0; j < _cardRows; j++)
            {
                GameObject obj = Instantiate(_card, currPos, Quaternion.identity);
                obj.transform.parent = _parent;
                var memBlock = obj.GetComponent<MemoryBlock>();
                blocksWithOutNumbers.Add(memBlock);
                currPos.x += _offset;
                _blocks.Add(memBlock);
            }
            currPos.x = _startingPos.position.x;
            currPos.y += _offset;
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

    /// <summary>
    /// Internal call for clearing the playField
    /// </summary>
    private void ClearPlayField()
    {
        if (_blocks != null)
        {
            foreach (var block in _blocks)
            {

                if (block != null)
                {
#if UNITY_EDITOR
                    // Special case to remove elements in editmode because of the editmode testscript

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
    /// <summary>
    /// Recreates the playfield. Current block are getting removed
    /// </summary>
    public void ReCreateGameArea()
    {
        CreateGameArea();
    }
    /// <summary>
    /// Removes all blocks on the playfield
    /// </summary>
    public void ClearGameArea()
    {
        ClearPlayField();
    }
}
