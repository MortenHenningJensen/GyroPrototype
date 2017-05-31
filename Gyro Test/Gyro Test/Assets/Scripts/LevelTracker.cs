using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTracker : MonoBehaviour
{

    [SerializeField]
    public int deathCounter;
    private static LevelTracker instanceRef;
    private Canvas gameEnd;
    public bool gameEnded;
    public static bool findOnce;

    void Awake()
    {
        findOnce = false;

        if (instanceRef == null)
        {
            instanceRef = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    private void Update()
    {
        if (!findOnce)
        {
            gameEnd = GameObject.Find("EndScreen").GetComponent<Canvas>();
            gameEnd.enabled = false;
            findOnce = true;
        }

        if (deathCounter >= 3)
        {
            gameEnded = true;
            gameEnd.enabled = true;
        }
    }

    public void AddDeath()
    {
        Debug.Log("ADD DEATH");
        deathCounter++;
    }
}
