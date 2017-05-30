using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTracker : MonoBehaviour {

    [SerializeField]
    public int deathCounter;
    private static LevelTracker instanceRef;
    public Canvas gameEnd;
    public bool gameEnded;


    void Awake()
    {
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
        gameEnd = GameObject.Find("EndScreen").GetComponent<Canvas>();
        gameEnd.enabled = false;

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
