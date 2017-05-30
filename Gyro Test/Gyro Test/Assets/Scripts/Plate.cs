using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{

    #region Fields
    public enum PlateType { NormalPlate = 1, ActivationPlate = 2, GoalPlate = 3, HolePlate = 4 };

    private GameManager gm;

    [SerializeField]
    private PlateType _typeNumb; //Type to choose from, in the Inspector

    [SerializeField]
    private bool _light; //Plate_Activation

    private Renderer _rend;

    [SerializeField]
    private Material _currentMaterial;
    [SerializeField]
    private Material _material1;
    [SerializeField]
    private Material _material2;
    [SerializeField]
    private Material _material3;
    [SerializeField]
    private Material _material4;
    [SerializeField]
    private Material _material5;
    [SerializeField]
    private Material _material6;

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

    public Material Material1
    {
        get
        {
            return _material1;
        }

        set
        {
            _material1 = value;
        }
    }

    public Material Material2
    {
        get
        {
            return _material2;
        }

        set
        {
            _material2 = value;
        }
    }

    public Material Material3
    {
        get
        {
            return _material3;
        }

        set
        {
            _material3 = value;
        }
    }

    public Material Material4
    {
        get
        {
            return _material4;
        }

        set
        {
            _material4 = value;
        }
    }

    public Material Material5
    {
        get
        {
            return _material5;
        }

        set
        {
            _material5 = value;
        }
    }

    public Material Material6
    {
        get
        {
            return _material6;
        }

        set
        {
            _material6 = value;
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
                CurrentMaterial = Material1; //Changes the Current-material to a specific material..
                gm.NormPlates.Add(go); //Adds the Gameobject to a list, which is used in the GameManager..
                break;
            case PlateType.ActivationPlate:
                CurrentMaterial = Material2; //Changes the Current-material to a specific material
                _light = false; //Turns all lights off at the start..
                gm.ActPlates.Add(go); //Adds the Gameobject to a list, which is used in the GameManager..
                gm.NumbOfWinPlates++;
                break;
            case PlateType.GoalPlate:
                CurrentMaterial = Material4; //Changes the Current-material to a specific material
                gm.CanEnd = false; //Sets the goal to false, so you can't end right away..
                gm.GoalPlate.Add(go); //Adds the Gameobject to a list, which is used in the GameManager..
                break;
            case PlateType.HolePlate:
                CurrentMaterial = Material6;
                this.GetComponent<BoxCollider>().enabled = false;
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
    void ChangeLight()
    {
        if (!gm.CanEnd) //If the game hasn't ended yet..
        {
            _light = !_light; //Switched the current light to the opposite
            if (_light)
            {
                CurrentMaterial = Material3;
                this._light = true; //Turns ActivationPlate's Light ON
                gm.NumbOfActivatedPlates++;
            }
            else if (!_light)
            {
                CurrentMaterial = Material2;
                this._light = false; //Turns ActivationPlate's Light OFF
                gm.NumbOfActivatedPlates--;
            }

            Rend.material = CurrentMaterial; //Opdates the current Material..

            Debug.Log("Plate is now " + _light);

            gm.WinningCondition(); //Checks if we can win...
        }
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
                ChangeLight();
            }

            if (this.TypeNumb == PlateType.GoalPlate && gm.CanEnd)
            {
                gm.EndStatus();
            }
        }
    }
}