using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlagManager : MonoBehaviour
{
    public List<Sprite> flags;
    public List<Image> collection;
    public Sprite lockedFlag;

    Points points;
    private void Start()
    {
        points = FindObjectOfType<Points>();

        for (int i = 0; i < collection.Count; i++)
        {
            collection[i].sprite = lockedFlag;
        }
    }
    private void Update()
    {
        ActivateFlags();
    }
    void ActivateFlags()
    {
        int pointsPerFlag = 10; // Number of points required per flag

        for (int i = 0; i < flags.Count; i++)
        {
            if (i * pointsPerFlag <= points.currentPoints)
            {
                // If the player has earned enough points for this flag, display it
                collection[i].sprite = flags[i];
            }
            else
            {
                // If not enough points, display the lockedFlag
                collection[i].sprite = lockedFlag;
            }
        }
    }
}