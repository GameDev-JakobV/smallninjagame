using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LerpPointCreator))]
public class LerpEditor : Editor
{
    LerpPointCreator Creator;
    LerpController LerpCont;
    private List<Vector2> Points;

    private void OnEnable()
    {
        Creator = (LerpPointCreator)target;
        if (Creator._LerpController == null)
        {
            Creator.CreatePath();
        }
        LerpCont = Creator._LerpController;
        Points = LerpCont.Points;
    }

    private void OnSceneGUI()
    {
        Draw();
    }

    private void Draw()
    {
        Handles.DrawBezier(Points[0], Points[3], Points[1], Points[2], Color.green, null, 2);
    }

}
