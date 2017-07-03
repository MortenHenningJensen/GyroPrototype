using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndScreen : MonoBehaviour
{
    public bool levelCompleted;
    private GameControl control;
    public int starsUnlocked;

    private void Start()
    {
        levelCompleted = false;
        control = FindObjectOfType<GameControl>();
    }

    public void BackToMenu()
    {
        if (Time.timeScale != 1)
        {
            Time.timeScale = 1;
        }
        Destroy(GameObject.Find("GameTracker"));

        if (levelCompleted)
        {
            SaveProgress(SceneManager.GetActiveScene().ToString(), starsUnlocked);
        }
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

        if (levelCompleted)
        {
            SaveProgress(SceneManager.GetActiveScene().ToString(), starsUnlocked);
        }
    }

    public void SaveProgress(string level, int stars)
    {
        PlayerPrefs.SetInt("Level " + level, stars);
        PlayerPrefs.SetInt("lvl " + level, 1);


        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        string path = SceneUtility.GetScenePathByBuildIndex(nextSceneIndex);
        string sceneName = path.Substring(0, path.Length - 6).Substring(path.LastIndexOf('/') + 1);

        PlayerPrefs.SetInt("lvl " + sceneName, 1);

        //FÅ FAT I NÆSTE LEVEL I BUILD SETTINGS, FÅ FAT I NAVN OG SÆT DENS "lvl " TIL 1!!!
        //FIND UD AF HVORDAN MAN UNLOCKER NÆSTE LEVEL
    }
}
