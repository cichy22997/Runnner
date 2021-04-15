using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeAttackBar : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEngine.UI.Slider slider;


    public void SetTime(float time)
    {
        slider.value = time;
    }
}
