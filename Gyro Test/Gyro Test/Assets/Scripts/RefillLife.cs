using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RefillLife : MonoBehaviour {

    private const int MAX_LIVES = 5;

    // 5 seconds for testing
    private static TimeSpan newLifeInterval = new TimeSpan(0, 0, 5);


    private DateTime lostLifeTimeStamp;
    public int livesLeft = MAX_LIVES;
    private int amountOfIntervalsPassed;

    // Update is called once per frame
    void Update()
    {
        if (livesLeft < MAX_LIVES)
        {
            TimeSpan t = DateTime.Now - lostLifeTimeStamp;

            // in theory if you wait for many many years, amountOfIntervalsPassed might be bigger than fits in an int 
            try
            {
                // round down or we get a new life whenever over half of interval has passed
                double intervalD = System.Math.Floor(t.TotalSeconds / newLifeInterval.TotalSeconds);
                amountOfIntervalsPassed = Convert.ToInt32(intervalD);
            }
            catch (OverflowException)
            {
                // something has probably gone wrong. give full lives. normalize the situation
               // livesleft = MAX_LIVES;
            }

            if (amountOfIntervalsPassed + livesLeft >= MAX_LIVES)
            {
                livesLeft = MAX_LIVES;
            }

            Debug.Log("it's been " + t + " since lives dropped from full (now " + livesLeft + "). " + amountOfIntervalsPassed + " reloads passed. Lives Left: " + getAmountOfLives());
        }
    }

    [ContextMenu("Lose Life")]
    public void lostLife()
    {
        if (livesLeft-- == MAX_LIVES)
        {
            // mark the timestamp only when lives drop from MAX to MAX -1
            lostLifeTimeStamp = DateTime.Now;

            ///////////////////////////////////////////////////////////////////////////////////
            // SAVE livesLeft AND lostLifeTimeStamp HERE AND RESTORE WHEN STARTING THE GAME ///
            ///////////////////////////////////////////////////////////////////////////////////
        }
    }

    int getAmountOfLives()
    {
        return Mathf.Min(livesLeft + amountOfIntervalsPassed, MAX_LIVES);
    }
}
