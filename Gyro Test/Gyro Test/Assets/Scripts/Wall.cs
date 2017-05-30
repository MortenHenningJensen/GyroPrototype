using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    public enum WallType { NormalWall = 1, Door = 2 };

    private GameManager gm;

    [SerializeField]
    private WallType _typeNumb; //Type to choose from, in the Inspector

    //Wall Material
    private Renderer _rend;
    [SerializeField]
    private Material _currentMaterial;
    [SerializeField]
    private Material _material1;
    [SerializeField]
    private Material _material2;

    //Wall Fields
    [SerializeField]
    private bool _wallActive; //Wall_Activation
    [SerializeField]
    private float _wallDownTime;
    private Animator _wallAni;


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

    public WallType TypeNumb
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

    public bool WallActive
    {
        get
        {
            return _wallActive;
        }

        set
        {
            _wallActive = value;
        }
    }

    #endregion

    // Use this for initialization
    void Start () {
        _wallAni = GetComponent<Animator>();
        _wallDownTime = 2f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //if (Input.GetKeyDown(KeyCode.A) && this.TypeNumb == WallType.Door)
        //{
        //    _wallActive = true;
        //    _wallAni.SetFloat("Blend", 1f);
        //}
        //if (Input.GetKeyDown(KeyCode.S) && this.TypeNumb == WallType.Door)
        //{
        //    _wallActive = false;
        //    _wallAni.SetFloat("Blend", 0f);
        //}
    }

    /// <summary>
    /// Setup the plates and gives them each the various colors and functions
    /// </summary>
    public void SetupWalls(GameObject go)
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>(); //Finds the GameManager, and assing it to the variable "gm"..

        Rend = this.GetComponent<Renderer>(); //Finds the plates Rendere..
        Rend.enabled = true; //Enables the rendere, so we can change it's material..
        CurrentMaterial = this.GetComponent<Material>(); //Assigns a new variable, which has properties of a material..

        _wallActive = false;

        switch (TypeNumb)
        {
            case WallType.NormalWall:
                CurrentMaterial = Material1; //Changes the Current-material to a specific material..
                gm.DoorWall.Add(go); //Adds the Gameobject to a list, which is used in the GameManager..
                break;
            case WallType.Door:
                CurrentMaterial = Material2;
                _wallActive = false;
                gm.DoorWall.Add(go);
                break;
            default:
                break;
        }

        Rend.material = CurrentMaterial; //Runs the render in the GameObject (Plate), so it gets the new material..
    }

    /// <summary>
    /// Activates the wall animation for "opening" and "closing"..
    /// Also sets the _wallActivation to "True" and the amount of time the path will be "open"..
    /// </summary>
    public void ChangeWall()
    {
        if (!_wallActive)
        {
            Debug.Log("Wall is going DOWN!");
            _wallActive = true;
            _wallAni.SetFloat("Blend", 1f);
            StartCoroutine(WallTimer(_wallDownTime));
        }
        else if (_wallActive)
        {
            Debug.Log("Wall is going UP!");
            _wallActive = false;
            _wallAni.SetFloat("Blend", 0f);
        }
    }

    /// <summary>
    /// The Wall timer, how long the path is open...
    /// </summary>
    /// <param name="waitTime">How long the wall should be open/down..</param>
    /// <returns>Hold timer..</returns>
    private IEnumerator WallTimer(float waitTime)
    {
        while (_wallActive)
        {
            yield return new WaitForSeconds(waitTime); //Holds the wall DOWN for time, waitTime..
            ChangeWall();
        }
    }
}
