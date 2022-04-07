using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Running")]
    [SerializeField] float runSpeed = 5f;

    [Header("Jumping")]
    [SerializeField] float jumpVelocity = 15f;
    [SerializeField] [Range(0f, 0.5f)] private float coyoteFactor = 1f;
    private float tempCoyote = 0f;
    private bool haveJumped = false;
    private bool canIJump = false;

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
        CanIJump();
        FlipSprite();
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

    //skal måske gøres med animation
    private void Dash()
    {
        //Stop downward movement 

        //Apply movement in the facing direction

        //stop after a certain amount of time
    }

    

    private void Run()
    {
        rb2d.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * runSpeed, rb2d.velocity.y);
    }

    private void Jump()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) && canIJump && coyoteFactor >= 0)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpVelocity);
            haveJumped = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            if(rb2d.velocity.y > 0)
            rb2d.velocity += new Vector2(rb2d.velocity.x, -rb2d.velocity.y * 1f);
        }
    }

    // TODO Rename to onGround()
    //Called in jump to know whether or not the object can jump
    // DER KAN IKKE VÆRE GAPS MED INGENTING UNDER, SKAL VÆRE ET OBJEKT FOR AT DET VIRKER
    public void CanIJump()
    {
        // should probably be made into a vector2
        Vector3 leftSide = new Vector3(transform.localPosition.x - 0.5f, transform.localPosition.y -1f);
        Vector3 rightSide = new Vector3(transform.localPosition.x + 0.5f, transform.localPosition.y - 1f);

        LayerMask layerMask = LayerMask.GetMask("Ground");

        var leftSidePhys = Physics2D.Raycast(leftSide, new Vector2(0, -0.2f), 20f, layerMask);
        var rightSidePhys = Physics2D.Raycast(rightSide, new Vector2(0, -0.2f), 20f, layerMask);

        //debugging
        Debug.DrawRay(leftSide, new Vector2 (0, -0.2f));
        Debug.DrawRay(rightSide, new Vector2(0, -0.2f));
        //print("Left side " + leftSidePhys.distance);
        //print("Right side "+ rightSidePhys.distance);
        //print(haveJumped);  
        
        

        if(leftSidePhys.distance <= 0 && !haveJumped || rightSidePhys.distance <= 0 && !haveJumped)
        {
            tempCoyote = coyoteFactor;
            canIJump = true;
        }
        else
        {
            haveJumped = false;
            tempCoyote -= Time.deltaTime;
            if (tempCoyote >= 0)
            {
                canIJump = true;
            }
            else
            {

                canIJump = false;
            }
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
