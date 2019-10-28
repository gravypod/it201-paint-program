using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public DateTime gameTime;
    public Transform hoursTransform;
    public Transform minutesTransform;
    public Transform secondsTransform;

    float HourRotation()
    {
        float hour = gameTime.Hour;
        return 360f * (hour / 24f);
    }

    float MinuteRotation()
    {
        float minute = gameTime.Minute;
        return 360f * (minute / 60f);
    }

    float SecondRotation()
    {
        float second = gameTime.Second;
        return 360f * (second / 60f);
    }


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        gameTime = GameObject.Find("Time").GetComponent<TimeManager>().gameTime;
        hoursTransform.localRotation = Quaternion.Euler(0f, HourRotation(), 0f);
        minutesTransform.localRotation = Quaternion.Euler(0f, MinuteRotation(), 0f);
        secondsTransform.localRotation = Quaternion.Euler(0f, SecondRotation(), 0f);
    }
}