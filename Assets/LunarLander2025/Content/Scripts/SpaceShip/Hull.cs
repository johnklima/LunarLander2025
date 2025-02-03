using Unity.VisualScripting;
using UnityEngine;



public class Hull : MonoBehaviour
{

    /// <summary>
    /// Script handeling health and damage taken
    /// 
    /// Update 17.01.2025 - Robin
    /// main outline
    /// Update 18.01.2025 - Robin
    /// changing some of the methods
    /// adding public to certain methods in order to access them from different scripts
    /// FallDamage and ShipRepair
    /// Update
    /// 
    /// </summary>


    

    public int maxHealth;
    public int remainingHealth;
    public int lives;
    public float fallGrace = 5f;

    public Gravity gravity;
   


    private void Awake()
    {
        gravity = GetComponent<Gravity>(); // should be changed to the active gravity script
    }



    private void Start()
    {
        //Get the current stats fo the spaceship
        // TODO : assign different values to different spaceships
        // upgrades 1-5 ? and increase by 20 or 50 per upgrade
        
        maxHealth = 100; 
        remainingHealth = maxHealth;
        lives = 5; // should we add lives ?
    }

  

  

    public void HealthHandler(int health = 0)     // setting default value to 0 to prevent errors if you call the function without a value
    {

       

        // handeling damage taken by checking if the player has more heatlth left
        // requires int input for the damage taken
        //combining HealthLost and HealthGained to one function

        remainingHealth += health;

        if (remainingHealth >= maxHealth)
        {
            remainingHealth = maxHealth;
        }            
        
        if (remainingHealth <= 0)
        {
            Death();
        }        
    }

    

    public void FallDamage()
    {
        //TODO: calculate the damage taken based on velocity or impact
        //then send the damage to the *HealthLost* method 
        

        // update 18.01.2025

        Vector3 speed = gravity.velocity;

        float dmg = speed.x + speed.z + speed.y;


        //tested fall damage, seems to work but you need to invoke the function through somewhere else, like the gravity script
        //placeholder which works

        /*
        if (dmg <= -20f | dmg >= 20f)
        {
            damageScale = 50;
        }
        else if (dmg <= -10f | dmg >= 10f)
        {
            damageScale = 20;
        }
        else
        {
            damageScale = 0;
        }
        */

        //simplifying the code
        if (dmg <= fallGrace & dmg >= -fallGrace) // adding some grace to prevent damage taken 
        {
            dmg = 0;
        }
        else if (dmg > 0) // making sure that the value is always positive
        {
            dmg *= -1;
            dmg += fallGrace;
        }
        else
        {
            dmg += fallGrace;
        }

        
       
        HealthHandler((int)dmg); // calling HealthLost method and passing the damage value, converting the float to int 
        Debug.Log("Fall damage");
        
    }

    public void ShipRepair()
    {
        //TODO:  repair the ship 
        //landing pad ? or powerup in the air ?
        //placeholder

        // add a bool for a landingpad, canRepair and keypress would repair the ship at a cost of X score ?
        int heal = (int)(maxHealth / 2);
        HealthHandler(heal); // having maxhealth/2 ensures that a powerup would scale regardless of the total value, hardcoding it to 20 or 50 would fall off if the total excedes a certain ammount
        Debug.Log("Ship repaired");
    }

    void Death()
    {
        //TODO : Play death animation / explosion
        if (remainingHealth <= 0 & lives ==0)
        {

            Debug.Log("Game Over");
        }
        else
        {
            Debug.Log("You died!");

            if (lives > 0)
            {
                lives -= 1;
                remainingHealth = maxHealth; // resetting health after losing a life
            }
        }

    }



}
