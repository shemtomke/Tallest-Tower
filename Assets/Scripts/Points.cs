using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour
{
    public Text[] pointsText;
    public Text[] bestScoreText;

    public int currentPoints = 0;
    public int highscore = 0;

    string highScoreSave = "HIGHSCORE";
    private void Start()
    {
        UpdateHighScoreText();
    }
    private void Update()
    {
        GetHighScore();
        UpdateScoreText();
    }
    public int FallenBlockPoints()
    { return currentPoints++; }
    void GetHighScore()
    {
        if (currentPoints > highscore)
        {
            highscore = currentPoints;
            PlayerPrefs.SetInt(highScoreSave, highscore);
            PlayerPrefs.Save();
            UpdateHighScoreText();
        }
    }
    // Reset the current score
    public void ResetScore()
    {
        currentPoints = 0;
        UpdateScoreText();
    }

    // Reset the high score
    public void ResetHighScore()
    {
        highscore = 0;
        PlayerPrefs.SetInt(highScoreSave, highscore);
        PlayerPrefs.Save();
        UpdateHighScoreText();
    }

    // Update the UI text for the current score
    private void UpdateScoreText()
    {
        for (int i = 0; i < pointsText.Length; i++)
        {
            pointsText[i].text = "" + currentPoints;
        }
    }

    // Update the UI text for the high score
    private void UpdateHighScoreText()
    {
        for (int i = 0; i < bestScoreText.Length; i++)
        {
            bestScoreText[i].text = "" + highscore;
        }
    }
}