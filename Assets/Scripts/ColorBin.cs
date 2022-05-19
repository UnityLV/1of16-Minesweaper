using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ColorBin",menuName = "ScriptableObject/ColorBin")] 
public class ColorBin : ScriptableObject
{
    [SerializeField] private Color[] _colors;
    public Color this[int index]=> _colors[index];
}
