using UnityEngine;
using System.Collections;

public class Results : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    public void UpdateResults(UIManager manager)
    {
        Level currlevel = manager.Control.CurrLevel;

        if (currlevel.CurrentDefeated)
        {
            manager.Control.CurrWorld.Levels[manager.LevelIndex + 1].Unlocked = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
