using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using TMPro;

public class MemoryGameHandler : MonoBehaviour
{
    private static MemoryGameHandler _instance; ///Singleton pattern variable for the MemoryGameHandler


    public MemoryBlock Selected1; /// First selected MemoryBlock
    public MemoryBlock Selected2; /// Second selected MemoryBlock

    public int Turns; /// Amount of turns absolved by the player
    public int MatchedPairs; /// Amount of matched pairs found by the player

    public TextMeshProUGUI StatText; /// Reference to the statText UI element used to display statistics
    public UnityEvent OnGameStarts;
    public UnityEvent OnGameOver; /// Reference to the event when a game is over

    public AudioSource VictoryTune; /// Reference to the victoryTune
    public AudioSource PlayTune; /// Reference to the tune played when matching pair has been found
    public ARGameTimer ArgTimer; /// Reference to the timer

    public float Countdown = 0f;
    public CountdownHandler CountdownHandler;
    public GameEndStatHandler GameEndHandler;

    private bool _isRunning; /// Status variable that hold the status if the game is running or not
    private bool _countDownTimerIsRunning;
    private float _cdTimeLeft;
    private int _lastIntegerTimeValue;

    

    /// <summary>
    /// This function is used to set up the singleton pattern for the MemoryGameHandler
    /// </summary>
    public static MemoryGameHandler Instance
    {
        get { return _instance; }
    }

    public bool IsRunning
    {
        get { return _isRunning; }
    }

    public void Awake()
    {
        _instance = this;
    }

    public void Update()
    {
        if(_countDownTimerIsRunning)
        {
            _cdTimeLeft -= Time.deltaTime;
            int currentIntegerTimeValue = (int)_cdTimeLeft + 1;
            if(currentIntegerTimeValue != _lastIntegerTimeValue)
            {
                _lastIntegerTimeValue = currentIntegerTimeValue;
                CountdownHandler.UpdatePreview(_lastIntegerTimeValue);
            }
            if(_cdTimeLeft < 0)
            {
                
                
                PostGameStart();
            }
        }
    }

    /// <summary>
    /// This function is accessed by memoryblocks to set themselves
    /// as part of the current selected memory blocks. If two blocks are selected
    /// we increase turns by one, check if the matching index is the same for both of the selected blcoks
    /// and if they are we increase matchedPairs by one and delete the blocks. If the pair is not matching, we hide both blocks again.
    /// </summary>
    public void SetNextBlock(MemoryBlock clickedObj)
    { 

        // Do not handle any input if the game is not running
        if(!_isRunning)
        {
            return;
        }
        if (clickedObj.active)
        {

            if (Selected1 == null)
            {
                Selected1 = clickedObj;
                Selected1.selected();
            }
            else if (Selected2 == null)
            {
                Turns++;

                Selected2 = clickedObj;
                Selected2.selected();
                if (Selected1.matchIndex == Selected2.matchIndex)
                {
                    //print("Match pair: " + selected1.matchIndex);
                    StartCoroutine(DeleteBlocks());
                    MatchedPairs++;

                }
                else
                {
                    StartCoroutine(ResetBlocks());
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
        StatText.text = "Turns: " + Turns + "\nMatches: " + MatchedPairs;
    }

    /// <summary>
    /// This function resets the current two selected blocks and destroys them. We also remove them
    /// from the existing blocks list. After 1 second we set the variables to null so we can select two new blocks
    /// and play a tune for having found a matching pair. 
    /// </summary>
    IEnumerator DeleteBlocks()
    {
        MemoryGameSetup.Instance.Blocks.Remove(Selected1);
        MemoryGameSetup.Instance.Blocks.Remove(Selected2);
        Selected1.destroy();
        Selected2.destroy();
        Selected1 = null;
        Selected2 = null;
        yield return new WaitForSecondsRealtime(1f);
        if (MemoryGameSetup.Instance.Blocks.Count == 0)
        {
            GameOver();
        }

    }

    /// <summary>
    /// Called when the game is over.
    /// Invokes the onGameOver event
    /// </summary>
    private void GameOver()
    {
        _isRunning = false;
        if(PlayTune)
        {
            PlayTune.Stop(); // stops the replay of the music that is played during the game
            VictoryTune.Play(); // starts the replay of the tune that is played when all blocks are matched -> victory is achieved
        }
        if(ArgTimer)
        {
            ArgTimer.StopTimer();
        }
        if(GameEndHandler)
        {
            GameEndHandler.SetPostGameStat(ArgTimer, Turns);
        }
        
        if (OnGameOver != null)
            OnGameOver.Invoke();
    }

    /// <summary>
    /// This function resets / deselects the current two selected blocks and clears the variables
    /// in order to make the selection of two new blocks possible.
    /// </summary>
    IEnumerator ResetBlocks()
    {
        Selected1.deselect();
        Selected2.deselect();
        yield return new WaitForSecondsRealtime(1f);
        Selected1 = null;
        Selected2 = null;
    }


    void ResetAudio()
    {
        if (PlayTune == null)
        {
            return;
        }
            if (VictoryTune.isPlaying)
        {
            VictoryTune.Stop();
        }

        if (!PlayTune.isPlaying)
        {
            PlayTune.Play();
        }

    }


    /// <summary>
    /// This function restarts the game by calling the Restart function in the MemoryGameSetup class.
    /// It also resets values for turns and matched pairs, updates the UI accordingly and resets the audio players and files. 
    /// </summary>
    public void Restart()
    {
        Turns = 0;
        MatchedPairs = 0;
        Selected1 = null;
        Selected2 = null;
        if(ArgTimer)
        {
            ArgTimer.StopTimer();
            ArgTimer.ResetTimer();
        }
        UpdateUi();
        ResetAudio();
        if (OnGameStarts != null)
            OnGameStarts.Invoke();
        MemoryGameSetup.Instance.ReCreateGameArea();
        if(Countdown < 1f)
        {
            PostGameStart();
        }else
        {
            InitGameCountdown();
        }
        


    }

    /// <summary>
    /// Postinitialization phase for the game. Here the timer will be started and game state will be set to Running 
    /// </summary>
    public void PostGameStart()
    {
        if (ArgTimer)
        {
            ArgTimer.StartTimer();
        }
        if(CountdownHandler)
        {
            CountdownHandler.ClosePreview();
        }
        _countDownTimerIsRunning = false;
        _isRunning = true;
    }


    /// <summary>
    /// Clear the game Area
    /// </summary>
    public void CloseGame()
    {
        MemoryGameSetup.Instance.ClearGameArea();
    }

    /// <summary>
    /// 
    /// </summary>
    private void InitGameCountdown()
    {
        _cdTimeLeft = Countdown;
        _lastIntegerTimeValue = (int)_cdTimeLeft + 1;
        _countDownTimerIsRunning = true;
        CountdownHandler.OpenPreview(_lastIntegerTimeValue);
        
    }
}
