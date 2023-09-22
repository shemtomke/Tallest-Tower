using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public Slider loadingSlider;
    public Text loadingPercent;

    public GameObject mainMenu;
    public GameObject loading;

    int currentValue;
    private void Start()
    {
        mainMenu.SetActive(false);
        currentValue = 0;
        StartCoroutine(LoadGame());
    }
    private void Update()
    {
        loadingPercent.text = "" + currentValue + "%";
        loadingSlider.value = currentValue;
    }
    IEnumerator LoadGame()
    {
        while (currentValue < 100) 
        {
            currentValue += 10;
            yield return new WaitForSeconds(0.5f);
        }

        loading.SetActive(false);
        mainMenu.SetActive(true);
    }
}
