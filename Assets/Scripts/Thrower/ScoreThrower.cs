using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreThrower : MonoBehaviour
{
    public static ScoreThrower Instance { get; set; }

    public Text multiplierText, TotalScoreText;

    public float subs;
    public int totalScore;
    public float multiplier;
    public int wave;
    public int level;

    private void Start()
    {
        Instance = this;
        multiplier = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        level = WaveSpawner.Instance.countLevel;
        wave = WaveSpawner.Instance.countWave;
        totalScore = (int)(subs * multiplier * 10);
        UpdateScore();   
    }

    private void UpdateScore()
    {
        TotalScoreText.text = "Total " + totalScore.ToString();
        multiplierText.text = "x" + multiplier.ToString("0.0");
    }

    public void ProgressUpdate()
    {
        PlayerProgress.Instance.AddScoreThrower();

    }
}
