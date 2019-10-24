using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    private DateTime currentTime = DateTime.Now;
    public Transform hoursTransform;
    public Transform minutesTransform;
    public Transform secondsTransform;

    float HourRotation()
    {
        float hour = currentTime.Hour;
        return 360f * (hour / 24f);
    }

    float MinuteRotation()
    {
        float minute = currentTime.Minute;
        return 360f * (minute / 60f);
    }

    float SecondRotation()
    {
        float second = currentTime.Second;
        return 360f * (second / 60f);
    }

    private void Awake()
    {
        Debug.Log(DateTime.Now);
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = DateTime.Now;
        hoursTransform.localRotation = Quaternion.Euler(0f, HourRotation(), 0f);
        minutesTransform.localRotation = Quaternion.Euler(0f, MinuteRotation(), 0f);
        secondsTransform.localRotation = Quaternion.Euler(0f, SecondRotation(), 0f);
    }
}