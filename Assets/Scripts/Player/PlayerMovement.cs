using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Running")]
    [SerializeField] float runSpeed = 5f;

    [Header("Jumping")]
    [SerializeField] float jumpVelocity = 15f;
    [SerializeField][Range(0f, 0.5f)] private float coyoteFactor = 1f;
    [SerializeField] private bool canIJump = false;
    private float tempCoyote = 0f;
    private bool haveJumped = false;

    [Header("Dash")]
    [SerializeField] float DashSpeed = 5f;
    [SerializeField] float DashTime = 1f;
    [SerializeField] float LengthOfDash = 6f;
    public bool CanDash = true;
    private bool Dashing = false;

    [Header("Wall Slide")]
    [SerializeField] float SlidingSpeed = -2f;
    [SerializeField] float WallJumpHeight = 5f;
    private bool WallSlid = false;
    private bool WallJumping = false;
    public float WallJumpTime = 0f;

    [Header("Health")]
    public GameObject healthI;
    private HealthUI healthScript;

    private int FacingRight = 1;
    private Rigidbody2D rb2d;
    private float xScale;

    private Vector3 TopLeft, TopRight, MiddleLeft, MiddleRight, BottomLeft, BottomRight;
    private RaycastHit2D TopL, TopR, MidL, MidR, BotL, BotR;
    private Bounds bounds;
    private float SkinWidth;


    SpriteRenderer BoxCol;
    Vector3 CharacterScale;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        rb2d = GetComponent<Rigidbody2D>();
        xScale = transform.localScale.x;
        healthScript = healthI.GetComponent<HealthUI>();
        BoxCol = GetComponent<SpriteRenderer>();
        SkinWidth = Physics2D.defaultContactOffset;
        DashTime = LengthOfDash / DashSpeed;
        WallJumpTime = BoxCol.bounds.size.x / runSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBounds();
        RayCasts();
        Run();
        OnGround();
        OnWall();
        FlipSprite();
        Jump();
        Dash();
        Tester();
    }

    private void UpdateBounds()
    {
        bounds = BoxCol.bounds;
        BottomRight = new Vector2(bounds.max.x, bounds.min.y);
        BottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        MiddleRight = new Vector2(bounds.max.x, bounds.max.y / 2);
        MiddleLeft = new Vector2(bounds.min.x, bounds.max.y / 2);
        TopRight = new Vector2(bounds.max.x, bounds.max.y);
        TopLeft = new Vector2(bounds.min.x, bounds.max.y);
    }

    private void RayCasts()
    {
        TopL = Physics2D.Raycast(TopLeft, Vector2.left, -SkinWidth, LayerMask.GetMask("Ground"));
        TopR = Physics2D.Raycast(TopRight, Vector2.right, SkinWidth, LayerMask.GetMask("Ground"));
        BotL = Physics2D.Raycast(BottomLeft, Vector2.down, SkinWidth, LayerMask.GetMask("Ground"));
        BotR = Physics2D.Raycast(BottomRight, Vector2.down, SkinWidth, LayerMask.GetMask("Ground"));
    }

    private void Run()
    {
        if (Dashing == true) return;
        if (WallJumping == true) return;

        rb2d.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * runSpeed, rb2d.velocity.y);
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

    //TODO: skal måske gøres med animation 
    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.E) && CanDash)
        {
            StartCoroutine(IDash());
        }
    }

    // kan bruge Time.Deltatime hvis den skal være mere præcis
    private IEnumerator IDash()
    {
        Dashing = true;
        CanDash = false;
        rb2d.gravityScale = 0f;
        rb2d.velocity = new Vector2(FacingRight * DashSpeed, 0f);
        yield return new WaitForSeconds(DashTime);
        rb2d.velocity = new Vector2(0f, 0f);
        rb2d.gravityScale = 1;
        Dashing = false;
        yield return null;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canIJump && coyoteFactor >= 0)
        {
            if (WallSlid)
            {
                StartCoroutine(IWallJump());
            }
            else
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpVelocity);
                haveJumped = true;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            if (rb2d.velocity.y > 0)
                rb2d.velocity += new Vector2(0f, -rb2d.velocity.y * 1f);
        }
    }

    private IEnumerator IWallJump()
    {
        WallJumping = true;
        rb2d.velocity = new Vector2(-FacingRight * runSpeed, jumpVelocity);
        yield return new WaitForSeconds(WallJumpTime);
        WallJumping = false;
        yield return null;
    }

    // i have no idea if this is good but well have to wait and see if some problems arise from it.
    private void OnWall()
    {
        WallSlid = false;
        if (TopL.collider is null && TopR.collider is null)
        {
            return;
        }

        if (rb2d.velocity.y > 0f)
        {
            return;
        }

        if (Input.GetAxisRaw("Horizontal") == -1 && TopR.collider is null)
        {
            CharacterScale.x = xScale;
            WallSlid = true;

            rb2d.velocity = new Vector2(0f, -1f);
        }
        if (Input.GetAxisRaw("Horizontal") == 1 && TopL.collider is null)
        {
            CharacterScale.x = -xScale;
            WallSlid = true;
            rb2d.velocity = new Vector2(0f, -1f);
        }
        transform.localScale = CharacterScale;
    }

    // TODO REFACTOR sådan at variabler ikke instantieres i funktion, Længden skal være skin width og på om den collider og ikke dens længde
    // Called in jump to know whether or not the object can jump
    // DER KAN IKKE VÆRE GAPS MED INGENTING UNDER, SKAL VÆRE ET OBJEKT FOR AT DET VIRKER
    private void OnGround()
    {
        Debug.DrawRay(BottomLeft, new Vector2(0, -0.2f), Color.red);
        Debug.DrawRay(BottomRight, new Vector2(0, -0.2f), Color.blue);

        if (BotL.collider is not null && !haveJumped || BotR.collider is not null && !haveJumped)
        {
            tempCoyote = coyoteFactor;
            canIJump = true;
            CanDash = true;
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
        CharacterScale = transform.localScale;
        if (WallSlid == true) return;
        if (WallJumping == true) return;
        // TODO SKAL GØRES MERE DYNAMISK 
        if (Input.GetAxis("Horizontal") < 0)
        {
            FacingRight = -1;
            CharacterScale.x = -xScale;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            FacingRight = 1;
            CharacterScale.x = xScale;
        }
        transform.localScale = CharacterScale;
    }
}
