using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private int _numbOfWinPlates; //Test
    [SerializeField]
    private int _numbOfActivatedPlates; //Test

    [SerializeField]
    private GameObject[] plates; //Finds all Gameobjects with tag "Plate"
    [SerializeField]
    private List<GameObject> allPlates; //Contains every Plate in game
    [SerializeField]
    private List<GameObject> normPlates; //Contains all the activationPlates
    [SerializeField]
    private List<GameObject> actPlates; //Contains all the activationPlates
    [SerializeField]
    private List<GameObject> goalPlate; //Contains only the Start-/GoalPlate

    [SerializeField]
    private GameObject[] doors; //Finds all Gameobjects with tag "Plate"
    [SerializeField]
    private List<GameObject> doorWall; //Contains only the Start-/GoalPlate


    [SerializeField]
    private bool _canEnd; //End condition has been achieved

    public Canvas gameEnd;
    public LevelTracker lt;

    #endregion

    #region Get/Sets
    public List<GameObject> AllPlates
    {
        get
        {
            return allPlates;
        }

        set
        {
            allPlates = value;
        }
    }

    public List<GameObject> NormPlates
    {
        get
        {
            return normPlates;
        }

        set
        {
            normPlates = value;
        }
    }

    public List<GameObject> ActPlates
    {
        get
        {
            return actPlates;
        }

        set
        {
            actPlates = value;
        }
    }

    public List<GameObject> GoalPlate
    {
        get
        {
            return goalPlate;
        }

        set
        {
            goalPlate = value;
        }
    }

    public int NumbOfActivatedPlates
    {
        get
        {
            return _numbOfActivatedPlates;
        }

        set
        {
            _numbOfActivatedPlates = value;
        }
    }

    public int NumbOfWinPlates
    {
        get
        {
            return _numbOfWinPlates;
        }

        set
        {
            _numbOfWinPlates = value;
        }
    }

    public bool CanEnd
    {
        get
        {
            return _canEnd;
        }

        set
        {
            _canEnd = value;
        }
    }

    public List<GameObject> DoorWall
    {
        get
        {
            return doorWall;
        }

        set
        {
            doorWall = value;
        }
    }

    #endregion

    // Use this for initialization
    void Start()
    {
        lt = GameObject.Find("GameTracker").GetComponent<LevelTracker>();
        _numbOfActivatedPlates = 0;
        _numbOfWinPlates = 0;
        allPlates = new List<GameObject>();     //List of all Plates ingame
        actPlates = new List<GameObject>();     //List of all Activation Plates
        normPlates = new List<GameObject>();    //List of all Normal Plates
        goalPlate = new List<GameObject>();     //List containing the Goal Plate
        doorWall = new List<GameObject>();      //List containing the Doors..

        plates = GameObject.FindGameObjectsWithTag("Plate"); //adds every gameobject with Tag "Plate" to this list.
        foreach (GameObject pl in plates)
        {
            allPlates.Add(pl); //Adds every Plate in game to a list..
        }

        doors = GameObject.FindGameObjectsWithTag("Door"); //adds every gameobject with Tag "Plate" to this list
        foreach (GameObject door in doorWall)
        {
            doorWall.Add(door); //Adds every Plate in game to a list..
        }

        StartGame(); //Runs the StartGame()..

        NumbOfWinPlates = actPlates.Count; //Number of activation plates to win
    }

    void StartGame()
    {
        foreach (GameObject pl in allPlates)
        {
            pl.GetComponent<Plate>().SetupPlates(pl); //Runs SetupPlates in Plate.cs, for every Plate..
        }
        foreach (GameObject wall in DoorWall)
        {
            wall.GetComponent<Wall>().SetupWalls(wall); //Runs SetupPlates in Plate.cs, for every Plate..
        }

        if (NumbOfWinPlates + actPlates.Count == 0)
        {
            WinningCondition();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Checks the number of activated plates, if it is equal to 
    /// the number of plates required to win..
    /// Disables the triggers on the activationPlates, so you don't accidently 
    /// ruin the win-condition..
    /// Changes the material on the StartPlate, to EndPlate..
    /// </summary>
    public void WinningCondition()
    {
        if (NumbOfActivatedPlates == NumbOfWinPlates)
        {
            CanEnd = true; //changes the bool to true, so the game can End.. Disables Plate.ChangeLights()..

            foreach (GameObject pl in goalPlate)
            {
                //Change material on the GoalPlate..
                pl.GetComponent<Plate>().CurrentMaterial = pl.GetComponent<Plate>().Material5;
                pl.GetComponent<Plate>().Rend.material = pl.GetComponent<Plate>().CurrentMaterial;
            }

            Debug.Log("You can now finish the game!");
        }
    }

    public void EndStatus()
    {
        gameEnd = GameObject.Find("EndScreen").GetComponent<Canvas>();
        gameEnd.enabled = true;

        Text endStatusText = GameObject.Find("Game Over").GetComponent<Text>();
        endStatusText.text = "YOU ROLL";

        lt.gameEnded = true;     

        //1. Load animation for switching levels
        //2. Pop-up with status screen (Time, Re-tries)
        //3. Buttons, "Main Menu", "Share Result", "Re-try","Next Level"
    }
}
