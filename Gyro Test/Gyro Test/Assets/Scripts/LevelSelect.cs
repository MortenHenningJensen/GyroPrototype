using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{

    string levelToEnterName;

    [SerializeField]
    public List<GameObject> levels;

    private GameObject[] levelstofind; //Finds all Gameobjects with tag "Plate"


    public void LevelSelected(string enterWorld)
    {
        if (PlayerPrefs.GetInt("lvl " + enterWorld) >= 1)
        {
            SceneManager.LoadScene(enterWorld);
        }
    }

    void Start()
    {
        levels = new List<GameObject>();

        levelstofind = GameObject.FindGameObjectsWithTag("LevelSelecter");

        foreach (GameObject lvl in levelstofind)
        {
            levels.Add(lvl); //Adds every Plate in game to a list..
        }

        SetUP();
    }

    private void SetUP()
    {
        foreach (GameObject item in levels)
        {
            // Debug.Log(item.GetComponentInChildren<Text>().text + " " + PlayerPrefs.GetInt("lvl " + item.GetComponentInChildren<Text>().text));
            if (PlayerPrefs.GetInt("lvl " + item.GetComponentInChildren<Text>().text) != 0)
            {
                // Debug.Log("TRUE");
                item.GetComponent<Button>().interactable = true;
            }
            else
            {
                // Debug.Log("FALSE");
                item.GetComponent<Button>().interactable = false;
                //item.IsInteractable();
            }
        }
    }

}
