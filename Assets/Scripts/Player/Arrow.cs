using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Arrow : MonoBehaviour
{
    Rigidbody2D rb2d;
    public Vector3 Velocity { get; set; }
    private float TimeElapsed = 0;
    private float StraightArrow = 0.5f;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.AddForce(Velocity);
        Debug.Log(rb2d.velocity);
    }

    private void Update()
    {
        Vector2 dir = Velocity;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void addforce(Vector3 vector3)
    {
        Velocity = vector3;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
