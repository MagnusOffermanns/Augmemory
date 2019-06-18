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

    private void InitGameHandler()
    {
        _gameHandler = new GameObject().AddComponent<MemoryGameHandler>();
        _gameHandler.Awake();
        _gameHandler.StatText = new GameObject().AddComponent<TMPro.TextMeshProUGUI>();

    }

    [Test]
	public void GameHandlerTestIsNotRunning() {
        Assert.IsFalse(_gameHandler.IsRunning);
	}


    [Test]
    public void GameHandlerTestIsRunning()
    {
        _gameHandler.Restart();
        Assert.IsTrue(_gameHandler.IsRunning);
    }

    [Test]
    public void GameHandlerTestBlockSelected()
    {
        _gameHandler.Restart();
        var block = _gameSetup.Blocks[0];
        _gameHandler.SetNextBlock(block);
        Assert.AreEqual(block, _gameHandler.Selected1);
    }

    [Test]
    public void GameHandlerTestTwoBlocksSelected()
    {
        _gameHandler.Restart();
        var block1 = _gameSetup.Blocks[0];
        var block2 = _gameSetup.Blocks[1];
        _gameHandler.SetNextBlock(block1);
        _gameHandler.SetNextBlock(block2);
        Assert.AreEqual(block1, _gameHandler.Selected1);
        Assert.AreEqual(block2, _gameHandler.Selected2);
    }

    [Test]
    public void GameHandlerTestSelectTheSameBlockTwice()
    {
        _gameHandler.Restart();
        var block1 = _gameSetup.Blocks[0];
        _gameHandler.SetNextBlock(block1);
        _gameHandler.SetNextBlock(block1);
        Assert.AreEqual(block1, _gameHandler.Selected1);
        Assert.IsNull(_gameHandler.Selected2);
    }

    [Test]
    public void GameHandlerTestSelectNotRunning()
    {
        var block1 = _gameSetup.Blocks[0];
        _gameHandler.SetNextBlock(block1);
        Assert.IsNull(_gameHandler.Selected1);
    }

    [Test]
    public void GameHandlerTestDoubleStartBehaviour()
    {
        _gameHandler.Restart();
        _gameHandler.Countdown = 1f;
        Assert.IsTrue(_gameHandler.IsRunning);
        _gameHandler.Restart();

        Assert.IsFalse(_gameHandler.IsRunning);
    }

    [Test]
    public void GameHandlerFinishGame()
    {
        _gameHandler.Restart();
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
                _gameHandler.SetNextBlock(pairs[i][j]);
            }
        }
        Assert.AreEqual(pairsCount, _gameHandler.MatchedPairs);
        Assert.AreEqual(0, _gameSetup.Blocks.Count);
    }
}
