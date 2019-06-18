using UnityEngine;
using NUnit.Framework;
using System.Collections.Generic;

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
        _gameSetup.StartingPos = _startPos;
        _parent = _gameSetup.transform;
        _gameSetup.Parent = _parent;
        _gameSetup.CardRows = _rows;
        _gameSetup.CardColumns = _columns;
        _gameSetup.Offset = _offset;
        GameObject block = TestUtil.GenerateBlockPrefab(_rows, _columns);
        _gameSetup.Card = block;
        _gameSetup.CreateGameArea(); 
        _gameSetup.Awake();
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

    [Test]
    public void GameSetupCheckReCreateBlocksAreDeleted()
    {
        List<MemoryBlock> blocks = _gameSetup.Blocks;
        _gameSetup.ReCreateGameArea();
        foreach (var block in blocks)
        {
            Assert.IsTrue(block == null);
        }
    }

    [Test]
    public void GameSetupCheckReCreateNewBlocksAreGenerated()
    {
        _gameSetup.ReCreateGameArea();
        List<MemoryBlock> blocks = _gameSetup.Blocks;
        
        foreach (var block in blocks)
        {
            Assert.IsTrue(block != null);
        }
    }

    [Test]
    public void GameSetupMatchingIndex()
    {
        List<MemoryBlock> blocks = _gameSetup.Blocks;
        // We have overall rows * columns items
        // there are always pairs of 2
        // so our maximum amount of pairs = (_rows * _columns) / 2
        int[] matchingIndexesCount = new int[(_rows * _columns) / 2];
        // Loop over all blocks and get amount of different indexes
        foreach (var block in blocks)
        {
            matchingIndexesCount[block.matchIndex]++;
        }
 
        foreach (var count in matchingIndexesCount)
        {
            Assert.AreEqual(2, count);
        }
    }
}
