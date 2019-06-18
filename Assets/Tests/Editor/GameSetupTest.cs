using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class GameSetupTest {

    private int _rows = 3;
    private int _columns = 4;
    private float _offset = 1f;
    private Transform _startPos;
    private Transform _parent;

    private MemoryGameSetup _gameSetup;

    [SetUp]
    public void Init()
    {

        _gameSetup = new GameObject().AddComponent<MemoryGameSetup>();
        _startPos = _gameSetup.transform;
        _gameSetup.startingPos = _startPos;
        _parent = _gameSetup.transform;
        _gameSetup.parent = _parent;
        _gameSetup.cardRows = _rows;
        _gameSetup.cardColumns = _columns;
        _gameSetup.offset = _offset;
        GameObject block = GenerateBlockPrefab();
        _gameSetup.card = block;
        _gameSetup.StartGame();
    }

    private GameObject GenerateBlockPrefab()
    {
        int items = (_rows * _columns) / 2;
        GameObject[] blockList = new GameObject[items];
        string[] blockNames = new string[items];
        Vector3[] customRotations = new Vector3[items];
        for (int i = 0; i < items; i++)
        {
            blockList[i] = new GameObject();
            blockNames[i] = "x";
        }


        GameObject block = new GameObject();
        var mBlock = block.AddComponent<MemoryBlock>();
        mBlock.objectList = blockList;
        mBlock.memoryObject = mBlock.gameObject;
        mBlock.customRotation = customRotations;
        mBlock.nameList = blockNames;
        mBlock.nameTag = new GameObject().AddComponent<TMPro.TextMeshProUGUI>();


        return block;
    }

	[Test]
	public void GameSetupCheckBlockCount() {
        Assert.AreEqual(_rows * _columns, _gameSetup.Blocks.Count);
	}

    [Test]
    public void GameSetupCheckPositionOfBlocks()
    {
        for (int y = 0; y < _columns; y++)
        {
            for (int x = 0; x < _rows; x++)
            {
                Vector3 position = new Vector3(_startPos.position.x + _offset * x, _startPos.position.y +  _offset  * y, _startPos.position.z );

                Assert.AreEqual(position, _gameSetup.Blocks[_rows * y + x].transform.position);
            }
        }
    }

    [Test]
    public void GameSetupCheckBlocksParent()
    {
        for (int y = 0; y < _columns; y++)
        {
            for (int x = 0; x < _rows; x++)
            {

                Assert.AreEqual(_parent, _gameSetup.Blocks[_rows * y + x].transform.parent);
            }
        }
    }
}
