using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    public enum MotionSource
    {
        None,
        Sine,
        Cosine,
        Tangent
    }

    public MotionSource sourceX = MotionSource.None, sourceY = MotionSource.None, sourceZ = MotionSource.None;

    public MotionSource sourceScaleX = MotionSource.None,
        sourceScaleY = MotionSource.None,
        sourceScaleZ = MotionSource.None;

    public MotionSource sourceRotationX = MotionSource.None,
        sourceRotationY = MotionSource.None,
        sourceRotationZ = MotionSource.None;

    public float speed = 1;

    private Quaternion rotationStart;
    private Vector3 positionStart;
    private Vector4 colorStart;
    private Vector3 scaleStart;
    private float time = 0.0f, sine = 0.0f, cosine = 0.0f, tangent = 0.0f;


    private float GetValue(MotionSource s)
    {
        switch (s)
        {
            case MotionSource.Cosine:
                return cosine;
            case MotionSource.Sine:
                return sine;
            case MotionSource.Tangent:
                return tangent;
            case MotionSource.None:
            default:
                return 0.0f;
        }
    }

    private void Start()
    {
        positionStart = transform.localPosition;
        scaleStart = transform.localScale;
        colorStart = GetComponent<MeshRenderer>().material.color;
        rotationStart = transform.localRotation;
    }

    Vector4 ColorChange()
    {
        var cosine = Mathf.Cos(Time.time);
        var sine = Mathf.Cos(Time.time);
        return colorStart + (new Vector4(cosine, sine, cosine, 1));
    }

    void Update()
    {
        time += (Time.deltaTime * speed);
        sine = Mathf.Sin(time);
        cosine = Mathf.Cos(time);
        tangent = Mathf.Tan(time);
        var rot = new Vector3(
                      GetValue(sourceRotationX) * 360f,
                      GetValue(sourceRotationY) * 360f,
                      GetValue(sourceRotationZ) * 360f
                  );
        var pos = new Vector3(GetValue(sourceX), GetValue(sourceY), GetValue(sourceZ));
        var scale = new Vector3(
            GetValue(sourceScaleX),
            GetValue(sourceScaleY),
            GetValue(sourceScaleZ)
        );

        if (sourceRotationX != MotionSource.None || sourceRotationY != MotionSource.None ||
            sourceRotationZ != MotionSource.None)
            transform.eulerAngles = rot;

        if (sourceScaleX != MotionSource.None || sourceScaleY != MotionSource.None ||
            sourceScaleZ != MotionSource.None)
            transform.localScale = scaleStart + scale;

        if (sourceX != MotionSource.None || sourceY != MotionSource.None ||
            sourceZ != MotionSource.None)
            transform.localPosition = positionStart + pos;
    }
}