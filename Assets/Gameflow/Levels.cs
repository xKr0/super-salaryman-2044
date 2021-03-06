﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Levels : MonoBehaviour
{
    [SerializeField] WorkDay[] days;
    [SerializeField] int maxJobCount;
    [SerializeField] GameObject nextLevelInstruction;
    [SerializeField] Roulette heroRoullette;

    [SerializeField] Color color;
    [SerializeField] Image upIcon;
    [SerializeField] Image rIcon;
    [SerializeField] Image dIcon;
    [SerializeField] Image lIcon;


    // for ui icons
    [SerializeField] IconForUI iconManager;

    // for counting score for each level
    public ScoreManager scoreManager;
    [SerializeField] Timer dayTimer;


    IList<Job> jobs = new List<Job>();
    public static SuperHero currentHero;

    int currentLevel = 0;
    bool isPlaying;

    public bool IsPlaying { get { return isPlaying; } }

    int jobCount { get { return jobs.Count; } }

    public bool Ended { get { return currentLevel == days.Length; } }

    event System.Action _onEnd = delegate {};

    public event System.Action OnEnd
    {
        add { _onEnd += value; }
        remove { _onEnd -= value; }
    }

    public void StartNext()
    {
        isPlaying = true;
        //reset score and time
        scoreManager.ResetScore();
        dayTimer.Begin();

        if (days[currentLevel].AddsJob)
        {      
            Job newJob = GameController.PickJob();            

            if (jobCount == maxJobCount)
            {
                int replacedJob = Random.Range(0, jobCount);
                jobs[replacedJob] = newJob;
            }
            else
            {
                jobs.Add(newJob);
            }

            if (GetCurrentJobCount() == 1)
            {
                Debug.Log("aaa" + newJob.icon);
                upIcon.sprite = newJob.icon;
                upIcon.color = color;
            }
            if (GetCurrentJobCount() == 2)
            {
                rIcon.sprite = newJob.icon;
                rIcon.color = color;
            }
            if (GetCurrentJobCount() == 3)
            {
                dIcon.sprite = newJob.icon;
                dIcon.color = color;
            }
            if (GetCurrentJobCount() == 4)
            {
                lIcon.sprite = newJob.icon;
                lIcon.color = color;
            }

            LogJob(newJob);
        }

        // create the first encouter
        NextEncounter();
    }

    // get the next oppennant, or if the maximum opennent was done then go to the next day
    public void NextEncounter()
    {
        Debug.Log(days[currentLevel].HeroCount);

        if (days[currentLevel].HeroCount == 0)
        {
            // we change towartd the new level
            EndCurrent();
        }
        else
        {
            currentHero = days[currentLevel].CreateEncounter();
            heroRoullette.Turn();
            // for ui icons
            iconManager.HandleIcons(currentHero.GetPowers(), currentHero.GetFears());
        }
    }

    public void EndCurrent()
    {
        //ends current level
        currentLevel++;

        // start the next level
        if (currentLevel < days.Length)
        {   
            isPlaying = false;
            if (nextLevelInstruction != null)
            {
                scoreManager.EndLevelScore((int)dayTimer.GetTimeLeft());
                nextLevelInstruction.SetActive(true);
            }  
        }
        else
        {
            _onEnd();
        }
    }

    void Update()
    {
        if (!isPlaying && Input.GetKeyDown(KeyCode.Space))
        {
            isPlaying = true;
            StartNext();
            if (nextLevelInstruction != null)
                nextLevelInstruction.SetActive(false);
        }
    }

    public int GetCurrentJobCount()
    {
        return jobCount;
    }

    public WorkDay GetCurrentDay()
    {
        return days[currentLevel];
    }

    public SuperHero GetSuperHero()
    {
        return currentHero;
    }

    public Job KeyToJob(int index)
    {
        return jobs[index];
    }

    public void LogJob(Job job)
    {
        string powersString = "";
        foreach (var p in job.GetPowers())
        {
            powersString += " " + p.Power;
        }

        string fearsString = "";
        foreach (var fear in job.GetFears())
        {
            fearsString += " " + fear.FearName;
        }
        Debug.Log("job choisi : " + job.GetName() + " / " + powersString + " / " + fearsString);
    }
}
