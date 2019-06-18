using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameEndStatHandler : MonoBehaviour {

    [SerializeField]
    private TextMeshPro _text;


    /// <summary>
    /// Applies the game stats on textlabel
    /// </summary>
    /// <param name="time"></param>
    /// <param name="turns"></param>
    public void SetPostGameStat(ARGameTimer gameTimer, int turns)
    {
        string timeText = "";
        if ((int)gameTimer.Timer <= 9) // if the seconds passed are 9 or below
        {
            timeText = gameTimer.Minutes + ":0" + (int)gameTimer.Timer; //add a 0 before displaying it
        }
        else //otherwise
        {
            timeText = gameTimer.Minutes + ":" + (int)gameTimer.Timer; //regular time display
        }
        _text.text = "Game Stats\nTime: " + timeText + "\nTurns: " + turns;
    }
}
