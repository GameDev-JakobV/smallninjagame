using System.Collections;
using UnityEngine;

public class GuardDetect : MonoBehaviour
{
    public GameObject Parent;
    private GuardAI Guard;

    private void Start()
    {
        Guard = GetComponentInParent<GuardAI>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Guard.IsPatrolling = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Guard.Point = collision.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Guard.IsPatrolling = true;
        }
    }
}
