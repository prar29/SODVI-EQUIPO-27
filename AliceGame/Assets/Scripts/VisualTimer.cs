using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualTimer : MonoBehaviour
{
    public static VisualTimer Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private Slider timerSlider;

    void Start()
    {
        timerSlider = GetComponent<Slider>();
    }
    public void maxTimerValue(float timerMaxValue)
    {
        timerSlider.maxValue = timerMaxValue;
    }
    public void fillingTimer(float timerCurrentValor)
    {
        timerSlider.value = timerCurrentValor;
    }
    public void initiateTimer(float timerCurrentValor)
    {
        maxTimerValue(timerCurrentValor);
        fillingTimer(timerCurrentValor);
    }
}