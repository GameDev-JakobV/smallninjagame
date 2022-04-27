using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour
{
    public bool Fire = false;
    public GameObject enemy;
    public List<GameObject> Enemies;

    // Start is called before the first frame update
    void Start()
    {
        Enemies = new List<GameObject>();
        StartCoroutine(SpawnBullet());
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D line = Physics2D.Raycast(gameObject.transform.position, Vector2.right, LayerMask.GetMask("Ground"));
        Debug.DrawRay(gameObject.transform.position, Vector2.right * 100, Color.green);
        Debug.Log(line.point);
    }

    private IEnumerator SpawnBullet()
    {
        while (true)
        {
            if (Fire == true)
            {
                Debug.Log("dwad");
                GameObject spawn = Instantiate(enemy, transform.position, enemy.transform.rotation);
                spawn.GetComponent<Bullet>().direction = new Vector2(10f, 0f);
                yield return new WaitForSeconds(0.8f);
            }
        }
    }
}
