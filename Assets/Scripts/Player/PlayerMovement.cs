using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Running")]
    [SerializeField] float runSpeed = 5f;

    [Header("Jumping")]
    [SerializeField] float jumpVelocity = 15f;
    [SerializeField] [Range(0f, 1f)] float floatingFactor = 1f;

    [Header("Health")]
    public GameObject healthI;
    private HealthUI healthScript;


    private Rigidbody2D rb2d;
    private float xScale;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        xScale = transform.localScale.x;



        healthScript = healthI.GetComponent<HealthUI>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
        CanIJump();
        Jump();
        Tester();
    }

    private void Tester()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            healthScript.TakeDamage();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            healthScript.GainHealth();
        }
    }

    private void Run()
    {
        rb2d.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * runSpeed, rb2d.velocity.y);
    }

    private void Jump()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) && CanIJump())
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpVelocity);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            if(rb2d.velocity.y > 0)
            rb2d.velocity += new Vector2(rb2d.velocity.x, -rb2d.velocity.y * 1f);
        }
    }

    // TODO Rename to onGround()
    //Called in jump to know whether or not the object can jump
    public bool CanIJump()
    {
        // should probably be made into a vector2
        Vector3 leftSide = new Vector3(transform.localPosition.x - 0.5f, transform.localPosition.y -1f);
        Vector3 rightSide = new Vector3(transform.localPosition.x + 0.5f, transform.localPosition.y - 1f);

        var leftSidePhys = Physics2D.Raycast(leftSide, new Vector2(0, -0.2f));
        var rightSidePhys = Physics2D.Raycast(rightSide, new Vector2(0, -0.2f));

        //debugging
        Debug.DrawRay(leftSide, new Vector2 (0, -0.2f));
        Debug.DrawRay(rightSide, new Vector2(0, -0.2f));
        //print("Left side " + leftSidePhys.distance);
        //print("Right side "+ rightSidePhys.distance);


        if(leftSidePhys.distance <= 0 && rightSidePhys.distance <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void FlipSprite()
    {

        // TODO SKAL GØRES MERE DYNAMISK 
        Vector3 characterScale = transform.localScale;
        if (Input.GetAxis("Horizontal") < 0)
        {
            characterScale.x = -xScale;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            characterScale.x = xScale;
        }
        transform.localScale = characterScale;
    }
}
