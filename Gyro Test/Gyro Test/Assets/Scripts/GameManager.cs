using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private Plate p;

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
    private bool _canEnd; //End condition has been achieved

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

    // Use this for initialization
    void Start()
    {
        _numbOfActivatedPlates = 0;
        _numbOfWinPlates = 0;
        allPlates = new List<GameObject>();     //List of all Plates ingame
        actPlates = new List<GameObject>();     //List of all Activation Plates
        normPlates = new List<GameObject>();    //List of all Normal Plates
        goalPlate = new List<GameObject>();     //List containing the Goal Plate

        plates = GameObject.FindGameObjectsWithTag("Plate"); //adds every gameobject with Tag "Plate" to this list.

        foreach (GameObject pl in plates)
        {
            allPlates.Add(pl);
        }

        StartGame();

        NumbOfWinPlates = actPlates.Count; //Number of activation plates to win
    }

    void StartGame()
    {
        //List<GameObject> tmpList = allPlates;

        //for (int i = 0; i < plates.Length; i++)
        //{
        //    allPlates[i].GetComponent<Plate>().SetupPlates(tmpList[i]);
        //}

        foreach (GameObject pl in allPlates)
        {
            pl.GetComponent<Plate>().SetupPlates(pl);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    //WinningCondition();

    /// <summary>
    /// Checks the number of activated plates, if it is equal to 
    /// the number of plates required to win..
    /// Disables the triggers on the activationPlates, so you don't accidently 
    /// ruin the win-condition..
    /// Changes the material on the StartPlate, to EndPlate..
    /// </summary>
    public void WinningCondition()
    {
        /*Hele denne metode skal måske flyttes, så det ikke er hver plate der har denne metode,
        men at det er en del af spillets logik istedet.. */
        if (_numbOfActivatedPlates == NumbOfWinPlates)
        {
            _canEnd = true;

            foreach (GameObject pl in actPlates)
            {
                pl.GetComponent<BoxCollider>().isTrigger = false;
            }

            foreach (GameObject pl in goalPlate)
            {
                pl.GetComponent<Plate>().CurrentMaterial = pl.GetComponent<Plate>().Material5;
                pl.GetComponent<Plate>().Rend.material = pl.GetComponent<Plate>().CurrentMaterial;
            }

            Debug.Log("You can now finish the game!");
        }
    }

    public void EndStatus()
    {
        //1. Load animation for switching levels
        //2. Pop-up with status screen (Time, Re-tries)
        //3. Buttons, "Main Menu", "Share Result", "Re-try","Next Level"
    }
}
