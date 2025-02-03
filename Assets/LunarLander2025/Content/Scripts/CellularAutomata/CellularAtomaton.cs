using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using JetBrains.Annotations;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using static Unity.VisualScripting.Member;

public class CellularAtomaton : MonoBehaviour
{

    // rules 0-255
    
    public byte ruleNumb = 0;
    public int[] inArray;
    public int[] outArray;
    public string[] pattern = { "111", "110", "101", "100", "011", "010", "001", "000" };
    public int generation;
    public int gridWidth;
    public string strCompare;
    private void Start()
    {
        inArray = ByteToRuleConverter(ruleNumb);
    }

    void Generation()
    {
        // for loop describing the grid
        //TODO: create the Grid generation
        // generate a string with a length of 3 , like "011" "111" ect
        //call objectplacement
        int[] nextGen = new int[gridWidth];

        for (int grid = 0; grid < gridWidth; grid++)
        {
            if (grid - 1 < 0)
            {
                strCompare = "" + inArray[gridWidth] + inArray[grid] + inArray[grid + 1];
            }
            else if (grid + 1 > gridWidth)
            {
                strCompare = "" + inArray[grid - 1] + inArray[grid] + inArray[0];

            }
            else
            {
                strCompare = "" + inArray[grid - 1] + inArray[grid] + inArray[grid +1];
            }
            nextGen[grid] = Rule(strCompare);
        }
    }

    void ObjectPlacement()
    {

        //place objects within scene
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
        //converting input to string
        


        //current pattern	111	110	101	100	011	010	001	000
        //TODO : simplify   111	110	101	100	011	010	001	000 to 1-8 

        

        //using a string array for verifying the values
       

        for (int i  = 0; i < pattern.Length;) // could go for a foreach loop, but then manually decleare an int variable for the count
        {
            //Debug.Log(pattern[i] + " " + test + " " + temp);
            if (pattern[i] == lmr ) return inArray[i]; 
           
        }
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


        

        return 0;


    }
    





}
