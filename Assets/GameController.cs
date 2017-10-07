﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] Levels levelsConfig;
    [SerializeField] static Job[] listOfJobs;
    [SerializeField] static SuperPower[] listOfPowers;
    int playerScore;

    public static Job GetRandomJob()
    {
        return listOfJobs[1];
    }

    // Use this for initialization
    void Start () {
        //initialisation logic
    }

    // Update is called once per frame
    void Update () {
		
        // depending on the touch pressed by the player the superhero is sent to a different job
        if (Input.GetKeyDown("z"))
        {
            print("z");
        }
        if (Input.GetKeyDown("q"))
        {
            print("q");
        }
        if (Input.GetKeyDown("s"))
        {
            print("s");
        }
        if (Input.GetKeyDown("d"))
        {
            print("d");
        }
    }

}
