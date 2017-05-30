using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum typetorotate { level, ball }
public enum controltype { normal, inverted }

public class Gyro : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody rb;
    public Transform level;
    [Space(5)]

    [Header("Variables")]
    public typetorotate type;
    public controltype ctrlType;
    [Space(5)]

    [Header("Phone Variables")]
    public float initialOrientationX;
    public float initialOrientationY;
    [Range(0.1f, 2f)]
    public float smooth = 0.5F;
    public float tiltAngle = 30.0F;
    [Range(5, 100)]
    public float speed;
    [Space(5)]

    [Header("PowerUps")]
    public bool inverted;

    private LevelTracker lt;

    // Use this for initialization
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        lt = GameObject.Find("GameTracker").GetComponent<LevelTracker>();
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

        if (ctrlType == controltype.inverted)
        {
            inverted = true;
        }
        else
        {
            inverted = false;
        }

    }

    void FixedUpdate()
    {
        if (!lt.gameEnded)
        {
            //This will reset the planes position, so the Y axis is straight (0), does add some "drag" to the game, either make this more smooth, or just use this somehow for movement
            Quaternion target = Quaternion.Euler(level.rotation.x, 0, level.rotation.y);
            level.rotation = Quaternion.Slerp(level.rotation, target, Time.deltaTime * smooth);

            switch (type)
            {
                case typetorotate.ball:
                    if (!inverted)
                    {
                        //Using -Input here, so it feels more real, so when you tilt for phone forward, the plane will go forward
                        initialOrientationX = Input.gyro.rotationRateUnbiased.x;
                        initialOrientationY = Input.gyro.rotationRateUnbiased.y;
                    }
                    else
                    {
                        initialOrientationX = -Input.gyro.rotationRateUnbiased.x;
                        initialOrientationY = -Input.gyro.rotationRateUnbiased.y;
                    }

                    rb.AddForce(initialOrientationY * speed, 0.0f, -initialOrientationX * speed);

                    break;

                case typetorotate.level:

                    if (!inverted)
                    {
                        //Using -Input here, so it feels more real, so when you tilt for phone forward, the plane will go forward
                        initialOrientationX = -Input.gyro.rotationRateUnbiased.x * tiltAngle;
                        initialOrientationY = -Input.gyro.rotationRateUnbiased.y * tiltAngle;
                    }
                    else
                    {
                        initialOrientationX = Input.gyro.rotationRateUnbiased.x * tiltAngle;
                        initialOrientationY = Input.gyro.rotationRateUnbiased.y * tiltAngle;
                    }

                    //Rotates the platform according to the gyroscope in the phone
                    level.Rotate(initialOrientationX / tiltAngle, 0, initialOrientationY / tiltAngle);
                    break;
            }
        }
    }

    public IEnumerator InvertControls()
    {
        inverted ^= true;
        yield return new WaitForSeconds(3);
        inverted ^= true;
    }

    public IEnumerator SpeedUp()
    {
        speed = speed * 3;
        yield return new WaitForSeconds(3);
        speed = speed / 3;
    }

    public IEnumerator InvertPlayer()
    {
        type = typetorotate.level;
        yield return new WaitForSeconds(3);
        type = typetorotate.ball;
    }

}
