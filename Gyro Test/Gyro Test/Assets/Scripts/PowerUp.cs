using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerType { invert, speed, changeControl }

public class PowerUp : MonoBehaviour
{

    public PowerType powerType;

    private void Update()
    {
        transform.Rotate(0, 45 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            switch (powerType)
            {
                case PowerType.invert:
                    other.gameObject.GetComponent<Gyro>().StartCoroutine("InvertControls");
                    break;
                case PowerType.speed:
                    other.gameObject.GetComponent<Gyro>().StartCoroutine("SpeedUp");
                    break;
                case PowerType.changeControl:
                    other.gameObject.GetComponent<Gyro>().StartCoroutine("InvertPlayer");
                    break;
            }
        }

        Destroy(this.gameObject);
    }
}
