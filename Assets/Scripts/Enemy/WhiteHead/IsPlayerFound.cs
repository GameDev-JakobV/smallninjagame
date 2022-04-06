using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPlayerFound : MonoBehaviour
{
    public GameObject Parent;
    private WhiteHead whiteHead;

    private void Start()
    {
        whiteHead = Parent.GetComponent<WhiteHead>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("dwadaw");
            whiteHead.MoveAndTurn(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("dwadaw");
            whiteHead.MoveBackToOrigin();
        }
    }
}
