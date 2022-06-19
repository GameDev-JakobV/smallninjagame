using System.Collections;
using UnityEngine;

public class GuardDetect : MonoBehaviour
{
    public GameObject Parent;
    private GuardAI Guard;
    private JumpWithLerp jumpWithLerp;
    private bool jumping = false;

    private void Start()
    {
        Guard = GetComponentInParent<GuardAI>();
        jumpWithLerp = GetComponentInParent<JumpWithLerp>();

    }

    //Skal lave en Line imellem enemy og player og at der ikke er noget imellem dem.
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
            if (jumpWithLerp.ShouldJump(collision.gameObject) && !Guard.Jumping)
            {
                StartCoroutine(jumpWithLerp.MovePointAcrossQuadraticCurve(collision.gameObject));
                Debug.Log("JUMPING");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jumpWithLerp.TimeWaited = 0f;
            Guard.IsPatrolling = true;
        }
    }
}
