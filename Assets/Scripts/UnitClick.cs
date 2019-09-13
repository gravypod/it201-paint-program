using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = System.Object;
using Random = UnityEngine.Random;

public class UnitClick : MonoBehaviour
{
    private const float PaintDrawDistance = 20.0f;
    public GameObject paintBlobTemplate;

    private GameObject CreateRandomizedPaintObject(
        Vector3 point,
        GameObject template,
        float minRed, float maxRed,
        float minGreen, float maxGreen,
        float minBlue, float maxBlue
    )
    {
        var color = new Color(
            Random.Range(minRed, maxRed),
            Random.Range(minGreen, maxGreen),
            Random.Range(minBlue, maxBlue)
        );
        var entity = Instantiate(template, point, Quaternion.identity);
        var mesh = entity.GetComponent<MeshRenderer>();
        mesh.material.color = color;
        return entity;
    }

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
        if (!Physics.Raycast(ray, out hit, PaintDrawDistance + 10))
            return;
        Destroy(hit.collider.gameObject);
    }

    private void UpdateDraw(Ray ray)
    {
        RaycastHit hit;

        // Assu
        var point = Physics.Raycast(ray, out hit, PaintDrawDistance)
            ? hit.point
            : ray.GetPoint(PaintDrawDistance);

        CreateRandomizedPaintObject(
            point,
            paintBlobTemplate,
            0, 1,
            0, 1,
            0, 1
        );
    }
}