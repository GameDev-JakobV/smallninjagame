using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcSpawner : MonoBehaviour
{
    public GameObject Dot;
    public Transform PosToHit;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        SpawnClick();    
    }
    

    private void SpawnClick()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var temp = Instantiate(Dot, transform);
            temp.GetComponent<ArcTest>().targetPos = PosToHit.position;
        }
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            
            yield return new WaitForSeconds(1f);
        }
    }

}
