using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public Canvas worldOne;
    public Canvas worldTwo;
    public Canvas worldSelect;

    // Use this for initialization
    void Start()
    {
        //worldSelect.enabled = true;
        //worldOne.enabled = false;
        //worldTwo.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnterWorldOne()
    {
        worldOne.enabled = true;

        worldSelect.enabled = false;
    }

    public void EnterWorldTwo()
    {
        worldTwo.enabled = true;

        worldSelect.enabled = false;
    }

    public void BackButton()
    {
        worldOne.enabled = false;
        worldTwo.enabled = false;

        worldSelect.enabled = true;
    }

}
