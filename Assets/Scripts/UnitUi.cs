using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class UnitUi : MonoBehaviour
{
    public enum PaintObjectType
    {
        Sphere,
        Cube,
        Cylinder,
        SphereCube,
        CubeCylinder,
        CylinderCylinder,
    }

    public Toggle timedDestroyToggle, animationRandomizeToggle, paintBrushMode, rapidFire;
    public Slider redSlider, greenSlider, blueSlider, scaleSlider, alphaSlider, emissionSlider, animationSlider;

    public Dropdown animationSelection;

    public Dropdown paintObjectDropdown;
    public GameObject spherePaintObjectTemplate;
    public GameObject cubePaintObjectTemplate;
    public GameObject cylinderPaintObjectTemplate;
    public GameObject sphereCubePaintObjectTemplate;
    public GameObject cubeCylinderPaintObjectTemplate;
    public GameObject cylinderCylinderPaintObjectTemplate;

    // Use this for initialization
    void Start()
    {
        var options = new List<Dropdown.OptionData>();

        foreach (var paintObjectTypeName in Enum.GetNames(typeof(PaintObjectType)))
            options.Add(new Dropdown.OptionData(paintObjectTypeName));

        paintObjectDropdown.options = options;
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// Get the paint object currently selected for drawing.
    /// </summary>
    /// <returns>GameObject that should be cloned to create a new paint object.</returns>
    public GameObject GetPaintObjectTemplate()
    {
        var selected = paintObjectDropdown.value;
        var text = paintObjectDropdown.options[selected].text;
        PaintObjectType type;

        if (text == null)
        {
            type = PaintObjectType.Sphere;
        }
        else
        {
            Debug.Log(text);
            type = (PaintObjectType) Enum.Parse(typeof(PaintObjectType), text);
        }

        switch (type)
        {
            case PaintObjectType.Sphere:
                return spherePaintObjectTemplate;
            case PaintObjectType.Cube:
                return cubePaintObjectTemplate;
            case PaintObjectType.Cylinder:
                return cylinderPaintObjectTemplate;
            case PaintObjectType.SphereCube:
                return sphereCubePaintObjectTemplate;
            case PaintObjectType.CubeCylinder:
                return cubeCylinderPaintObjectTemplate;
            case PaintObjectType.CylinderCylinder:
                return cylinderCylinderPaintObjectTemplate;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    /// <summary>
    /// Find the maximum selected R, G, and B value for color generation.
    /// </summary>
    /// <returns>float[] with {R, G, B}</returns>
    public float[] GetColorMaximums()
    {
        return new float[]
        {
            redSlider.normalizedValue,
            greenSlider.normalizedValue,
            blueSlider.normalizedValue
        };
    }

    public float GetAlpha()
    {
        return alphaSlider.value;
    }

    public float GetGlow()
    {
        return emissionSlider.value;
    }

    public float[] GetSizeMaximums()
    {
        var value = scaleSlider.value;
        return new[] {value, value, value};
    }

    public bool IsTimedDestroyEnabled()
    {
        return timedDestroyToggle.isOn;
    }

    public bool IsPaintBrushMode()
    {
        return paintBrushMode.isOn;
    }

    public bool IsRapidFire()
    {
        return rapidFire.isOn;
    }

    public void OnClearPressed()
    {
        Debug.Log("[Clear] - Pressed");

        foreach (var paintObject in GameObject.FindGameObjectsWithTag("PaintObject"))
        {
            Destroy(paintObject);
        }
    }

    public void ConfigureAnimationSettings(Puppet puppet)
    {
        int selectedOption = animationSelection.value;

        if (animationRandomizeToggle.isOn)
        {
            selectedOption = UnityEngine.Random.Range(0, animationSelection.options.Count);
        }

        switch (animationSelection.options[selectedOption].text)
        {
            case "Position":
                puppet.style = Puppet.MotionStyle.Position;
                break;
            case "Rotation":
                puppet.style = Puppet.MotionStyle.Rotation;
                break;
            case "Scale":
                puppet.style = Puppet.MotionStyle.Scale;
                break;
            default:
                Debug.Log("ERROR: Unknown animation selected");
                break;
        }


        foreach (Animation animation in puppet.GetComponentsInChildren<Animation>())
        {
            animation.speed = animationSlider.value;
        }
    }
}