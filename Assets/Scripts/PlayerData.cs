using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int distance;
    public int subs;
    public int level;
    public int wave;
    public bool twoMilionAward;
    public bool showAward;

    public PlayerData (PlayerProgress player)
    {
        distance = player.distance;
        subs = player.subs;
        level = player.level;
        wave = player.wave;
        twoMilionAward = player.twoMilionAward;
        showAward = player.showAward;

    }
}
