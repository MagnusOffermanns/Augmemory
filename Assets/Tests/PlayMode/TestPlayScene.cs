using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;

public class TestPlayScene {


    private MemoryGameSetup _gameSetup;
    private MemoryGameHandler _gameHandler;
    private PlayModeTestHelper _testHelper;
    private Camera _camera;


    [SetUp]
    public void Init()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    private void SetFields()
    {
        _gameSetup = MemoryGameSetup.Instance;
        _gameHandler = MemoryGameHandler.Instance;
        _testHelper = PlayModeTestHelper.Instance;
        _camera = Camera.main;
    }

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator TestFinishGame() {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        yield return new WaitForSeconds(1f);
        SetFields();
        
        _gameHandler.Restart();
        _gameHandler.PostGameStart();
        var blocks = _gameSetup.Blocks;
        int pairsCount = blocks.Count / 2;
        List<MemoryBlock>[] pairs = new List<MemoryBlock>[pairsCount];
        for (int j = 0; j < pairsCount; j++)
        {
            pairs[j] = new List<MemoryBlock>();
        }

        foreach (var block in blocks)
        {
            int currentBlockAmount = pairs[block.matchIndex].Count;
            if (currentBlockAmount >= 2)
            {
                throw new System.Exception("A pair consists only of 2 blocks");
            }
            pairs[block.matchIndex].Add(block);
        }
        yield return new WaitForSeconds(1f);
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


    [UnityTest]
    public IEnumerator TestStartGame()
    {
        yield return new WaitForSeconds(1f);
        SetFields();
        yield return new WaitForSeconds(1f);
        var mainMenuButton = _testHelper.Objects[0].GetComponent<HandleClickEvent>();
        mainMenuButton.OnInputClicked(null);
        yield return new WaitForSeconds(1f);
        var blocks = _gameSetup.Blocks;
        Assert.AreEqual(_gameSetup.CardRows * _gameSetup.CardColumns, _gameSetup.Blocks.Count);
    }

    [UnityTest]
    public IEnumerator TestCloseGame()
    {
        yield return new WaitForSeconds(1f);
        SetFields();
        yield return new WaitForSeconds(1f);
        var mainMenuButton = _testHelper.Objects[0].GetComponent<HandleClickEvent>();
        mainMenuButton.OnInputClicked(null);
        yield return new WaitForSeconds(1f);
        var blocks = _gameSetup.Blocks;
        var backButton = _testHelper.Objects[1].GetComponent<HandleClickEvent>();
        backButton.OnInputClicked(null);
        yield return new WaitForSeconds(1f);
        Assert.AreEqual(0, _gameSetup.Blocks.Count);
        Assert.IsTrue(mainMenuButton.gameObject.activeInHierarchy);
    }
}
