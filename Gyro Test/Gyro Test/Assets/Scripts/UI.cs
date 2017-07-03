using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UI : MonoBehaviour
{

    int currentLevel = 1;
    [SerializeField]
    public float timeLeft = 5;
    string timeDisp;
    bool gameOver;
    public Text _txtTimer;
    private LevelTracker lt;

    public Canvas pauseMenu;

    // Use this for initialization
    void Start()
    {
        gameOver = false;
        lt = GameObject.Find("GameTracker").GetComponent<LevelTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!lt.gameEnded)
        {
            timeDisp = timeLeft.ToString();
            if (gameOver == false)
            {
                timeLeft -= Time.deltaTime;
            }
            if (timeLeft < 1 && gameOver == false)
            {
                GameOver();
            }

            GameObject.Find("CurrentLevel").GetComponent<Text>().text = currentLevel.ToString();

            GameObject.Find("Deaths").GetComponent<Text>().text = lt.deathCounter.ToString();
        }
    }

    void GameOver()
    {
        gameOver = true;
        lt.AddDeath();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnGUI()
    {
        int minutes = Mathf.FloorToInt(timeLeft / 60F);
        int seconds = Mathf.FloorToInt(timeLeft - minutes * 60);
        string timeDisp = string.Format("{0:0}:{1:00}", minutes, seconds);
        _txtTimer.text = timeDisp;
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseMenu.enabled = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.enabled = false;
    }
}
