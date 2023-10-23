using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlagManager : MonoBehaviour
{
    public List<Sprite> flags;
    public List<Image> collection;
    public Sprite lockedFlag;

    public Image flagPrefab;
    public RectTransform contentPos;

    public GameObject unlockCountryObj;
    public Image countryOpened;

    Points points;

    int pointsPerFlag;
    private void Start()
    {
        points = FindObjectOfType<Points>();
        pointsPerFlag = 10; // Number of points required per flag
        for (int i = 0; i < collection.Count; i++)
        {
            Image flag = Instantiate(flagPrefab, contentPos);
            collection[i] = flag;
            flag.sprite = lockedFlag;
        }
    }
    private void Update()
    {
        ActivateFlags();
        UnlockedCountry();
    }
    void ActivateFlags()
    {
        for (int i = 0; i < flags.Count; i++)
        {
            if ((i + 1) * pointsPerFlag <= points.highscore)
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
    public void UnlockedCountry()
    {
        for (int i = 0; i < flags.Count; i++)
        {
            if ((i + 1) * pointsPerFlag == points.highscore)
            {
                unlockCountryObj.SetActive(true);
                countryOpened.sprite = flags[i];
            }
            else
            {
                unlockCountryObj.SetActive(false);
            }
        }
    }
}