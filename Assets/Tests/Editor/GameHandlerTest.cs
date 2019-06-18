using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections.Generic;

public class GameHandlerTest {

    private int _rows = 3;
    private int _columns = 4;
    private float _offset = 1f;
    private Transform _startPos;
    private Transform _parent;

    private MemoryGameSetup _gameSetup;
    private MemoryGameHandler _gameHandler;


    [SetUp]
    public void Init()
    {

        InitGameSetup();
        InitGameHandler();
    }


    private void InitGameSetup()
    {
        _gameSetup = new GameObject().AddComponent<MemoryGameSetup>();
        _startPos = _gameSetup.transform;
        _gameSetup.startingPos = _startPos;
        _parent = _gameSetup.transform;
        _gameSetup.parent = _parent;
        _gameSetup.cardRows = _rows;
        _gameSetup.cardColumns = _columns;
        _gameSetup.offset = _offset;
        GameObject block = TestUtil.GenerateBlockPrefab(_rows, _columns);
        _gameSetup.card = block;
        _gameSetup.CreateGameArea();
        _gameSetup.Awake();
    }

    private void InitGameHandler()
    {
        _gameHandler = new GameObject().AddComponent<MemoryGameHandler>();
        _gameHandler.Awake();
        _gameHandler.statText = new GameObject().AddComponent<TMPro.TextMeshProUGUI>();

    }

    [Test]
	public void GameHandlerTestIsNotRunning() {
        Assert.IsFalse(_gameHandler.IsRunning);
	}


    [Test]
    public void GameHandlerTestIsRunning()
    {
        _gameHandler.restart();
        Assert.IsTrue(_gameHandler.IsRunning);
    }

    [Test]
    public void GameHandlerTestBlockSelected()
    {
        _gameHandler.restart();
        var block = _gameSetup.Blocks[0];
        _gameHandler.setNextBlock(block);
        Assert.AreEqual(block, _gameHandler.selected1);
    }

    [Test]
    public void GameHandlerTestTwoBlocksSelected()
    {
        _gameHandler.restart();
        var block1 = _gameSetup.Blocks[0];
        var block2 = _gameSetup.Blocks[1];
        _gameHandler.setNextBlock(block1);
        _gameHandler.setNextBlock(block2);
        Assert.AreEqual(block1, _gameHandler.selected1);
        Assert.AreEqual(block2, _gameHandler.selected2);
    }

    [Test]
    public void GameHandlerTestSelectTheSameBlockTwice()
    {
        _gameHandler.restart();
        var block1 = _gameSetup.Blocks[0];
        _gameHandler.setNextBlock(block1);
        _gameHandler.setNextBlock(block1);
        Assert.AreEqual(block1, _gameHandler.selected1);
        Assert.IsNull(_gameHandler.selected2);
    }

    [Test]
    public void GameHandlerTestSelectNotRunning()
    {
        var block1 = _gameSetup.Blocks[0];
        _gameHandler.setNextBlock(block1);
        Assert.IsNull(_gameHandler.selected1);
    }

    [Test]
    public void GameHandlerFinishGame()
    {
        _gameHandler.restart();
        int pairsCount = (_rows * _columns) / 2;
        List<MemoryBlock>[] pairs = new List<MemoryBlock>[pairsCount];
        var blocks = _gameSetup.Blocks;
        for (int j = 0; j < pairsCount; j++)
        {
            pairs[j] = new List<MemoryBlock>();
        }

        foreach (var block in blocks)
        {
            int currentBlockAmount = pairs[block.matchIndex].Count;
            if(currentBlockAmount >= 2)
            {
                throw new System.Exception("A pair consists only of 2 blocks");
            }
            pairs[block.matchIndex].Add(block);
        }
        for (int i = 0; i < pairsCount; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                _gameHandler.setNextBlock(pairs[i][j]);
            }
        }
        Assert.AreEqual(pairsCount, _gameHandler.matchedPairs);
        Assert.AreEqual(0, _gameSetup.Blocks.Count);
    }
}
