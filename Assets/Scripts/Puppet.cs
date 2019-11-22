using System;
using UnityEngine;

public class Puppet : MonoBehaviour
{
    public enum MotionStyle
    {
        Scale,
        Rotation,
        Position,
    }

    public MotionStyle style;
    public UnitUi.PaintObjectType type;
    private Animation[] _animations;

    private void Start()
    {
        _animations = GetComponentsInChildren<Animation>();
    }

    void ConfigureSingleAnimation(Animation a)
    {
        switch (style)
        {
            case MotionStyle.Scale:
                a.sourceScaleX = Animation.MotionSource.Cosine;
                a.sourceScaleY = Animation.MotionSource.Cosine;
                a.sourceScaleZ = Animation.MotionSource.Cosine;
                break;
            case MotionStyle.Position:
                a.sourceX = Animation.MotionSource.Cosine;
                a.sourceY = Animation.MotionSource.Sine;
                break;
            case MotionStyle.Rotation:
                a.sourceRotationX = Animation.MotionSource.Sine;
                a.sourceRotationY = Animation.MotionSource.Cosine;
                a.sourceRotationZ = Animation.MotionSource.Sine;
                break;
        }
    }

    private void FixedUpdate()
    {
        switch (type)
        {
            // All of the single-object types.
            case UnitUi.PaintObjectType.Cube:
            case UnitUi.PaintObjectType.Cylinder:
            case UnitUi.PaintObjectType.Sphere:
                ConfigureSingleAnimation(_animations[0]);
                break;
            default:
                Debug.Log("Unknown object placed");
                break;
        }
    }
}