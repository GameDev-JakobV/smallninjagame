using UnityEngine;

public class GuardDetect : MonoBehaviour
{
    public GameObject Parent;
    private GuardAI Guard;
    private Coroutine GetPathChase;
    private Coroutine GetPathPatrol;

    private void Start()
    {
        Guard = Parent.GetComponent<GuardAI>();
        Debug.Log(Guard.transform.position.x);
        GetPathPatrol = StartCoroutine(Guard.GetPath(Guard.PatrolPoints[Guard.CurrentPatrolPoint]));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StopCoroutine(GetPathPatrol);
            GetPathChase = StartCoroutine(Guard.GetPath(collision.gameObject.transform));
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Guard.IsPatrolling = false;
            Debug.Log("Attacking");
            Guard.MoveToPlayer(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log(GetPathChase);
            if (GetPathChase is not null)
            {
                StopCoroutine(GetPathChase);
            }
            Guard.IsPatrolling = true;
            GetPathPatrol = StartCoroutine(Guard.GetPath(Guard.PatrolPoints[Guard.CurrentPatrolPoint]));
        }
    }
}
