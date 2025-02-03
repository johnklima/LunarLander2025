using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CAManager : MonoBehaviour
{

    public GameObject protoCell;

    int[] cells;
    private GameObject[] cellObjs;

    public int[] ruleset = { 0, 0, 0, 1, 1, 1, 1, 0 }; //rule 30

    private int length = 32;
    private int generation = 0;
    public int mod = 3; 

    // Start is called before the first frame update
    void Start()
    {

        cellObjs = new GameObject[length];
        cells = new int[length];
        generation = 0;
        InstantiateGeneration(); //make the first row
        generation = 1;          //increment generation

        cellObjs[16].SetActive(true);
        cells[16] = 1;

    }
    void InstantiateGeneration()
    {
        for (int i = 0; i < length; i++)
        {
            cellObjs[i] = Instantiate(protoCell);
            cellObjs[i].name = "meIs" + i + "gen" + generation; 
            cellObjs[i].transform.parent = transform;
            cellObjs[i].transform.localPosition = Vector3.right * i + Vector3.forward * generation;
            cellObjs[i].SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        generate();
    }
    void generate()
    {

        if (generation > length) return;


        //create the next row of cellObjs
        InstantiateGeneration();

        // First we create an empty array for the new values
        int[] nextgen = new int[length];

        // Ignore edges that only have one neighor
        for (int i = 1; i < length - 1; i++)
        {
            int left = cells[i - 1]; // Left neighbor state
            int me = cells[i]; // Current state
            int right = cells[i + 1]; // Right neighbor state
            nextgen[i] = Rules(left, me, right); // Compute next generation state based on ruleset
        }

        // The current generation is the new generation
        cells = nextgen;

        for (int i = 0; i < length; i++)
            if (cells[i] == 1)
                cellObjs[i].SetActive(true);
            else
                cellObjs[i].SetActive(false);

        generation++;

    }
    private int Rules(int a, int b, int c)
    {
        if (a == 1 && b == 1 && c == 1) return ruleset[0];
        if (a == 1 && b == 1 && c == 0) return ruleset[1];
        if (a == 1 && b == 0 && c == 1) return ruleset[2];
        if (a == 1 && b == 0 && c == 0) return ruleset[3];
        if (a == 0 && b == 1 && c == 1) return ruleset[4];
        if (a == 0 && b == 1 && c == 0) return ruleset[5];
        if (a == 0 && b == 0 && c == 1) return ruleset[6];
        if (a == 0 && b == 0 && c == 0) return ruleset[7];


        return 0;
    }
}
