using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum typetorotate { level, ball }

public class Gyro : MonoBehaviour
{

    float speed;
    public Rigidbody rb;
    public typetorotate type;
    public Transform mytransform;

    public float initialOrientationX;
    public float initialOrientationY;

    public float smooth = 0.5F;
    public float tiltAngle = 30.0F;

    Quaternion startOffset;

    // Use this for initialization
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        if (!Input.gyro.enabled)
        {
            Input.gyro.enabled = true;
        }

        if (GetComponent<Rigidbody>() != null)
        {
            rb = GetComponent<Rigidbody>();
        }

        startOffset = Input.gyro.attitude;
        speed = 10;
    }

    // Update is called once per frame
    void Update()
    {
        // if (mytransform.rotation.y >= 5 || mytransform.rotation.y <= 5)
        // {
        //  ResetGyro();
        //    Input.gyro.enabled = false;
        //    var rotationsVector = mytransform.rotation.eulerAngles;
        //    rotationsVector.y = 0;
        //    mytransform.rotation = Quaternion.Euler(rotationsVector);
        //    Input.gyro.enabled = true;
        // }

        //float xRot = Mathf.Clamp(mytransform.rotation.x, minX, maxX);
        //float zRot = Mathf.Clamp(mytransform.rotation.z, minY, maxY);
        //float yRot = Mathf.Clamp(mytransform.rotation.y, 0, 0);

        //mytransform.rotation = new Quaternion(xRot, yRot, zRot, 0);
        //   if (mytransform.rotation.y > 0.0003f)
        // {
        // ResetGyro();
        //  }
    }

    void FixedUpdate()
    {
        initialOrientationX = -Input.gyro.rotationRateUnbiased.x * tiltAngle;
        initialOrientationY = -Input.gyro.rotationRateUnbiased.y * tiltAngle;

        switch (type)
        {
            case typetorotate.ball:
                rb.AddForce(initialOrientationY * speed, 0.0f, -initialOrientationX * speed);
                break;
            case typetorotate.level:
                //mytransform.Rotate(-Input.gyro.gravity.x, 0, -Input.gyro.gravity.y);
                Quaternion target = Quaternion.Euler(initialOrientationX, 0, initialOrientationY);
                mytransform.rotation = Quaternion.Slerp(mytransform.rotation, target, Time.deltaTime * smooth);
                //mytransform.Rotate(initialOrientationX, 0, initialOrientationY);
                break;
        }

    }

    void ResetGyro()
    {
        Quaternion test = new Quaternion(mytransform.eulerAngles.x, 0, mytransform.eulerAngles.z, 0);
        mytransform.rotation = test;
    }
}
