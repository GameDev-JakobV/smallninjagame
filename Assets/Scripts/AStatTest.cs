using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AStatTest : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public Camera Cam;
    public GameObject Player;
    Path Path;
    
    int CurrentWayPoint = 0;
    int CurrentPoint = 0;
    public float NextWaypointDistance = 0.5f;
    // Dunno om hvad den skal bruges, den var med i tutorialen men umiddelbart bruges den heller ikke der
    bool ReachedEndOfPath = false;
    public List<Transform> Points;
    Vector3 Point;

    public bool IsPatrolling = true;
    private bool Moving = false;

    Seeker seeker;
    public Coroutine Seek = null;

    private void Awake()
    {
        seeker = GetComponent<Seeker>();
    }

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Seek = StartCoroutine(GetPath());
    }

    private void Update()
    {
        if (IsPatrolling)
        {
            Patroll();
        }
        else
        {
            Chase();
        }
    }

    public IEnumerator GetPath()
    {
        while (true)
        {
            seeker.StartPath(transform.position, Point, OnPathComplete);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            Path = p;
            CurrentWayPoint = 0;
        }
    }

    void Patroll()
    {
        Debug.Log(CurrentPoint);
        if (Path is null) return;
        if (CurrentWayPoint >= Path.vectorPath.Count)
        {
            CurrentWayPoint = 0;
            CurrentPoint++;
            if (CurrentPoint == Points.Count)
            {
                Debug.Log("Resetting Path");
                CurrentPoint = 0;
            }
            Path = null;
        }
        else
        {
            Vector2 direction = (Path.vectorPath[CurrentWayPoint] - transform.position).normalized;
            Debug.DrawRay(transform.position, direction, Color.red);
            rb2d.velocity = direction * 5f;

            float distance = Vector2.Distance(transform.position, Path.vectorPath[CurrentWayPoint]);

            if (distance < NextWaypointDistance)
            {
                CurrentWayPoint++;
            }
        }

        Point = Points[CurrentPoint].position;
        
    }

    void Chase()
    {
        Point = Player.transform.position;
        if (Path is null) return;
        if (CurrentWayPoint >= Path.vectorPath.Count)
        {
            return;
        }
        else
        {

        }

        Vector2 direction = (Path.vectorPath[CurrentWayPoint] - transform.position).normalized;
        Debug.DrawRay(transform.position, direction, Color.red);
        rb2d.velocity = direction * 5f;

        float distance = Vector2.Distance(transform.position, Path.vectorPath[CurrentWayPoint]);

        if (distance < NextWaypointDistance)
        {
            CurrentWayPoint++;
        }
    }
}
