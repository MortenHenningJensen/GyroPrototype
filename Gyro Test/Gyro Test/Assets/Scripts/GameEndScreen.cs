using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndScreen : MonoBehaviour
{
    private GameControl control;

    private void Start()
    {
        control = FindObjectOfType<GameControl>();
    }

    public void BackToMenu()
    {
        if (Time.timeScale != 1)
        {
            Time.timeScale = 1;
        }
        Destroy(GameObject.Find("GameTracker"));

        //control.StartGame("LevelSelecter");

        SceneManager.LoadScene("Level Select");
    }

    public void ReplayLevel()
    {
        if (Time.timeScale != 1)
        {
            Time.timeScale = 1;
        }
        Destroy(GameObject.Find("GameTracker"));
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
