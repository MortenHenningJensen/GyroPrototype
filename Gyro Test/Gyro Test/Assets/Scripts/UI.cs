using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    int currentLevel = 1;
    string levelString;
    [SerializeField]
    float timeLeft = 5;
    string timeDisp;
    int deathCount = 0;
    string deathAmount;
    bool gameOver;
    public Text _txtTimer;

	// Use this for initialization
	void Start () {
        gameOver = false;
    }   
    // Update is called once per frame
    void Update () {
        timeDisp = timeLeft.ToString();
        if (gameOver == false)
        {
            timeLeft -= Time.deltaTime; 
        }     
        if (timeLeft < 1 && gameOver == false)
        {
            GameOver();
        }
        GameObject.Find("CurrentLevel").GetComponent<Text>().text = levelString;
        levelString = currentLevel.ToString();

        GameObject.Find("Deaths").GetComponent<Text>().text = deathAmount;
        deathAmount = deathCount.ToString();
    }
    void GameOver()
    {
        gameOver = true;
        deathCount++;       
    }
    void OnGUI()
    {
        int minutes = Mathf.FloorToInt(timeLeft / 60F);
        int seconds = Mathf.FloorToInt(timeLeft - minutes * 60);
        string timeDisp = string.Format("{0:0}:{1:00}", minutes, seconds);
        _txtTimer.text = timeDisp;
    }
}
