using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitUi : MonoBehaviour
{
    private enum PaintObjectType
    {
        Sphere,
        Cube,
        Cylinder
    }

    public Toggle timedDestroyToggle;
    public Slider redSlider, greenSlider, blueSlider, scaleSlider, alphaSlider, emissionSlider;

    public Dropdown paintObjectDropdown;
    public GameObject spherePaintObjectTemplate;
    public GameObject cubePaintObjectTemplate;
    public GameObject cylinderPaintObjectTemplate;

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

    public void OnClearPressed()
    {
        Debug.Log("[Clear] - Pressed");

        foreach (var paintObject in GameObject.FindGameObjectsWithTag("PaintObject"))
        {
            Destroy(paintObject);
        }
    }
}