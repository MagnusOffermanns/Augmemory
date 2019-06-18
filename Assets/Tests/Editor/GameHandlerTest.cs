using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class GameHandlerTest {

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
	public void GameHandlerTestSimplePasses() {
		// Use the Assert class to test conditions.
	}

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator GameHandlerTestWithEnumeratorPasses() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		yield return null;
	}
}
