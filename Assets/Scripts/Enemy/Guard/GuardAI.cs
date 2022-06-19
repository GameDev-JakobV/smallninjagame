using System.Collections;
using UnityEngine;
using Pathfinding;
using System.Collections.Generic;

public class GuardAI : MonoBehaviour, IEnemyHealth
{
    [Header("Tweaking")]
    [SerializeField] private int Health = 50;
    public int Hp
    {
        get { return Health; }
        set { Health = value; }
    }
    [SerializeField] private float speed = 5f;
    [SerializeField] private float RoomBetween = 2f;

    [Header("For Pathfinding")]
    [SerializeField] private float NextWaypointDistance = 0.5f;
    [SerializeField] private List<Transform> Points;

    [HideInInspector] public bool IsPatrolling = true;
    [HideInInspector] public Vector3 Point;
    private int CurrentWayPoint = 0;
    private int CurrentPoint = 0;

    Rigidbody2D rb2d;
    Seeker seeker;
    Path Path;
    public GameObject Dot;

    private Vector2 CharacterScale;
    private float xScale;
    [HideInInspector] public bool Jumping = false;
    private JumpWithLerp jumpWithLerp;


    private void Awake()
    {
        seeker = GetComponent<Seeker>();
    }

    // Start is called before the first frame update
    void Start()
    {
        IsPatrolling = true;
        rb2d = GetComponent<Rigidbody2D>();
        jumpWithLerp = GetComponent<JumpWithLerp>();
        xScale = transform.localScale.x;
        StartCoroutine(GetPath());
    }

    private void Update()
    {
        if (Jumping) return;
        if (IsPatrolling)
        {
            Patroll();
        }
        else
        {
            Chase();
        }
    }

    private void Fighting()
    {

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


    //Looking for player
    //Patrol on Platform
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
            rb2d.velocity = new Vector2(direction.x * speed, rb2d.velocity.y);


            float distance = Vector2.Distance(transform.position, Path.vectorPath[CurrentWayPoint]);

            if (distance < NextWaypointDistance)
            {
                CurrentWayPoint++;
            }
        }
        Point = Points[CurrentPoint].position;
    }

    //Fighting
    //Move close enough to attack
    //Attack
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

        rb2d.velocity = new Vector2(direction.x * speed, rb2d.velocity.y);

        float distance = Vector2.Distance(transform.position, Path.vectorPath[CurrentWayPoint]);
        if (distance < NextWaypointDistance)
        {
            CurrentWayPoint++;
        }
    }

    private void FlipSprite()
    {
        CharacterScale = transform.localScale;
        // TODO SKAL GØRES MERE DYNAMISK 
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


    
}
