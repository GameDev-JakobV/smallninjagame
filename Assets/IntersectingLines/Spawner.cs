using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public BulletFire _BulletFire;
    public float SpeedX = 5f;
    public float SpeedY = 10f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy() 
    {
        while (true)
        {
            GameObject spawn = Instantiate(enemy, transform.position, enemy.transform.rotation);
            spawn.GetComponent<Bullet>().direction = new Vector2(SpeedX, SpeedY);
            _BulletFire.Enemies.Add(spawn);
            yield return new WaitForSeconds(0.8f);
        }
    }
}