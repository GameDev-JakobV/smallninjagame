using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteHead : MonoBehaviour
{
    CircleCollider2D detection;
    Rigidbody2D rb2d;
    public Vector3 startPosition;

    private bool isAttacking = false;

    [Header("Movement")]
    [SerializeField] [Range(0f, 50f)] float speed = 1f;
    [SerializeField] [Range(0f, 800f)] float angleChangeSpeed = 500f;
    [SerializeField] [Range(0f, 50f)] float attackingSpeed = 5f;
    [SerializeField] [Range(0f, 800f)] float attackingAngleChangeSpeed = 500f;

    //TODO Make it do damage

    // Start is called before the first frame update
    void Start()
    {
        detection = GetComponent<CircleCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    private void Update()
    {
        // TODO add patrol range so that it doesnt look so rigid
        if (!isAttacking)
        {
            MoveBackToOrigin();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isAttacking = true;
            Vector2 direction = (Vector2)collision.transform.position - rb2d.position;
            print(direction);
            direction.Normalize();
            print(direction.normalized);
            float rotateAmount = Vector3.Cross(direction, transform.up).z;            
            rb2d.angularVelocity = -attackingAngleChangeSpeed * rotateAmount;
            rb2d.velocity = transform.up * attackingSpeed;
            
            Debug.DrawRay(transform.position, rb2d.velocity, Color.red);
            //rb2d.position = Vector2.MoveTowards(transform.position, collision.transform.position, speed * Time.deltaTime);
            //transform.up = collision.transform.position - transform.position;
            //print(Vector2.MoveTowards(transform.position, collision.transform.position, 1));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isAttacking = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isAttacking = false;
        }   
    }


    private void MoveBackToOrigin()
    {
        Vector2 direction = (Vector2)startPosition - rb2d.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb2d.angularVelocity = -angleChangeSpeed * rotateAmount;
        rb2d.velocity = transform.up * speed;
        Debug.DrawRay(transform.position, rb2d.velocity, Color.black);
    }
}
