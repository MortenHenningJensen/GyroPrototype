using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum typetorotate { level, ball }

public class Gyro : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody rb;
    [Space(5)]

    [Header("Variables")]
    public typetorotate type;
    [Space(5)]

    [Header("Phone Variables")]
    public float initialOrientationX;
    public float initialOrientationY;
    [Range(0.1f, 2f)]
    public float smooth = 0.5F;
    public float tiltAngle = 30.0F;
    [Range(5, 100)]
    public float speed;

    // Use this for initialization
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        if (!Input.gyro.enabled)
        {
            Input.gyro.enabled = true;
        }

        if (GetComponent<Rigidbody>() != null)
        {
            rb = GetComponent<Rigidbody>();
            type = typetorotate.ball;
            speed = 10;
        }
        else
        {
            type = typetorotate.level;
        }

    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        //Using -Input here, so it feels more real, so when you tilt for phone forward, the plane will go forward
        initialOrientationX = -Input.gyro.rotationRateUnbiased.x * tiltAngle;
        initialOrientationY = -Input.gyro.rotationRateUnbiased.y * tiltAngle;

        switch (type)
        {
            case typetorotate.ball:
                rb.AddForce(initialOrientationY * speed, 0.0f, -initialOrientationX * speed);
                break;
            case typetorotate.level:
                //Rotates the platform according to the gyroscope in the phone
                transform.Rotate(initialOrientationX / tiltAngle, 0, initialOrientationY / tiltAngle);

                //This will reset the planes position, so the Y axis is straight (0), does add some "drag" to the game, either make this more smooth, or just use this somehow for movement
                Quaternion target = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.y);
                transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
                break;
        }

    }
}
