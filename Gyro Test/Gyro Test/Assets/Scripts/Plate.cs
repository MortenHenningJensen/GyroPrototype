using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{

    #region Fields
    public enum PlateType { NormalPlate = 1, ActivationPlate = 2, GoalPlate = 3, HolePlate = 4, LeaverPlate = 5 };
    public enum ActivationPlateState { Off = 1, On = 2 };

    private GameManager gm;
    private Wall wallScript;

    [SerializeField]
    private PlateType _typeNumb; //Type to choose from, in the Inspector
    [SerializeField]
    private ActivationPlateState _actPlaState; //State the Activation Plate is in..

    //Light Plate
    [SerializeField]
    public bool _light; //Plate_Activation

    //Plate Material
    private Renderer _rend;
    [SerializeField]
    private Material _currentMaterial;
    [SerializeField]
    private Material _matNormPlate;
    [SerializeField]
    private Material _matActPlateOff;
    [SerializeField]
    private Material _matActPlateOn;
    [SerializeField]
    private Material _matGoalOff;
    [SerializeField]
    private Material _matGoalOn;
    [SerializeField]
    private Material _matHole;
    [SerializeField]
    private Material _matActPlateLocked;

    #endregion

    #region Get/Sets
    public Material CurrentMaterial
    {
        get
        {
            return _currentMaterial;
        }

        set
        {
            _currentMaterial = value;
        }
    }

    public Material MatNormPlate
    {
        get
        {
            return _matNormPlate;
        }

        set
        {
            _matNormPlate = value;
        }
    }

    public Material MatActPlateOff
    {
        get
        {
            return _matActPlateOff;
        }

        set
        {
            _matActPlateOff = value;
        }
    }

    public Material MatActPlateOn
    {
        get
        {
            return _matActPlateOn;
        }

        set
        {
            _matActPlateOn = value;
        }
    }

    public Material MatGoalOff
    {
        get
        {
            return _matGoalOff;
        }

        set
        {
            _matGoalOff = value;
        }
    }

    public Material MatGoalOn
    {
        get
        {
            return _matGoalOn;
        }

        set
        {
            _matGoalOn = value;
        }
    }

    public Material MatHole
    {
        get
        {
            return _matHole;
        }

        set
        {
            _matHole = value;
        }
    }

    public Material MatActPlateLocked
    {
        get
        {
            return _matActPlateLocked;
        }

        set
        {
            _matActPlateLocked = value;
        }
    }

    public PlateType TypeNumb
    {
        get
        {
            return _typeNumb;
        }

        set
        {
            _typeNumb = value;
        }
    }

    public ActivationPlateState ActPlaState
    {
        get
        {
            return _actPlaState;
        }

        set
        {
            _actPlaState = value;
        }
    }

    public Renderer Rend
    {
        get
        {
            return _rend;
        }

        set
        {
            _rend = value;
        }
    }

    #endregion

    // Use this for initialization
    public void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A) && this.TypeNumb == PlateType.ActivationPlate)
        //{
        //    ChangeLight();
        //}
    }

    /// <summary>
    /// Setup the plates and gives them each the various colors and functions
    /// </summary>
    public void SetupPlates(GameObject go)
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>(); //Finds the GameManager, and assing it to the variable "gm"..
        Rend = this.GetComponent<Renderer>(); //Finds the plates Rendere..
        Rend.enabled = true; //Enables the rendere, so we can change it's material..
        CurrentMaterial = this.GetComponent<Material>(); //Assigns a new variable, which has properties of a material..

        switch (TypeNumb)
        {
            case PlateType.NormalPlate:
                CurrentMaterial = MatNormPlate; //Blue material..
                gm.NormPlates.Add(go); //Adds the Gameobject to a list, which is used in the GameManager..
                break;
            case PlateType.ActivationPlate:
                ActPlaState = ActivationPlateState.Off;
                ActivationPlateSetup(this.gameObject);
                _light = false; //Turns all lights off at the start..
                gm.ActPlates.Add(go); //Adds the Gameobject to a list, which is used in the GameManager..
                gm.NumbOfWinPlates++;
                break;
            case PlateType.GoalPlate:
                CurrentMaterial = MatGoalOff; //Yellow material..
                gm.CanEnd = false; //Sets the goal to false, so you can't end right away..
                gm.GoalPlate.Add(go); //Adds the Gameobject to a list, which is used in the GameManager..
                break;
            case PlateType.HolePlate:
                //CurrentMaterial = MatHole;
                //BoxCollider col = go.GetComponent<BoxCollider>();
                //col.size = new Vector3(0.25f, 0.25f, 0.25f);
                // this.GetComponent<BoxCollider>().enabled = false;
                this.gameObject.SetActive(false);
                break;
            case PlateType.LeaverPlate:
                wallScript = GameObject.Find("Door").GetComponent<Wall>(); //Finds the WallScript, and assing it to the variable "gm"..
                CurrentMaterial = wallScript.Material2; //Brown material..
                wallScript.WallActive = false; //Sets the wallActive to false, so it is closed..
                gm.DoorWall.Add(go); //Adds the Gameobject to a list, which is used in GameManager..
                break;
            default:
                break;
        }

        Rend.material = CurrentMaterial; //Runs the render in the GameObject (Plate), so it gets the new material..
    }

    /// <summary>
    /// Can only Change the Light, if all the lights have not been activated..
    /// Changes the light "On" and "Off", and the color of the Plate...
    /// </summary>
    public void ChangeLight()
    {
        if (!gm.CanEnd) //If the game hasn't ended yet..
        {
            _light = !_light; //Switched the current light to the opposite
            if (_light)
            {
                CurrentMaterial = MatActPlateOn;
                this._light = true; //Turns ActivationPlate's Light ON
                gm.NumbOfActivatedPlates++;
            }
            else if (!_light)
            {
                CurrentMaterial = MatActPlateOff;
                this._light = false; //Turns ActivationPlate's Light OFF
                gm.NumbOfActivatedPlates--;
            }

            Rend.material = CurrentMaterial; //Opdates the current Material..

            Debug.Log("Plate is now " + _light);

            gm.WinningCondition(); //Checks if we can win...
        }
    }

    public void ActivationPlateSetup(GameObject go)
    {
        switch (go.GetComponent<Plate>().ActPlaState)
        {
            case ActivationPlateState.Off:
                go.GetComponent<Plate>().CurrentMaterial = go.GetComponent<Plate>().MatActPlateLocked; //Red-Cross Material
                Debug.Log("Material is now: " + go.GetComponent<Plate>().CurrentMaterial);
                break;
            case ActivationPlateState.On:
                go.GetComponent<Plate>().CurrentMaterial = go.GetComponent<Plate>().MatActPlateOff; //Red material..
                Debug.Log("Material is now: " + go.GetComponent<Plate>().CurrentMaterial);
                break;
            default:
                break;
        }
        go.GetComponent<Plate>().Rend.material = go.GetComponent<Plate>().CurrentMaterial;
    }

    /// <summary>
    /// When the ball enters an ActivationPlate, it runs the ChangeLight()..
    /// When the ball enters the GoalPlate AND canEnd-bool is true, it runs gm.EndStatus()..
    /// </summary>
    /// <param name="ballCol">The ball collider</param>
    void OnTriggerEnter(Collider ballCol)
    {
        Debug.Log("I have entered a balls collision: " + this.gameObject.GetComponent<Collider>());

        if (ballCol == GameObject.FindGameObjectWithTag("Ball").GetComponent<Collider>())
        {
            if (this.TypeNumb == PlateType.ActivationPlate && !gm.CanEnd)
            {
                if (this._actPlaState == ActivationPlateState.Off)
                {
                    Debug.Log("Can't turn this on right now..");
                }
                else if (this._actPlaState == ActivationPlateState.On)
                {
                    ChangeLight();
                    if (gm.tog != TypeOfGame.Normal)
                    {
                        gm.LockedPlatesStatus();
                    }
                }
            }

            if (this.TypeNumb == PlateType.GoalPlate && gm.CanEnd)
            {
                gm.EndStatus();
            }

            //if (this.TypeNumb == PlateType.HolePlate)
            //{
            //    //GameObject go = GameObject.Find("LevelContainer");
            //    //go.SetActive(false);
            //}

            if (this.TypeNumb == PlateType.LeaverPlate)
            {
                wallScript.ChangeWall();
            }
        }
    }
}