using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Arrow : MonoBehaviour
{
    Rigidbody2D rb2d;
    public Vector3 Velocity { get; set; }
    private float TimeElapsed = 0;
    private float StraightArrow = 0.1f;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.AddForce(Velocity);
    }

    private void Update()
    {

        // TODO: THIS COULD PROBABLY BE DONE BETTER BUT I DONT KNOW HOW.
        if (TimeElapsed < StraightArrow)
        {
            Vector2 dir = Velocity;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            TimeElapsed += Time.deltaTime;
        }
        else
        {
            Vector2 dir = rb2d.velocity;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    public void GetDirection(Vector3 Direction)
    {
        Velocity = Direction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //TODO: ARROW DAMAGE

        //TRIGGER DAMAGE ON ENEMY

        //Ignore Player

        //Determine the damage of arrow

        Destroy(gameObject);
    }
}
