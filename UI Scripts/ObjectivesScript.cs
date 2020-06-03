﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ObjectivesScript : MonoBehaviour
{
    public GameObject LevelCompleted; 
    public GameObject Congratulations;
    public GameObject Player;
    public GameObject BossCannibal;
    public GameObject BossBoar;
    public GameObject BossAppear;
    public Text Score;
    public EnemyManager EnemyManager;

    private int initial_Target_Boars = 5;
    private int initial_Target_Cannibals = 10;

    private int Target_Boars = 5;
    private int Target_Cannibals = 10;

    
    public int Num_Missions_Completed = 0;

    private int random;

    [Header("Text Properties(CongratulationsText)")]
    public Color CongratultionsColor = Color.white;
    public int CongratulationsFontSize = 40;

    Text ObjectiveText;
    [Header("Text Properties(Objectives)")]
    public Color ObjectiveTextColour = Color.white;
    public int ObjectiveFontSize;
    public FontStyle fontStyle;

    private int maxCapCannibals;
    private int maxCapBoars;


    public float counter1 = 0f;
    private float counter2 = 0f;
    private float counter3 = 0f;

    void Start()
    {
        //Gets the components required to run the code
        ObjectiveText = GetComponent<Text>();
        ObjectiveText.color = ObjectiveTextColour;
        ObjectiveText.fontSize = ObjectiveFontSize;
        ObjectiveText.fontStyle = fontStyle;
        ObjectiveText.text = "Objectives\n" + "\n" + "Boars: " + initial_Target_Boars + "\n" + "Cannibals: " + initial_Target_Cannibals;
        BossAppear = GetComponent<GameObject>();
        EnemyManager = GetComponent<EnemyManager>();
        LevelCompleted = GetComponent<GameObject>();
        Score = GetComponent<Text>();
    }

    private void Mission()
    {
        maxCapBoars = initial_Target_Boars;
        maxCapCannibals = initial_Target_Cannibals;
        //if the Boars killed is greater than the target, revert back to target
        if(ScoreScript.scoreValueBoars > maxCapBoars) 
        {
            ScoreScript.scoreValueBoars = maxCapBoars;
        }
        //if the Cannibals killed is greater than the target, revert back to target
        else if (ScoreScript.scoreValueCannibals > maxCapCannibals)
        {
            ScoreScript.scoreValueCannibals = maxCapCannibals;
        }
        //Checks if the number of completed missions is below 5, if true it loops
        while ((Num_Missions_Completed < 5))
        {
            //checks if the boars killed has reached target for boars then continue
            if (ScoreScript.scoreValueBoars == initial_Target_Boars)
            {
                //checks if the cannibals killed has reached target for cannibals then continue
                if (ScoreScript.scoreValueCannibals == initial_Target_Cannibals)
                {
                    //increases the number of mission completed by 1, Sets new targets, resets the score and runs a congratulations text
                    Num_Missions_Completed += 1;
                    NewTargetAmounts();
                    ResetScore();
                    Congrats();
                    initial_Target_Boars = Target_Boars;
                    initial_Target_Cannibals = Target_Cannibals;
                    ObjectiveText.text = "Objectives\n" + "\n" + "Boars: " + initial_Target_Boars + "\n" + "Cannibals: " + initial_Target_Cannibals;
                    
                }
                //if the previous if was false then leave the loop
                else
                {
                    break;
                }
            }
            //if the previous if was false then leave the loop
            else
            {
                break;
            }
        }
        //Checks if number of missions completed is 5 so a boss can be spawned
        if(Num_Missions_Completed == 5)
        {
            SpawnBoss();
        }
        //same as the previous while loop
        while(Num_Missions_Completed > 6 && Num_Missions_Completed < 9)
        {
            if (ScoreScript.scoreValueBoars == initial_Target_Boars)
            {
                if (ScoreScript.scoreValueCannibals == initial_Target_Cannibals)
                {
                    Num_Missions_Completed += 1;
                    NewTargetAmounts();
                    ResetScore();
                    Congrats();
                    initial_Target_Boars = Target_Boars;
                    initial_Target_Cannibals = Target_Cannibals;
                    ObjectiveText.text = "Objectives\n" + "\n" + "Boars: " + initial_Target_Boars + "\n" + "Cannibals: " + initial_Target_Cannibals;
                }

                else
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }
        //same as previous if statemement
        if(Num_Missions_Completed == 10)
        {
            BossAppeared();
            SpawnBoss();
        }

        //if the number of mission is greater than 10 then run the function DoneText()
        else if (Num_Missions_Completed > 10)
        {
            DoneText();
        }
        
    }

    //Resets the scores that are visible for boars and cannibals
    private void ResetScore()
    {
        ScoreScript.scoreValueBoars = 0;
        ScoreScript.scoreValueCannibals = 0;
    }

    //Turns on the gameObject LevelCompleted and then invokes the functions DoneTextOff after 10 seconds
    private void DoneText()
    {
        LevelCompleted.SetActive(true);
        Invoke("DoneTextOff", 10);
    }

    //Turns off gameObject LevelCompleted an then returns to the main menu
    private void DoneTextOff()
    {
        LevelCompleted.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main_Menus");
    }

    //Turns on the gameObject BossAppear and then invokes the functions BossAppearedOff after 10 seconds
    private void BossAppeared()
    {
        BossAppear.SetActive(true);
        Invoke("BossAppearedOff", 10);
    }

    //Turns off gameObject BossAppear
    private void BossAppearedOff()
    {
        BossAppear.SetActive(false);
    }

    //Sets new targets by multiplying the previous by 2
    private void NewTargetAmounts()
    {
        Target_Boars *= 2;
        Target_Cannibals *= 2;
    }

    //spawns the boss by calling a void function from the enemy manager
    private void SpawnBoss()
    {
        print("Boss Spawned");
        EnemyManager.SpawnBoss();
        Num_Missions_Completed += 1;
    }
    //runs this every frame
    void Update()
    {
        //calls the mission function
        Mission();
        ObjectiveText.color = Color.white;
        ObjectiveText.fontSize = ObjectiveFontSize;
        ObjectiveText.text = "Objectives\n" + "\n" + "Boars: " + initial_Target_Boars + "\n" + "Cannibals: " + initial_Target_Cannibals;
    }

    //runs a debug.log statement to test if it works in the console and sets congratulations gameobject to true and then run another function in 10 seconds
    void Congrats()
    {
        Debug.Log("Congrats has been run");
        Congratulations.SetActive(true);
        Invoke("CongratsOff", 10);
    }

    //turns off congratulations gameobject
    void CongratsOff()
    {
        Congratulations.SetActive(false);
    }

}
