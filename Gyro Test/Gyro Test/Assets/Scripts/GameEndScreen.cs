using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndScreen : MonoBehaviour {

    public void BackToMenu()
    {
        Destroy(GameObject.Find("GameTracker"));
        SceneManager.LoadScene("Level Select");
    }

    public void ReplayLevel()
    {
        Destroy(GameObject.Find("GameTracker"));
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
