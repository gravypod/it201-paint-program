using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;
using Random = UnityEngine.Random;

public class UnitClick : MonoBehaviour
{
    private const float TimedDestroyDelay = 3.0f;
    private const float PaintDrawDistance = 50.0f;
    public GameObject camera;
    public Text mousePositionText;

    private GameObject CreateRandomizedPaintObject(
        Vector3 point,
        GameObject template,
        float alpha, float glow,
        float minRed, float maxRed,
        float minGreen, float maxGreen,
        float minBlue, float maxBlue,
        float minXScale, float maxXScale,
        float minYScale, float maxYScale,
        float minZScale, float maxZScale
    )
    {
        var color = new Color(
            Random.Range(minRed, maxRed),
            Random.Range(minGreen, maxGreen),
            Random.Range(minBlue, maxBlue),
            alpha
        );
        var entity = Instantiate(template, point, Quaternion.identity);

        var mesh = entity.GetComponent<MeshRenderer>();
        mesh.material.color = color;

        // Magic from unity forum
        mesh.material.EnableKeyword("_EMISSION");
        mesh.material.SetColor("_EmissionColor", new Color(1.0f, 1.0f, 1.0f, 1.0f) * glow);

        var tran = entity.GetComponent<Transform>();
        tran.localScale = new Vector3(
            Random.Range(minXScale, maxXScale),
            Random.Range(minYScale, maxYScale),
            Random.Range(minZScale, maxZScale)
        );

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
        var c = camera.GetComponent<Camera>();
        var mouse = Input.mousePosition;
        var ray = c.ScreenPointToRay(mouse);

        if (Input.GetMouseButtonDown(1))
            UpdateErase(ray);

        if (Input.GetMouseButtonDown(0))
            UpdateDraw(ray);

        mousePositionText.text = string.Format("Mouse Position: x {0}, y: {1}", mouse.x, mouse.y);
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
        var point = Physics.Raycast(ray, out hit, PaintDrawDistance, LayerMask.NameToLayer("UI"))
            ? hit.point
            : ray.GetPoint(PaintDrawDistance);

        Debug.DrawRay(ray.origin, point, Color.white);

        DrawAt(point);
    }

    private void DrawAt(Vector3 point)
    {
        var canvas = FindObjectOfType<Canvas>();
        var unit = canvas.GetComponent<UnitUi>();

        if (unit == null)
            return;
        
        var rgbColorRange = unit.GetColorMaximums();

        var scaleRange = unit.GetSizeMaximums();
        var alpha = unit.GetAlpha();
        var glow = unit.GetGlow();
        var minScales = new float[] {0.0001f, 0.0001f, 0.0001f};

        var paintObject = CreateRandomizedPaintObject(
            point,
            unit.GetPaintObjectTemplate(),
            alpha, glow,
            0, rgbColorRange[0],
            0, rgbColorRange[1],
            0, rgbColorRange[2],
            minScales[0], scaleRange[0] + minScales[0],
            minScales[1], scaleRange[1] + minScales[1],
            minScales[2], scaleRange[2] + minScales[2]
        );

        if (unit.IsTimedDestroyEnabled())
            Destroy(paintObject, TimedDestroyDelay);
    }
}