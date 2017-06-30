using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    private static GameControl instanceRef;
    public const int NumWorlds = 3;
    public const int NumLevels = 20;
    public List<World> allWorlds;

    public World CurrWorld
    {
        set
        {
            currWorld = value;
            Debug.Log("Current world set.");
        }
        get
        {
            if (currWorld != null)
            {
                return currWorld;
            }
            else
            {
                Debug.Log("Can't fetch current world, Value is null");
                return null;
            }
        }
    }
    public Level CurrLevel
    {
        set
        {
            currLevel = value;
            Debug.Log("Current Level set.");
        }
        get
        {
            if (currLevel != null)
            {
                return currLevel;
            }
            else
            {
                Debug.Log("Can't fetch current Level, Value is null");
                return null;
            }
        }
    }

    private Level currLevel;
    private World currWorld;

    //[SerializeField]
    //Game gamePrefab;

    //public Game CurrentGame;

    private void Awake()
    {
        if (instanceRef == null)
        {
            instanceRef = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }

    }

    // Use this for initialization
    void Start()
    {
        // StartGame(0);
        allWorlds = new List<World>();
        for (int i = 0; i < NumWorlds; i++)
        {
            allWorlds.Add(new World());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame(string level)
    {
        SceneManager.LoadScene(level);

        //if (CurrentGame != null)
        //{
        //    CheckForCurrentGameOver();
        //}

        //CurrentGame = Instantiate(gamePrefab) as Game;
        //CurrentGame.gameObject.name = "Game";

        //CurrentGame.InitLevel(level, 3);
    }

    //void CheckForCurrentGameOver()
    //{
    //    if (CurrentGame.currLevelObj == null && CurrentGame.gameOver)
    //    {
    //        Destroy(CurrentGame.gameObject);
    //        Debug.Log("Game Over!");
    //    }
    //}

    //LAV EN METODE HER, SOM HUSKER PÅ DEN SCORE MAN FÅR, SÅ DEN KAN TILDELE STARS NÅR MAN KOMMER UD?
    //ELLER FIX DET, SÅ UIMANAGER KØRER PÅ SINGLETON, MEN FAKTISK FATTER NOGET
}
