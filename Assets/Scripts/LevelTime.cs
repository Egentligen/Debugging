using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelTime : MonoBehaviour
{
    [Header("GameOverCanvas")]
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] TextMeshProUGUI timeSurvivedDisplay;
    [Header("TimeDisplay")]
    [SerializeField] TextMeshProUGUI timeDisplay;

    float time;

    void Start()
    {
        gameOverCanvas.SetActive(false);
    }

    void Update()
    {
        Timer();
    }

    public void DisplayTimeSurvived() 
    {
        gameOverCanvas.SetActive(true);

        timeSurvivedDisplay.text = "Time Survived: " + timeDisplay.text;
        timeDisplay.text = string.Empty;
    }

    void Timer()
    {
        time += Time.deltaTime;

        float second = Mathf.FloorToInt(time / 60f);
        float minute = Mathf.FloorToInt(time % 60f);
        timeDisplay.text = string.Format("{0:00}:{1:00}", second, minute);
    }
}
