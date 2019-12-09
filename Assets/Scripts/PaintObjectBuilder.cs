using UnityEditor;
using UnityEngine;

public class PaintObjectBuilder : MonoScript
{
    private readonly GameObject _template;
    private float _x, _y, _z;
    private float _red, _green, _blue;
    private Vector3 _position;
    private float _alpha, _glow;

    public PaintObjectBuilder(GameObject template)
    {
        this._template = template;
    }

    public PaintObjectBuilder SetPosition(Vector3 position)
    {
        this._position = position;
        return this;
    }

    public PaintObjectBuilder SetAlpha(float alpha)
    {
        this._alpha = alpha;
        return this;
    }

    public PaintObjectBuilder SetGlow(float glow)
    {
        this._glow = glow;
        return this;
    }


    public PaintObjectBuilder SetX(float x)
    {
        this._x = x;
        return this;
    }

    public PaintObjectBuilder SetY(float y)
    {
        this._y = y;
        return this;
    }

    public PaintObjectBuilder SetZ(float z)
    {
        this._z = z;
        return this;
    }

    public PaintObjectBuilder SetRed(float red)
    {
        this._red = red;
        return this;
    }

    public PaintObjectBuilder SetGreen(float green)
    {
        this._green = green;
        return this;
    }

    public PaintObjectBuilder SetBlue(float blue)
    {
        this._blue = blue;
        return this;
    }

    public PaintObjectBuilder SetXRange(float min, float max)
    {
        return this.SetX(Random.Range(min, max));
    }


    public PaintObjectBuilder SetYRange(float min, float max)
    {
        return this.SetY(Random.Range(min, max));
    }

    public PaintObjectBuilder SetZRange(float min, float max)
    {
        return this.SetZ(Random.Range(min, max));
    }


    public PaintObjectBuilder SetRedRange(float min, float max)
    {
        return this.SetRed(Random.Range(min, max));
    }


    public PaintObjectBuilder SetGreenRange(float min, float max)
    {
        return this.SetGreen(Random.Range(min, max));
    }

    public PaintObjectBuilder SetBlueRange(float min, float max)
    {
        return this.SetBlue(Random.Range(min, max));
    }

    public GameObject Build()
    {
        var color = new Color(_red, _green, _blue, _alpha);
        var entity = Instantiate(_template, _position, Quaternion.identity);

        foreach (MeshRenderer mesh in entity.GetComponentsInChildren<MeshRenderer>())
        {
            mesh.material.color = color;

            // Magic from unity forum
            mesh.material.EnableKeyword("_EMISSION");
            mesh.material.SetColor("_EmissionColor", new Color(1.0f, 1.0f, 1.0f, 1.0f) * _glow);
        }

        var tran = entity.GetComponent<Transform>();
        tran.localScale = new Vector3(_x, _y, _z);

        return entity;
    }
}