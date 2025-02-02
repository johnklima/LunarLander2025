using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class StarScript : MonoBehaviour
{
    public float totalStars;
    public int maxStars = 5;
    public int topScore;
    public int currentScore;
    public string numberOfStars ="";
    public int maxScore = 1000;


    string Stars(int topScore, int currentScore)
    {
        // 3/(TopScore /score)

        totalStars = maxStars / (topScore / currentScore);
        numberOfStars = String.Concat(Enumerable.Repeat("* ", (int)totalStars-1));


        return numberOfStars; //TODO: send data to UI  Trigger script when you complete the level
    
    }

  

}
