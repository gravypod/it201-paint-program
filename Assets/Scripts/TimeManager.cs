using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public DateTime gameTime;
    private DateTime _lastUpdateTime = DateTime.Now;
    public Slider timeSpeedMultiplierSlider;

    private TimeSpan getTimeSinceLastUpdate()
    {
        DateTime currentTime = DateTime.Now;
        TimeSpan offset = currentTime - _lastUpdateTime;
        _lastUpdateTime = currentTime;
        return offset;
    }

    private void ManageTimeHotKeys()
    {
        float delta;

        if (Input.GetKey("o"))
        {
            delta = -5;
        }
        else if (Input.GetKey("p"))
        {
            delta = +5;
        }
        else
        {
            delta = 0;
        }

        timeSpeedMultiplierSlider.value = Mathf.Clamp(
            timeSpeedMultiplierSlider.value + delta,
            timeSpeedMultiplierSlider.minValue,
            timeSpeedMultiplierSlider.maxValue
        );
    }

    // Use this for initialization
    void Start()
    {
        gameTime = DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {
        ManageTimeHotKeys();
        int speedMultiplier = (int) timeSpeedMultiplierSlider.value;
        var delta = getTimeSinceLastUpdate();
        for (int i = 0; i < speedMultiplier; i++)
            gameTime += delta;
    }
}