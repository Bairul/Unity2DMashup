using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class XPBar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI levelText;

    public void SetMaxXP(float xp)
    {
        slider.maxValue = xp;
        slider.value = 0;
    }

    public void SetXP(float xp)
    {
        slider.value = xp;
    }
}
