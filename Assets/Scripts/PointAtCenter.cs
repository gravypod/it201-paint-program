using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAtCenter : MonoBehaviour
{
    public Transform sunTransform;
    
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var center = new Vector3(320, 0, 0);
        
        sunTransform.RotateAround(center, new Vector3(1, 0, 0), 20 * Time.deltaTime);
        // Look towards the camera
        sunTransform.LookAt(center);
    }
}