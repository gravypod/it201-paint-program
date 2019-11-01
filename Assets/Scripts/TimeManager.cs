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

    // Use this for initialization
    void Start()
    {
        gameTime = DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {
        int speedMultiplier = (int) timeSpeedMultiplierSlider.value;
        var delta = getTimeSinceLastUpdate();
        for (int i = 0; i < speedMultiplier; i++)
            gameTime += delta;
    }
}