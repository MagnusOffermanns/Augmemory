using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using HoloToolkit.Unity.InputModule;

public class TestPlayScene {


    [SetUp]
    public void Init()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator TestPlaySceneWithEnumeratorPasses() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		yield return new WaitForSeconds(1f);
        //HoloToolkit.Unity.
	}
}
