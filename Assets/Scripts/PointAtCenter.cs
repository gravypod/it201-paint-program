using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAtCenter : MonoBehaviour
{
    public Transform sunTransform;

    private Transform copyOfOriginal;

    private void CopyTransform(Transform src, Transform dst)
    {
        dst.position = src.position;
        dst.rotation = src.rotation;

        dst.localPosition = src.localPosition;
        dst.localRotation = src.localRotation;
    }

    // Use this for initialization
    void Start()
    {
        copyOfOriginal = new GameObject().transform;
        CopyTransform(sunTransform, copyOfOriginal);
    }

    // Update is called once per frame
    void Update()
    {
        var offsetToCorrectRotation = 270f;
        var gameTime = GameObject.Find("Time").GetComponent<TimeManager>().gameTime;
        float secondsIntoDay = (float) gameTime.TimeOfDay.TotalSeconds;
        var secondsInFullDay = 24 * 60 * 60;
        var rotation = ((secondsIntoDay / secondsInFullDay) * 360f) + offsetToCorrectRotation;
        var center = new Vector3(320, 0, 0);
        
        CopyTransform(copyOfOriginal, sunTransform);
        sunTransform.RotateAround(center, new Vector3(1, 0, 0), rotation);
        sunTransform.LookAt(center);
    }
}