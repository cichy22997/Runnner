using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProgress : MonoBehaviour
{
    public int distance;
    public int subs;
    public bool twoMilionAward;
    public int level;
    public int wave;
    public bool showAward;

    public static int distanceBuffer;
    public static int subsBuffer;
    public static int levelBuffer;
    public static int waveBuffer;
    public static bool twoMilionAwardBuffer;
    public static bool showAwardBuffer;

    public GameObject AwardPanel;

    public static PlayerProgress Instance { set; get; }

    public Text totalSubs, maxDistance, maxLevel, maxWave;

    private void Awake()
    {
        Instance = this;
        ProgressUpdate();
    }

    private void Update()
    {
        totalSubs.text = subs.ToString();
        maxDistance.text = distance.ToString();
        maxLevel.text = "Level " + level.ToString();
        maxWave.text = "Wave " + wave.ToString();

        if (subs >= 2000000)
        {
            twoMilionAward = true;
            if (showAwardBuffer)
                {
                GainAward();
                }
        }
        else
            showAwardBuffer = true;
    }

    private void GainAward()
    {

        showAwardBuffer = false;
        AwardPanel.SetActive(true);
    }

    public void CloseAward()
    {
        AwardPanel.SetActive(false);
    }

    public void AddScore()
    {
        if (Score.Instance.distanceRace > distanceBuffer)
        {
            distanceBuffer = Score.Instance.distanceRace;
        }

        subsBuffer += Score.Instance.subs;


    }

    public void AddScoreThrower()
    {

        if (ScoreThrower.Instance.level >= levelBuffer)
        {
            levelBuffer = ScoreThrower.Instance.level;
            if (ScoreThrower.Instance.wave > waveBuffer)
            {
                waveBuffer = ScoreThrower.Instance.wave;
            }
        }

            subsBuffer += ScoreThrower.Instance.totalScore;
    }
    public void ProgressUpdate()
    {
        distance = distanceBuffer;
        level = levelBuffer;
        wave = waveBuffer;
        subs = subsBuffer;
        twoMilionAward = twoMilionAwardBuffer;
        showAward = showAwardBuffer;
    }


    public void SaveProgress()
    {
        SavingScript.SaveProgress(this);
    }

    public void LoadProgress()
    {
 
        string path = Application.persistentDataPath + "/player.dont";
        if (File.Exists(path))
        {
            PlayerData data = SavingScript.LoadProgress();
            distanceBuffer = data.distance;
            subsBuffer = data.subs;
            twoMilionAwardBuffer = data.twoMilionAward;
            waveBuffer = data.wave;
            levelBuffer = data.level;
            showAwardBuffer = data.showAward;


        }
        else
        {
            SaveProgress();
        }


        ProgressUpdate();
    }
}
