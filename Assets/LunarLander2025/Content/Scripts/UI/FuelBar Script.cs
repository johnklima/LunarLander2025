using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FuelBarScript : MonoBehaviour
{
    private ProgressBar _fuelBar;
    public float fuelLevel = 100; //make the highes value for the fuel
    public float fuelLowerAmount = 5; //How much will be consumed 

    // Start is called before the first frame update
    void Start()
    {
        //connecting and getting the root to UI Toolkit document 
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        _fuelBar = root.Q<ProgressBar>("Fuelbar");

        Updatefuel(fuelLevel); 
    }

    // Update is called once per frame
    void Update()
    {
        //testing if UI work
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    fuelLevel -= fuelLowerAmount;
        //    Updatefuel(fuelLevel);
        //}
           
        // - Update: Commented out fuel level test after connecting Updatefuel method to actual Fuel Tank script - Paul 01.02.2025
    }

    public void Updatefuel (float value)
    {
        //link the fuelbar value UI
        _fuelBar.value = value;


        //if fuel is less then 25 it will turn red if not it will be normal green
        if (value <= 25)
        {
            _fuelBar.AddToClassList("low");
            Debug.Log("Low fuel");
        }

        else
        {
            _fuelBar.AddToClassList("normal");
        }
    }
}
