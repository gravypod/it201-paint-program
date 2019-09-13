using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitUi : MonoBehaviour
{
    public Toggle timedDestroyToggle;
    public Slider redSlider, greenSlider, blueSlider;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }


    /**    
     * Find the maximum color value (0.0f to 1.0f) for Red, Green, and Blue.
     * @return {red, green, blue}
     */
    public float[] GetColorMaximums()
    {
        return new float[]
        {
            redSlider.normalizedValue,
            greenSlider.normalizedValue,
            blueSlider.normalizedValue
        };
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