using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ARGameTimer : MonoBehaviour
{
    public float timer; //the float variable for the timer (seconds)
    public int minutes; //the int variable that changes according to the minutes passed
    public TextMeshProUGUI timerText; // a reference to the text on the UI displaying the time
    private bool timertoggler = false; // this bool is used to not execute the counting routine in the update function


     public void startTimer() // starts the timer
    {
        timertoggler = true;
    }


    public void stopTimer() { //stops the timer to see how long one needed
        timertoggler = false;
    }

    public void resetTimer() // resets the timer to 0:0
    {
        timer = 0.0f;
        minutes = 0;
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
        if (timertoggler) // is not executed if the timer is stopped i.e. timertoggler =false
        {
            timer = timer + Time.deltaTime; //increases timer variable

            if ((int)timer == 60) //if 60 seconds have passed
            {
                minutes++; //increase number of minutes
                timer = 0f; //reset the seconds timer
            }


            if ((int)timer <= 9) // if the seconds passed are 9 or below
            {
                timerText.text = minutes + ":0" + (int)timer; //add a 0 before displaying it
            }
            else //otherwise
            {
                timerText.text = minutes + ":" + (int)timer; //regular time display
            }
        }
    }

    
}
