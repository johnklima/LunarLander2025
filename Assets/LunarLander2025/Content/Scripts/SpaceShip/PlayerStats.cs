using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{



    //WIP testing random stuff


    Dictionary<string,float> stats;
    public int tester;



    public bool click = false;


    Hull hull;
    FuelTank fuelTank;

    private void Start()
    {
        GetData();
        
    }
    void GetData()
    {
        hull = GetComponent<Hull>();
        fuelTank = GetComponent<FuelTank>();
    }

    private void Update()
    {
        if (click)
        {
            CurrentStats();
            click = false;
        }
    }


    void CurrentStats()
    {
        stats = new Dictionary<string, float>()
        {
            {"healthMax",hull.maxHealth},
            {"fuelMax" , fuelTank.maxFuel },
            {"lives" , hull.lives },
            {"score", 0 }, //TODO: get score from script HighScore / planet
            {"health", hull.remainingHealth},
            {"fuel" , fuelTank.currentFuel}

        };


        foreach (KeyValuePair<string,float> item in stats)
        {
            Debug.Log(item.Key + " " + item.Value);
        }

        SetStats();
    }

    void SetStats()
    {


        hull.HealthHandler();

    }
}
