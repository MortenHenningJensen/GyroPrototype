using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum typetorotate { level, ball }

public class Gyro : MonoBehaviour
{

    float speed;
    public Rigidbody rb;
    public typetorotate type;

    // Use this for initialization
    void Start()
    {
        speed = 10;
    }

    // Update is called once per frame
    void Update()
    {

        //var dir = Vector3.zero;

        //dir.x = Input.acceleration.x;
        //dir.y = Input.acceleration.y;
        //dir.z = Input.acceleration.z;


        //if (dir.sqrMagnitude > 1)
        //{
        //    dir.Normalize();
        //}

        //dir *= Time.deltaTime;
        //transform.Rotate(dir * speed);
    }

    void FixedUpdate()
    {
        Input.gyro.enabled = true;
        float initialOrientationX = Input.gyro.rotationRateUnbiased.x;
        float initialOrientationY = Input.gyro.rotationRateUnbiased.y;

        switch (type)
        {
            case typetorotate.ball:
                rb.AddForce(initialOrientationY * speed, 0.0f, -initialOrientationX * speed);
                break;
            case typetorotate.level:
                transform.Rotate(initialOrientationX, 0.0f, initialOrientationY);
                break;
        }


    }
}
