using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using TMPro;

public class MemoryGameHandler : MonoBehaviour
{
    public MemoryBlock selected1;
    public MemoryBlock selected2;

    public int turns;
    public int matchedPairs;

    public TextMeshProUGUI statText;
    public UnityEvent onGameOver;

    public AudioSource victoryTune;
    public AudioSource playTune;

    private static MemoryGameHandler _instance;


    public static MemoryGameHandler Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        _instance = this;
    }
    public void setNextBlock(MemoryBlock clickedObj)
    {
       
                if (clickedObj.active)
                {

                    if (selected1 == null)
                    {
                        selected1 = clickedObj;
                        selected1.selected();
                    }
                    else if (selected2 == null)
                    {
                        turns++;
                        
                        selected2 = clickedObj;
                        selected2.selected();
                        if (selected1.matchIndex == selected2.matchIndex)
                        {
                            //print("Match pair: " + selected1.matchIndex);
                            StartCoroutine(deleteBlocks());
                            matchedPairs++;
                            
                        }
                        else
                        {
                            StartCoroutine(resetBlocks());
                        }
                        UpdateUi();
                    }
                }
    }


    private void UpdateUi()
    {
        statText.text = "Turns: " + turns + "\nMatches: " + matchedPairs;
    }


    IEnumerator deleteBlocks()
    {
        MemoryGameSetup.Instance.Blocks.Remove(selected1);
        MemoryGameSetup.Instance.Blocks.Remove(selected2);
        selected1.destroy();
        selected2.destroy();
        yield return new WaitForSecondsRealtime(1f);
        selected1 = null;
        selected2 = null;
        if(MemoryGameSetup.Instance.Blocks.Count == 0)
        {
            onGameOver.Invoke();
            playTune.Stop();
            victoryTune.Play();
        }

    }

    IEnumerator resetBlocks()
    {
        selected1.deselect();
        selected2.deselect();
        yield return new WaitForSecondsRealtime(1f);
        selected1 = null;
        selected2 = null;
    }

    void resetAudio()
    {
        if (victoryTune.isPlaying)
        {
            victoryTune.Stop();
        }

        if (!playTune.isPlaying)
        {
            playTune.Play();
        }

    } 

    public void restart()
    {
        turns = 0;
        matchedPairs = 0;
        selected1 = null;
        selected2 = null;
        UpdateUi();
        resetAudio();
        MemoryGameSetup.Instance.RestartGame();

        
        
    }
}
