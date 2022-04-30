using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GuardAI : MonoBehaviour
{
    private int Hp = 5;

    //Detect Player
    //2 States looking for player, and fighting
    //Looking for player
    //Patrol on Platform
    //Fighting
    //Move close enough to attack
    //Attack
    Seeker seeker;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("COL");
        }
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

    private void Patrolling()
    {

    }

    private void Fighting()
    {

    }


}
