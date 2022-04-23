using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHP(int HP)
    {
        slider.maxValue = HP;
        slider.value = HP;
    }

    public void SetHP(int HP)
    {
        slider.value = HP;
    }
}
