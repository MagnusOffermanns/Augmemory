using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ARGameTimer : MonoBehaviour 
{
    public float Timer; //the float variable for the timer (seconds)
    public int Minutes; //the int variable that changes according to the minutes passed
    public TextMeshProUGUI TimerText; // a reference to the text on the UI displaying the time
    private bool _timertoggler = false; // this bool is used to not execute the counting routine in the update function


     public void StartTimer() // starts the timer
    {
        _timertoggler = true;
    }


    public void StopTimer() { //stops the timer to see how long one needed
        _timertoggler = false;
    }

    public void ResetTimer() // resets the timer to 0:0
    {
        Timer = 0.0f;
        Minutes = 0;
    }

    // Update is called once per frame
    /* 
    In the update function we create a timer for the game. If the seconds passed are 9 or below,
    a 0 is added in front of the seconds to keep a consistent display. If 60 seconds have passed, 
    the minutes variable increases, to communicate the playtime better to the player. 
    TextMeshPro is used to create a text object in the game for the timer, referenced by the variable 'timerText'.
    */
    void Update()
    {
        if (_timertoggler) // is not executed if the timer is stopped i.e. timertoggler =false
        {
            Timer = Timer + Time.deltaTime; //increases timer variable

            if ((int)Timer == 60) //if 60 seconds have passed
            {
                Minutes++; //increase number of minutes
                Timer = 0f; //reset the seconds timer
            }


            if ((int)Timer <= 9) // if the seconds passed are 9 or below
            {
                TimerText.text = Minutes + ":0" + (int)Timer; //add a 0 before displaying it
            }
            else //otherwise
            {
                TimerText.text = Minutes + ":" + (int)Timer; //regular time display
            }
        }
    }

    
}
