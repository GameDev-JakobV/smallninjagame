using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LerpController : MonoBehaviour
{
    public Transform[] Transforms;
    public GameObject[] MovingDot;
    public float Duration = 1f;
    public float StartValue = 0f;
    public List<Vector2> Points;


    private List<Transform> DotTrans;
    private Vector2 point, point1, point2, point3, point4, point5, point6;
    public bool ReachedEnd;
    public List<Vector2> Path;
    public bool PointsAdded = false;
    private LineRenderer lineRenderer;
    private bool done = false;

    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(MovePointAcrossCurve());
    }

    public LerpController(Vector2 centre)
    {
        Points = new List<Vector2>()
        {
            centre + Vector2.left,
            centre + (Vector2.left+Vector2.up)*0.5f,
            centre + (Vector2.right+Vector2.down)*0.5f,
            centre + Vector2.right
        };
    }

    private void Update()
    {

    }

    private static Vector2 QuadraticCurve(Vector2 a, Vector2 b, Vector2 c, float t)
    {
        Vector2 p0 = Vector2.Lerp(a, b, t);
        Vector2 p1 = Vector2.Lerp(b, c, t);
        return Vector2.Lerp(p0, p1, t);
    }

    private static Vector2 CubicBezierCurve(Vector2 a, Vector2 b, Vector2 c, Vector2 d, float t)
    {
        Vector2 p0 = QuadraticCurve(a, b, c, t);
        Vector2 p1 = QuadraticCurve(b, c, d, t);
        return Vector2.Lerp(p0, p1, t);
    }

    private IEnumerator MovePointAcrossCurve()
    {
        float TimeElapsed = 0f;
        while (TimeElapsed < Duration)
        {
            DotTrans[5].position = CubicBezierCurve(Transforms[0].position, Transforms[1].position, Transforms[2].position, Transforms[3].position, TimeElapsed / Duration);
            TimeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    private void DrawBezier()
    {
        SetLineRenderer();
        if (StartValue > 1)
        {
            ReachedEnd = true;
            PointsAdded = true;
        }
        if (StartValue < 0)
        {
            ReachedEnd = false;
        }
        if (!ReachedEnd)
        {
            StartValue += (Duration * Time.deltaTime);
            point = Vector2.Lerp(Transforms[0].position, Transforms[1].position, StartValue);
            DotTrans[0].position = point;
            point1 = Vector2.Lerp(Transforms[1].position, Transforms[2].position, StartValue);
            DotTrans[1].position = point1;
            point2 = Vector2.Lerp(Transforms[2].position, Transforms[3].position, StartValue);
            DotTrans[2].position = point2;
            point3 = Vector2.Lerp(point, point1, StartValue);
            DotTrans[3].position = point3;
            point4 = Vector2.Lerp(point1, point2, StartValue);
            DotTrans[4].position = point4;
            point5 = Vector2.Lerp(point3, point4, StartValue);
            DotTrans[5].position = point5;
            if (!PointsAdded)
            {
                Path.Add(point5);
            }
        }
        else if (ReachedEnd)
        {
            StartValue -= Duration * Time.deltaTime;
            point = Vector2.Lerp(Transforms[0].position, Transforms[1].position, StartValue);
            DotTrans[0].position = point;
            point1 = Vector2.Lerp(Transforms[1].position, Transforms[2].position, StartValue);
            DotTrans[1].position = point1;
            point2 = Vector2.Lerp(Transforms[2].position, Transforms[3].position, StartValue);
            DotTrans[2].position = point2;
            point3 = Vector2.Lerp(point, point1, StartValue);
            DotTrans[3].position = point3;
            point4 = Vector2.Lerp(point1, point2, StartValue);
            DotTrans[4].position = point4;
            point5 = Vector2.Lerp(point3, point4, StartValue);
            DotTrans[5].position = point5;
        }
    }

    private void SetLineRenderer()
    {
        if (PointsAdded && !done)
        {
            for (int i = 0; i < Path.Count - 1; i++)
            {
                lineRenderer.SetPosition(i, (Vector3)Path[i]);
            }
            done = true;
        }
    }
}
