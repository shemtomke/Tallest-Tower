using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour
{
    public Text pointsText;
    public int currentPoints = 0;

    private void Update()
    {
        pointsText.text = "" + currentPoints;
    }
    public int FallenBlockPoints()
    { return currentPoints++; }

    // When a player reaches a certain number of points, one of the flags is opened. Every 10 points unlock a new flag.

}