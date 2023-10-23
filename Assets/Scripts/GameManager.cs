using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Screens")]
    public GameObject menu;
    public GameObject loadingScreen;
    public GameObject gameScn1;
    public GameObject gameScn2;
    public GameObject gameOverSreen;

    [Header("Language")]
    public Lang currentLanguage = Lang.usa;
    public Sprite[] langSprites;

    [Header("Feedback")]
    public Text feedback;

    [Header("Sound/Audio")]
    public GameObject soundObject;
    public AudioSource bgMusic;
    public Sprite mute;
    public Sprite unmute;

    bool isMute;
    public bool isGameOver = false;

    private void Start()
    {
        isMute = false;
    }
    private void Update()
    {
        bgMusic.mute = isMute ? true : false;
        gameOverSreen.SetActive(isGameOver);
    }
    public void GameOver(bool gameOver)
    {
        isGameOver = gameOver;
    }
    // Retry Game
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameScn2.SetActive(true);
        gameScn2.SetActive(true);
        menu.SetActive(false);
        loadingScreen.SetActive(false);
    }
    // Reset Game Scene
    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    // Feedback Send
    public void SendFeedback()
    {
        string feed = feedback.text.ToString();

        // send the feedback

    }
    // Sound Settings
    public void SoundSetting()
    {
        isMute = !isMute;

        soundObject.GetComponent<Image>().sprite = isMute ? mute : unmute;
    }
    // Change Language
    public void ChangeLang()
    {
        switch (langSprites.Length)
        {
            case 0:
                currentLanguage = Lang.usa;
                break;
            case 1:
                currentLanguage = Lang.portugal;
                break;
            case 2:
                currentLanguage = Lang.france;
                break;
            default:
                break;
        }
    }
    void France()
    {

    }
    void Portugal()
    {

    }
    void Usa()
    {

    }
    // Quit Game
    public void Quit()
    {
        Application.Quit();
    }
}
public enum Lang
{
    usa, portugal, france
}