using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedWitchAI : MonoBehaviour, IEnemyHealth
{
    public int Hp { get; set; }

    public GameObject Dot;
    public int PathForOrbs = 8;
    private float DegressBetweenPoints;

    // Start is called before the first frame update
    void Start()
    {
        DegressBetweenPoints = 360/PathForOrbs;
        float point = 0;
        for (int i = 0; i < PathForOrbs; i++)
        {
            
            var temp = Quaternion.AngleAxis(point, Vector3.forward) * new Vector2(1,0).normalized;
            var dot = Instantiate(Dot, temp, Quaternion.identity);
            dot.transform.SetParent(gameObject.transform, false);

            point += DegressBetweenPoints;
        }
        StartCoroutine(RotateOrbs());
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    //Skal rotere omkring sig selv
    private IEnumerator RotateOrbs()
    {
        float timetaken = 0;
        float timetotake = 3f;
        //start.rotation = Quaternion.identity;
        //end.rotation = Quaternion.AngleAxis(360, Vector3.forward);
        //Vector2 start = new Vector2(0,0);
        //Vector2 end = new Vector2(0,360);
        while (timetaken < timetotake)
        {
            
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(360, Vector3.forward), timetaken / timetotake);
            timetaken += Time.deltaTime;
        }
        yield return null;
    }

    #region Living entity variables
    private void DoIDie()
    {
        if (Hp <= 0) Die();
    }

    private void TakeDamage(int damageTaken)
    {
        Hp = Hp - damageTaken;
        DoIDie();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
    #endregion
}
