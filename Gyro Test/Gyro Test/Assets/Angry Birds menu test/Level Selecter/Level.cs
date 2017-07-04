using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{

    public Level()
    {
        highScore = 0;
        currentScore = 0;
        unlocked = false;
        OneStarReq = 10;
        TwoStarReq = 8;
        ThreeStarReq = 6;
    }

    public int OneStarReq, TwoStarReq, ThreeStarReq;

    public bool Defeated
    {
        get
        {
            if (highScore > OneStarReq)
            {
                return true;
            }
            return false;
        }
    }

    public bool CurrentDefeated
    {
        get
        {
            if (currentScore >= OneStarReq)
            {
                return true;
            }
            return false;
        }
    }

    public int CurrStarScore
    {
        get
        {
            if (currentScore >= ThreeStarReq)
                return 3;
            if (currentScore >= TwoStarReq)
                return 2;
            if (currentScore >= OneStarReq)
                return 1;
            return 0;
        }
    }

    public int HighStarScore
    {
        get
        {
            if (highScore >= ThreeStarReq)
                return 3;
            if (highScore >= TwoStarReq)
                return 2;
            if (highScore >= OneStarReq)
                return 1;
            return 0;

        }
    }

    public int HighScore
    {
        get
        {
            return highScore;
        }

        set
        {
            highScore = value;
        }
    }

    public bool Unlocked
    {
        get
        {
            return unlocked;
        }

        set
        {
            unlocked = value;
        }
    }

    public int CurrentScore
    {
        get
        {
            return currentScore;
        }

        set
        {
            currentScore = value;
        }
    }

    private int highScore, currentScore;
    private bool unlocked;
}
