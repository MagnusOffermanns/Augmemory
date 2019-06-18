using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using TMPro;

public class MemoryGameHandler : MonoBehaviour
{
    public MemoryBlock selected1; /// First selected MemoryBlock
    public MemoryBlock selected2; /// Second selected MemoryBlock

    public int turns; /// Amount of turns absolved by the player
    public int matchedPairs; /// Amount of matched pairs found by the player

    public TextMeshProUGUI statText; /// Reference to the statText UI element used to display statistics 
    public UnityEvent onGameOver; /// Reference to the event when a game is over

    public AudioSource victoryTune; /// Reference to the victoryTune
    public AudioSource playTune; /// Reference to the tune played when matching pair has been found

    private static MemoryGameHandler _instance; //Singleton pattern variable for the MemoryGameHandler

    /// <summary>
    /// This function is used to set up the singleton pattern for the MemoryGameHandler
    /// </summary>
    public static MemoryGameHandler Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        _instance = this;
    }

    /// <summary>
    /// This function is accessed by memoryblocks to set themselves
    /// as part of the current selected memory blocks. If two blocks are selected
    /// we increase turns by one, check if the matching index is the same for both of the selected blcoks
    /// and if they are we increase matchedPairs by one and delete the blocks. If the pair is not matching, we hide both blocks again.
    /// </summary>
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

    /// <summary>
    /// This function updates the UI for turns and matches and prints the local variables 
    /// </summary>
    private void UpdateUi()
    {
        statText.text = "Turns: " + turns + "\nMatches: " + matchedPairs;
    }

    /// <summary>
    /// This function resets the current two selected blocks and destroys them. We also remove them
    /// from the existing blocks list. After 1 second we set the variables to null so we can select two new blocks
    /// and play a tune for having found a matching pair. 
    /// </summary>
    IEnumerator deleteBlocks()
    {
        MemoryGameSetup.Instance.Blocks.Remove(selected1);
        MemoryGameSetup.Instance.Blocks.Remove(selected2);
        selected1.destroy();
        selected2.destroy();
        yield return new WaitForSecondsRealtime(1f);
        selected1 = null;
        selected2 = null;
        if (MemoryGameSetup.Instance.Blocks.Count == 0)
        {
            onGameOver.Invoke();
            playTune.Stop();
            victoryTune.Play();
        }

    }

    /// <summary>
    /// This function resets / deselects the current two selected blocks and clears the variables
    /// in order to make the selection of two new blocks possible.
    /// </summary>
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

    /// <summary>
    /// This function restarts the game by calling the Restart function in the MemoryGameSetup class.
    /// It also resets values for turns and matched pairs, updates the UI accordingly and resets the audio players and files. 
    /// </summary>
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
