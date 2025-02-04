using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class CellularAtomaton : MonoBehaviour
{

    // rules 0-255

    public byte ruleNumb = 0;
    public int[] inArray;
    public int[] outArray;

    public string[] pattern = { "111", "110", "101", "100", "011", "010", "001", "000" }; // ints representing a bit of information

    public GameObject objPrefab;
    public GameObject[] arrayPrefabs;

    public int generation;
    [Min(1)] public int gridWidth = 16;
    public string strCompare = "";
    public bool click = true;



    // creating an empty array and converting the rule from number to bits
    private void Start()
    {
        inArray = ByteToRuleConverter(ruleNumb);
        outArray = EmptyArray(gridWidth);

    }

    // when pressing click iterate the  generation
    private void Update()
    {
        if (click == true)
        {
            outArray = Generator();
            click = false;
        }
    }


    int[] EmptyArray(int input)
    {

        int[] emptyArray = new int[input];
        for (int i = 0; i < input; i++)
        {
            if (input / 2 == i)
            {
                emptyArray[i] = 1;
            }
            else
            {
                emptyArray[i] = 0;
            }
        }
        return emptyArray;
    }

    int[] Generator() // void Generator() -> int[] Generator()  for debugging
    {
        // for loop describing the grid
        //TODO: create the Grid generation
        // generate a string with a length of 3 , like "011" "111" ect
        //call objectplacement
        int[] nextGen = new int[gridWidth];

        for (int i = 0; i < gridWidth; i++)
        {
            /// New solution for the edges
            /// using modulus to prevent index out of bounds of the array
            int left = outArray[(i - 1 + gridWidth) % gridWidth];
            int mid = outArray[(i + gridWidth) % gridWidth];
            int right = outArray[(i + 1 + gridWidth) % gridWidth];

            strCompare = "" + left + mid + right;

            nextGen[i] = Rule(strCompare);

            /*
            if (i - 1 < 0)
            {
                Debug.Log("If");
                strCompare = "" + outArray[gridWidth - 1] + outArray[i] + outArray[i + 1];

            }
            else if (i == gridWidth - 1)
            {
                Debug.Log("Else If");
                strCompare = "" + outArray[i - 1] + outArray[i] + outArray[0];

            }
            else
            {
                Debug.Log("Else");
                strCompare = "" + outArray[i - 1] + outArray[i] + outArray[i + 1];

            }
            Debug.Log(i);
            */


        }

        //outArray = nextGen;
        //ObjectPlacement();

        return nextGen;


    }

    void ObjectPlacement()
    {

        //place objects within scene

        for (int i = 0; i < gridWidth; i++)
        {

        }





    }


    int[] ByteToRuleConverter(byte ruleNumber)
    //converting number to rule
    {
        BitArray b = new BitArray(new byte[] { ruleNumber });
        int[] bits = b.Cast<bool>().Select(bit => bit ? 1 : 0).ToArray();
        // used stack overflow to understand how to convert an int to a bit array
        // providing the same results as the rules for CellularAtomaton
        // couldnt figure out the syntax so had to look it up 

        Array.Reverse(bits); //simple yet effective reversing the order because the code above provided the numbers reverse

        return bits;
    }

    public int Rule(string lmr)
    {

        //current pattern	111	110	101	100	011	010	001	000
        //TODO : simplify   111	110	101	100	011	010	001	000 to 1-8 



        //using a string array for verifying the values

        // -1 for beeing stupic forgetting to add i++  creating crashes....
        for (int i = 0; i <= 7; i++) // could go for a foreach loop, but then manually decleare an int variable for the count
        {
            //Debug.Log(pattern[i] + " " + test + " " + temp);
            if (pattern[i] == lmr)
            {

                return inArray[i];
            }

        }
        return 0;
        /*
        if (right == 1 & middle == 1 & left == 1) { return temp[0]; }
        if (right == 1 & middle == 1 & left == 0) { return temp[1]; }
        if (right == 1 & middle == 0 & left == 1) { return temp[2]; }
        if (right == 1 & middle == 0 & left == 0) { return temp[3]; }
        if (right == 0 & middle == 1 & left == 1) { return temp[4]; }
        if (right == 0 & middle == 1 & left == 1) { return temp[5]; }
        if (right == 0 & middle == 0 & left == 1) { return temp[6]; }
        if (right == 0 & middle == 0 & left == 0) { return temp[7]; }
        */







    }






}
