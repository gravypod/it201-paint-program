using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitUi : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
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