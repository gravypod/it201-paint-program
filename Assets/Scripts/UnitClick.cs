using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = System.Object;

public class UnitClick : MonoBehaviour
{
    public GameObject paintBlobTemplate;
    const float PAINT_DRAW_DISTANCE = 20.0f;

    // Use this for initialization
    private void Start()
    {
        Debug.Log("Starting UnitClick");
    }

    // Update is called once per frame
    private void Update()
    {
        var c = GetComponent<Camera>();
        var ray = c.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(1))
            UpdateErase(ray);

        if (Input.GetMouseButtonDown(0))
            UpdateDraw(ray);
    }

    private void UpdateErase(Ray ray)
    {
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit, PAINT_DRAW_DISTANCE + 10))
            return;
        Destroy(hit.collider.gameObject);
    }

    private void UpdateDraw(Ray ray)
    {
        RaycastHit hit;

        // Assu
        var point = Physics.Raycast(ray, out hit, PAINT_DRAW_DISTANCE)
            ? hit.point
            : ray.GetPoint(PAINT_DRAW_DISTANCE);

        var entity = Instantiate(this.paintBlobTemplate, point, Quaternion.identity);
        var mesh = entity.GetComponent<MeshRenderer>();
        mesh.material.color = Color.black;
    }
}