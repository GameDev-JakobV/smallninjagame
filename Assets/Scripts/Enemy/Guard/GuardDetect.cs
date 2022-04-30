using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardDetect : MonoBehaviour
{
    public GameObject Parent;
    private GuardAI Guard;
    private Coroutine Getpath;

    private void Start()
    {
        Guard = Parent.GetComponent<GuardAI>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Guard.IsPatrolling = false;
            Getpath = StartCoroutine(Guard.GetPath(collision.gameObject.transform));
            Guard.MoveToPlayer(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log(Getpath);
            StopCoroutine(Getpath);
            //Start Patrolling
            Guard.IsPatrolling = true;
        }
    }
}
