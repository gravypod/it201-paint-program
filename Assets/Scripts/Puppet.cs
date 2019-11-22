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
                switch (type)
                {
                    case UnitUi.PaintObjectType.Sphere:
                        a.sourceScaleX = Animation.MotionSource.Cosine;
                        a.sourceScaleY = Animation.MotionSource.Cosine;
                        a.sourceScaleZ = Animation.MotionSource.Cosine;
                        a.speed = 0.5f;
                        break;
                    case UnitUi.PaintObjectType.Cube:
                        a.sourceScaleX = Animation.MotionSource.Sine;
                        a.sourceScaleY = Animation.MotionSource.Cosine;
                        a.sourceScaleZ = Animation.MotionSource.Sine;
                        a.speed = 3;
                        break;
                    case UnitUi.PaintObjectType.Cylinder:
                        a.sourceScaleX = Animation.MotionSource.Sine;
                        a.sourceScaleY = Animation.MotionSource.Sine;
                        a.sourceScaleZ = Animation.MotionSource.Sine;
                        a.speed = 1.5f;
                        break;
                    default:
                        Debug.Log("Unsuported ConfigureSingleAnimation /w Scale");
                        break;
                }

                break;
            case MotionStyle.Position:
                switch (type)
                {
                    case UnitUi.PaintObjectType.Sphere:
                        a.sourceX = Animation.MotionSource.Sine;
                        a.sourceY = Animation.MotionSource.Cosine;
                        a.speed = 2f;
                        break;
                    case UnitUi.PaintObjectType.Cube:
                        a.sourceX = Animation.MotionSource.Cosine;
                        a.sourceY = Animation.MotionSource.Cosine;
                        a.speed = 1f;
                        break;
                    case UnitUi.PaintObjectType.Cylinder:
                        a.sourceX = Animation.MotionSource.Cosine;
                        a.sourceY = Animation.MotionSource.Sine;
                        a.speed = 0.5f;
                        break;
                    default:
                        Debug.Log("Unsuported ConfigureSingleAnimation /w Position");
                        break;
                }
                break;
            case MotionStyle.Rotation:
                switch (type)
                {
                    case UnitUi.PaintObjectType.Sphere:
                        a.sourceRotationX = Animation.MotionSource.Sine;
                        a.sourceRotationY = Animation.MotionSource.Cosine;
                        a.sourceRotationZ = Animation.MotionSource.Sine;
                        a.speed = 10f;
                        break;
                    case UnitUi.PaintObjectType.Cube:
                        a.sourceRotationX = Animation.MotionSource.Tangent;
                        a.sourceRotationY = Animation.MotionSource.Sine;
                        a.sourceRotationZ = Animation.MotionSource.Sine;
                        a.speed = 1f;
                        break;
                    case UnitUi.PaintObjectType.Cylinder:
                        a.sourceRotationX = Animation.MotionSource.Cosine;
                        a.sourceRotationY = Animation.MotionSource.Sine;
                        a.sourceRotationZ = Animation.MotionSource.Cosine;
                        a.speed = 1f;
                        break;
                    default:
                        Debug.Log("Unsuported ConfigureSingleAnimation /w Position");
                        break;
                }
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