using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

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

    Path Path;
    int CurrentWayPoint = 0;
    public float NextWaypointDistance = 1f;
    bool ReachedEndOfPath = false;
    Seeker seeker;
    public Coroutine Patroll = null;

    // Start is called before the first frame update
    void Start()
    {
        IsPatrolling = true;
        seeker = GetComponent<Seeker>();
        rb2d = GetComponent<Rigidbody2D>();
        xScale = transform.localScale.x;
        //Patroll = StartCoroutine(GetPath(PatrolPoints[CurrentPatrolPoint]));
    }

    private void Update()
    {
        Debug.Log(CurrentPatrolPoint);
        Patrolling();
    }

    public IEnumerator GetPath(Transform Paths)
    {
        while (true)
        {
            StartPatrolling = true;
            seeker.StartPath(transform.position, Paths.position, OnPathComplete);
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
    public void MoveToPlayer(GameObject Player)
    {
        if (Path is null) return;
        if (CurrentWayPoint >= Path.vectorPath.Count)
        {
            ReachedEndOfPath = true;
            return;
        }
        else
        {
            ReachedEndOfPath = false;
        }

        Vector2 direction = (Player.transform.position - Path.vectorPath[CurrentWayPoint]).normalized;
        rb2d.velocity = direction * speed;

        float distance = Vector2.Distance(Player.transform.position, Path.vectorPath[CurrentWayPoint]);
        if (distance < RoomBetween)
        {
            //Animation and hitting
            rb2d.velocity = direction * -speed;
            return;
        }

        if (distance < NextWaypointDistance)
        {
            CurrentWayPoint++;
        }
    }
    private void Patrolling()
    {
        if (!IsPatrolling) return;
        if (Path is null) return;
        if (CurrentWayPoint >= Path.vectorPath.Count)
        {
            ReachedEndOfPath = true;
            StopCoroutine(Patroll);
            StartPatrolling = false;
            CurrentPatrolPoint++;
            if (CurrentPatrolPoint == PatrolPoints.Length && !StartPatrolling)
            {
                CurrentPatrolPoint = 0;
                Patroll = StartCoroutine(GetPath(PatrolPoints[CurrentPatrolPoint]));
                return;
            }
            Patroll = StartCoroutine(GetPath(PatrolPoints[CurrentPatrolPoint]));
            return;
        }
        else
        {
            //Dunno hvad den bliver brugt til.
            ReachedEndOfPath = false;
        }

        Vector2 direction = (Path.vectorPath[CurrentWayPoint] - transform.position).normalized;
        rb2d.velocity = direction * speed;

        float distance = Vector2.Distance(transform.position, Path.vectorPath[CurrentWayPoint]);

        if (distance < NextWaypointDistance)
        {
            CurrentWayPoint++;
        }
        /*
        
        for (int i = 0; i < patrol.targets.Length; i++)
        {
            float prevdistance = 1000f;
            distance = Vector2.Distance(transform.position, patrol.targets[i].position);
            if (distance < prevdistance)
            {
                pos = i;
            }
        }



        rb2d.velocity = direction * speed;

        if (distance < NextWaypointDistance)
        {
            pos++;
        }
        */
    }


    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "DamageDealer")
        {
            Debug.Log("DKP");
        }
    }

    */
    private void Fighting()
    {

    }
}
