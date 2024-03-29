﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteHead : MonoBehaviour
{
    // TODO: Bug hvis white er vinkelret med player og på lige grund, så drejer den ikke altid og kører i en lige linje


    Rigidbody2D rb2d;
    public Vector3 startPosition;

    public bool isAttacking { get; set; }
    private float xScale = 0;

    [Header("Movement")]
    [SerializeField] [Range(0f, 50f)] float speed = 1f;
    [SerializeField] [Range(0f, 800f)] float angleChangeSpeed = 500f;
    [SerializeField] [Range(0f, 50f)] float attackingSpeed = 5f;
    [SerializeField] [Range(0f, 800f)] float attackingAngleChangeSpeed = 30f;


    [SerializeField] int Hp = 50;

    public int MyHp
    {
        get { return Hp; }
        set { Hp = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        isAttacking = false;
        rb2d = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        xScale = transform.localScale.x;
    }

    private void Update()
    {

        FlipSprite();
        // TODO add patrol range so that it doesnt look so rigid
        if (!isAttacking)
        {
            MoveBackToOrigin();
        }
    }

    #region Living entity variables
    void DoIDie()
    {
        if (Hp <= 0) Die();
    }

    void TakeDamage(int damageTaken)
    {
        Hp = Hp - damageTaken;
    }

    void Die()
    {
        Destroy(gameObject);
    }
    #endregion

    // skal have invulnerability frames, men det skal ske i animationen 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DamageDealer")
        {
            var temp = collision.GetComponent<DamageToDeal>();
            Debug.Log(temp.MySwordDamage);
            TakeDamage(temp.MySwordDamage);
            DoIDie();
        }
    }

    public void MoveAndTurn(Collider2D collision)
    {
        isAttacking = true;
        // Skal have en retning 
        Vector2 direction = (Vector2)collision.transform.position - rb2d.position;
        //print(direction);
        // normalizere den til mellem 0 og 1, fordi det er vores transform.up
        direction.Normalize();
        // vi får en rotation mængde baseret på om linjerne
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        // Angular velocity er vinkler i sekundet, Dette kan give mærkelige rotations hastigheder, Fordi krydsproduktet kan give store svingninger i hastighed.
        rb2d.angularVelocity = -attackingAngleChangeSpeed * rotateAmount;
        //Debug.Log(rb2d.angularVelocity);
        rb2d.velocity = transform.up * attackingSpeed;
        Debug.DrawRay(transform.position, rb2d.velocity, Color.red);
        //rb2d.position = Vector2.MoveTowards(transform.position, collision.transform.position, speed * Time.deltaTime);
        //transform.up = collision.transform.position - transform.position;
        //print(Vector2.MoveTowards(transform.position, collision.transform.position, 1));
    }

    private void FlipSprite()
    {
        float angle = transform.rotation.eulerAngles.z;
        Vector3 characterScale = transform.localScale;
        if (angle > 180 && angle <= 360)
        {
            characterScale.x = -xScale;
            //Debug.Log("dwar " + characterScale.x);
        }
        if (angle < 180 && angle >= 0)
        {
            characterScale.x = xScale;
            //Debug.Log("Du " + characterScale.x);
        }
        transform.localScale = characterScale;
    }

    public void MoveBackToOrigin()
    {
        Vector2 direction = (Vector2)startPosition - rb2d.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb2d.angularVelocity = angleChangeSpeed * -rotateAmount;
        rb2d.velocity = transform.up * speed;
        //Debug.DrawRay(transform.position, rb2d.velocity, Color.black);
    }
}
