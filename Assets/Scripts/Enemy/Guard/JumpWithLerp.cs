using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpWithLerp : MonoBehaviour
{
    public float Duration = 3f;
    GameObject Player;
    private GuardAI guardAI;
    private List<Vector2> Points;
    SpriteRenderer PlayerSpriteRenderer;
    SpriteRenderer MySpriteRenderer;
    PlayerMovement player;


    [HideInInspector] public float TimeWaited = 0f;
    [SerializeField] private float TimeToWait = 2f;


    // Start is called before the first frame update
    void Start()
    {
        //PlayerSpriteRenderer = Player.GetComponent<SpriteRenderer>();
        MySpriteRenderer = GetComponent<SpriteRenderer>();
        guardAI = GetComponent<GuardAI>();
    }

    //increments timewaited when the angle is greater then 45
    //skal umiddelbart finpusses sådan at den er mere kompleks end 45 grader, måske retningen af A* seeker vector retning
    public bool ShouldJump(GameObject Player)
    {
        player = Player.GetComponent<PlayerMovement>();
        RaycastHit2D Line = Physics2D.Linecast(transform.position, Player.transform.position);
        Debug.DrawLine(transform.position, Player.transform.position, Color.red);

        float angle = Mathf.Atan2(Player.transform.position.y - transform.position.y, Player.transform.position.x - transform.position.x) * Mathf.Rad2Deg;

        if (angle > 0)
        {
            if (angle > 45 && angle < 135)
            {
                TimeWaited += Time.deltaTime;
                if (TimeWaited >= TimeToWait && player.canIJump && !player.WallSlid)
                {
                    return true;
                }
            }
            else
            {
                TimeWaited = 0f;
            }
        }
        else
        {
            if (angle < -45 && angle > -135)
            {
                TimeWaited += Time.deltaTime;
                if (TimeWaited >= TimeToWait && player.canIJump && !player.WallSlid) 
                {
                    return true;
                }
            }
            else
            {
                TimeWaited = 0f;
            }
        }
        return false;
    }

    private IEnumerator MovePointAcrossCubicCurve()
    {
        //Instantiate(Dot, new Vector3(transform.position.x, transform.position.y + 2f, 0f), Quaternion.identity);
        //Instantiate(Dot, new Vector3(Player.transform.position.x, Player.transform.position.y + 2f, 0f), Quaternion.identity);

        Points = new List<Vector2>()
        {
            transform.position,
            new Vector2(transform.position.x, transform.position.y + 2f),
            new Vector2(Player.transform.position.x, Player.transform.position.y + 2f),
            Player.transform.position,
        };
        float TimeElapsed = 0f;

        while (TimeElapsed < Duration)
        {
            float t = TimeElapsed / Duration;
            t = Mathf.Sin(t * Mathf.PI * 0.5f);
            Player.transform.position = CubicBezierCurve(Points[0], Points[1], Points[2], Points[3], t);

            TimeElapsed += Time.deltaTime;

            yield return null;
        }
    }

    public IEnumerator MovePointAcrossQuadraticCurve(GameObject player)
    {
        var xMove = (transform.position.x - player.transform.position.x) / 2;
        var yMove = (transform.position.y - player.transform.position.y) / 2;
        //Instantiate(Dot, new Vector3(player.transform.position.x + xMove, player.transform.position.y - yMove, 0f), Quaternion.identity);

        Points = new List<Vector2>()
        {
            transform.position,
            new Vector2(player.transform.position.x + xMove, player.transform.position.y - yMove),
            player.transform.position,
        };
        float TimeElapsed = 0f;
        while (TimeElapsed < Duration)
        {
            float t = TimeElapsed / Duration;
            t = Mathf.Sin(t * Mathf.PI * 0.5f);
            transform.position = QuadraticCurve(Points[0], Points[1], Points[2], t);
            guardAI.Jumping = true;
            TimeElapsed += Time.deltaTime;

            yield return null;
        }
        guardAI.Jumping = false;

        yield return null;
    }


    private static Vector2 QuadraticCurve(Vector2 a, Vector2 b, Vector2 c, float t)
    {
        Vector2 p0 = Vector2.Lerp(a, b, t);
        Vector2 p1 = Vector2.Lerp(b, c, t);
        return Vector2.Lerp(p0, p1, t);
    }

    private static Vector2 CubicBezierCurve(Vector2 a, Vector2 b, Vector2 c, Vector2 d, float t)
    {
        Vector2 p0 = QuadraticCurve(a, b, c, t);
        Vector2 p1 = QuadraticCurve(b, c, d, t);
        return Vector2.Lerp(p0, p1, t);
    }

}
