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

    private UnitUi unit;
    private Canvas canvas;
    private Vector3 mousePosition = Vector3.one;

    private bool drawScheduled, removeScheduled;

    private bool CanDraw()
    {
        if (unit.IsRapidFire())
        {
            return Input.GetMouseButton(0);
        }
        else
        {
            return Input.GetMouseButtonDown(0);
        }
    }

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

        foreach (MeshRenderer mesh in entity.GetComponentsInChildren<MeshRenderer>())
        {
            mesh.material.color = color;

            // Magic from unity forum
            mesh.material.EnableKeyword("_EMISSION");
            mesh.material.SetColor("_EmissionColor", new Color(1.0f, 1.0f, 1.0f, 1.0f) * glow);
        }


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
        canvas = FindObjectOfType<Canvas>();
        unit = canvas.GetComponent<UnitUi>();
    }

    private void Update()
    {
        if (!drawScheduled)
            drawScheduled = CanDraw();

        if (!removeScheduled)
            removeScheduled = Input.GetMouseButtonDown(1);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        var c = camera.GetComponent<Camera>();
        var mouse = Input.mousePosition;
        var ray = c.ScreenPointToRay(mouse);

        if (removeScheduled)
            UpdateErase(ray);

        if (drawScheduled)
            UpdateDraw(ray);

        // Clear queued schedules
        mousePosition = Input.mousePosition;
        drawScheduled = removeScheduled = false;
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
        var point = ray.GetPoint(PaintDrawDistance);

        if (!unit.IsPaintBrushMode())
        {
            // If we are not in paint brush mode, raytrace and test physical collisions
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, PaintDrawDistance, LayerMask.NameToLayer("UI")))
            {
                point = hit.point;
            }
        }

        DrawAt(point);
    }

    private void DrawAt(Vector3 point)
    {
        var rgbColorRange = unit.GetColorMaximums();

        var scaleRange = unit.GetSizeMaximums();
        var alpha = unit.GetAlpha();
        var glow = unit.GetGlow();
        var minScales = new float[] {0.0001f, 0.0001f, 0.0001f};

        if (unit.IsPaintBrushMode())
        {
            var diff = (Input.mousePosition - mousePosition).normalized;
            
            scaleRange = new[]
            {
                0.0f, 0.0f, 0.0f
            };
            minScales = new[]
            {
                diff.x, diff.y, 1.0f,
            };
        }

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

        var puppet = paintObject.GetComponent<Puppet>();
        if (puppet != null)
            unit.ConfigureAnimationSettings(puppet);
        else
            Debug.Log("No animation");
        if (unit.IsTimedDestroyEnabled())
            Destroy(paintObject, TimedDestroyDelay);
    }
}