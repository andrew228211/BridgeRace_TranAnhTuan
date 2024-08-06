using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step : MonoBehaviour
{

    [SerializeField] MeshRenderer _meshRender;
    [SerializeField] private TypeColor _color = TypeColor.none;
    private bool isCollected = false;
    public bool IsCollected
    {
        get { return isCollected; }
        set { isCollected = value; }
    }
    public TypeColor GetColor()
    {
        return _color;
    }
    public void SetColor(Material materialColor, TypeColor _color)
    {
        this._color = _color;
        _meshRender.material = materialColor;
    }
}
