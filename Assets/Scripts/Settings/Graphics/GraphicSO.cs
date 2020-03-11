using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Graphic", menuName = "ScriptableObjects/GraphicSO", order = 1)]
public class GraphicSO : ScriptableObject
{
    public int graphicLevel;
    public FullScreenMode screenMode;
    public string resolution;
}
