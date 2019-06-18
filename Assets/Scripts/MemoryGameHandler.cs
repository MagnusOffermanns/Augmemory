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

    public AudioSource playTune; //Is a audiosource which plays the music during the current game
    public AudioSource victoryTune; //Is a audiosource which plays the music when all blocks are matched i.e. victory is achieved

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
            playTune.Stop(); // stops the replay of the music that is played during the game
            victoryTune.Play(); // starts the replay of the tune that is played when all blocks are matched -> victory is achieved
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

    void resetAudio() // is used to reset the Audiosources
    {
        if (victoryTune.isPlaying) // stops the victory tune if it is playing
        {
            victoryTune.Stop();
        }

        if (!playTune.isPlaying) // starts the audio during normal play if it is not already playing
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
        resetAudio();  //resets the audioSources
        MemoryGameSetup.Instance.RestartGame();

        
        
    }
}
