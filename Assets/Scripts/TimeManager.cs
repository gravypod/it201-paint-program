using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public DateTime gameTime;
    private int timeSpeedMultiplier = 1;
    private DateTime _lastUpdateTime = DateTime.Now;

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
        var delta = getTimeSinceLastUpdate();
        for (int i = 0; i < timeSpeedMultiplier; i++)
            gameTime += delta;
    }
}