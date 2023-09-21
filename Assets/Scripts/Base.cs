using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Base : MonoBehaviour
{
    // set the base to hold the blocks to be same as the screen size for the bottom
    private void Update()
    {
        BasePosition();
    }
    void BasePosition()
    {
        // Get the bottom Y-axis position in world coordinates
        float bottomY = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;

        // Set the position of the Base object to be at the bottom of the screen
        Vector3 newPosition = transform.position;
        newPosition.y = bottomY;
        transform.position = newPosition;
    }
}