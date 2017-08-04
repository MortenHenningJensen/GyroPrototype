using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalibrateCounter : MonoBehaviour {

    public Vector3 counterPos;
    public Text counterText;

    Matrix4x4 calibrationMatrix;
    Vector3 wantedDeadzone = Vector3.zero;

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(this);

        if (!Input.gyro.enabled)
        {
            Input.gyro.enabled = true;
        }

    }

    public void SetCounter()
    {
        counterPos.x = Input.acceleration.x;
        counterPos.y = -Input.acceleration.y;
        counterText.text = counterPos.ToString();
    }

    void calibrateAccelerometer()
    {
        wantedDeadzone = Input.acceleration;
        Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0f, 0f, -1f), wantedDeadzone);
        Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, rotateQuaternion, new Vector3(1f, 1f, 1f));
        calibrationMatrix = matrix.inverse;
    }

}
