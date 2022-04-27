using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour
{
    public bool Fire = false;
    public GameObject enemy;
    public GameObject Path;
    public GameObject spawner;
    public Spawner spawnerScript;
    public Transform LaunchPos;
    public List<GameObject> Enemies;
    private float speed = 2f;
    private LineRenderer _lr;
    private bool Simulated = false;

    // Start is called before the first frame update
    void Start()
    {
        //Enemies = new List<GameObject>();
        spawnerScript = spawner.GetComponent<Spawner>();
        Debug.Log(spawnerScript.SpeedY);
        Debug.Log(spawnerScript.SpeedX);
        _lr = GetComponent<LineRenderer>();
        StartCoroutine(SpawnBullet());

    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<LineRenderer>().SetPositions(SimulatePath());
        SimulatePath();

        //RaycastHit2D line = Physics2D.Raycast(gameObject.transform.position, Vector2.right, LayerMask.GetMask("Ground"));
        //Debug.DrawRay(gameObject.transform.position, Vector2.right * 100, Color.green);
    }



    private void SimulatePath()
    {
        if (Simulated == false)
        {
            float MaxDuration = 5f;
            float TimeStepInterval = 0.04f;
            int MaxSteps = (int)(MaxDuration / TimeStepInterval);
            Vector2 DirectionVector = new Vector2(spawnerScript.SpeedX, spawnerScript.SpeedY);

            for (int i = 0; i < MaxSteps; i++)
            {
                Vector3 CalcPos = (Vector2)Enemies[0].transform.position + DirectionVector * i * TimeStepInterval;
                Instantiate(Path, CalcPos, Path.transform.rotation);
                if (CalcPos.y < transform.position.y + 0.4f & CalcPos.y > transform.position.y + 0.2f)
                {
                    Debug.Log(CalcPos.y);
                    Debug.Log(transform.position.y + 0.4f);
                    Debug.Log(transform.position.y + 0.2f);

                    float TimeNeededToReach = i * TimeStepInterval;
                    Vector2 LengthOfTravel = CalcPos - transform.position;
                    float TimeToWait = LengthOfTravel.x / 10; //should be speed
                    Debug.Log(TimeNeededToReach);
                    Debug.Log(TimeToWait);
                    float temp = TimeNeededToReach - TimeToWait;
                    Debug.Log(temp);
                    StartCoroutine(SpawnsBullet(TimeNeededToReach - TimeToWait));
                    Debug.Log("Made it");
                    break;
                }
            }
            Simulated = true;
        }
    }

    private IEnumerator SpawnsBullet(float TimeToWait)
    {
        yield return new WaitForSeconds(TimeToWait);
        Debug.Log("Firing");
        GameObject spawn = Instantiate(enemy, transform.position, enemy.transform.rotation);
        spawn.GetComponent<Bullet>().direction = new Vector2(10f, 0f); //should be speed
        yield return null;
    }

    private IEnumerator SpawnBullet()
    {
        while (true)
        {
            if (Fire == true)
            {
                Debug.Log("Firing");
                GameObject spawn = Instantiate(enemy, transform.position, enemy.transform.rotation);
                spawn.GetComponent<Bullet>().direction = new Vector2(10f, 0f);
            }
            yield return new WaitForSeconds(0.8f);
        }
    }
}
