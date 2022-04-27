using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = direction;
        RaycastHit2D Path = Physics2D.Raycast(gameObject.transform.position, direction);
        Debug.DrawRay(gameObject.transform.position, direction, Color.red);
    }
}
