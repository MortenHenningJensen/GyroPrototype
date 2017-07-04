using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PowerType { invert, speed, changeControl, GoldenEgg }

public class PowerUp : MonoBehaviour
{
    public string levelToUnlock;
    public PowerType powerType;

    private void Start()
    {
        if (powerType == PowerType.GoldenEgg)
        {
            if (PlayerPrefs.GetInt("Level " + SceneManager.GetActiveScene().name) >= 3)
            {
                transform.gameObject.SetActive(true);
            }
            else
            {
                transform.gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        transform.Rotate(0, 45 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
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
                case PowerType.GoldenEgg:
                    PlayerPrefs.SetInt("Hidden " + levelToUnlock, 1);
                    Debug.Log(PlayerPrefs.GetInt("Hidden " + levelToUnlock, 1));
                    break;
            }

        }

        Destroy(this.gameObject);
    }
}
