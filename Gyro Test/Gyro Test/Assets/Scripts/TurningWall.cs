using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningWall : MonoBehaviour {

    [SerializeField]
    int rotationspeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, rotationspeed * Time.deltaTime, 0); //rotates 50 degrees per second around z axis
    }
}
