using System.Collections;
using UnityEngine;
using Pathfinding;
using System.Collections.Generic;

public class GuardAI : MonoBehaviour
{
    [SerializeField] public int Hp = 5;
    [SerializeField] float speed = 5f;
    public float RoomBetween = 2f;
    public Transform[] PatrolPoints;
    [HideInInspector] public int CurrentPatrolPoint = 0;

    public bool IsPatrolling = true;
    private bool StartPatrolling = false;
    private Vector2 CharacterScale;
    private float xScale;
    Rigidbody2D rb2d;

    Seeker seeker;
    Path Path;
    int CurrentWayPoint = 0;
    int CurrentPoint = 0;
    public float NextWaypointDistance = 0.5f;
    // Dunno om hvad den skal bruges, den var med i tutorialen men umiddelbart bruges den heller ikke der
    bool ReachedEndOfPath = false;
    public List<Transform> Points;
    [HideInInspector] public Vector3 Point;

    private void Awake()
    {
        seeker = GetComponent<Seeker>();
    }

    // Start is called before the first frame update
    void Start()
    {
        IsPatrolling = true;
        rb2d = GetComponent<Rigidbody2D>();
        xScale = transform.localScale.x;
        StartCoroutine(GetPath());
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

    #region Living entity variables
    private void DoIDie()
    {
        if (Hp <= 0) Die();
    }

    private void TakeDamage(int damageTaken)
    {
        Hp = Hp - damageTaken;
        DoIDie();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
    #endregion

    //Looking for player
    //Patrol on Platform
    //Fighting
    //Move close enough to attack
    //Attack
    void Patroll()
    {
        if (Path is null) return;
        if (CurrentWayPoint >= Path.vectorPath.Count)
        {
            CurrentWayPoint = 0;
            CurrentPoint++;
            if (CurrentPoint == Points.Count)
            {
                CurrentPoint = 0;
            }
            Path = null;
        }
        else
        {
            Vector2 direction = (Path.vectorPath[CurrentWayPoint] - transform.position).normalized;
            Debug.Log(rb2d.velocity);
            rb2d.velocity = direction * speed;

            float distance = Vector2.Distance(transform.position, Path.vectorPath[CurrentWayPoint]);

            if (distance < NextWaypointDistance)
            {
                CurrentWayPoint++;
            }
        }

        Point = Points[CurrentPoint].position;
    }

    public void Chase()
    {
        //Point = Player.transform.position;
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
        rb2d.velocity = direction * speed;

        float distance = Vector2.Distance(transform.position, Path.vectorPath[CurrentWayPoint]);
        if (distance < NextWaypointDistance)
        {
            CurrentWayPoint++;
        }
    }

    private void FlipSprite()
    {
        CharacterScale = transform.localScale;
        // TODO SKAL G�RES MERE DYNAMISK 
        if (Input.GetAxis("Horizontal") < 0)
        {
            CharacterScale.x = -xScale;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            CharacterScale.x = xScale;
        }
        transform.localScale = CharacterScale;
    }

    private void Fighting()
    {

    }
}
