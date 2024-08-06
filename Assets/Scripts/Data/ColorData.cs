using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorData", menuName = "ScriptableObjects/Color")]
public class ColorData : ScriptableObject
{
    [SerializeField] private Material[] materials;
    public Material GetMat(TypeColor colorType)
    {
        return materials[(int)colorType];
    }
}
public enum TypeColor
{
    none=0,
    red=1,
    green=2,
    yellow=3,
    blue=4,
}
