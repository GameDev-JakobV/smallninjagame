using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public GameObject DamageDeal;

    DamageToDeal damageToDeal;
    [SerializeField] int Damage;

    // Start is called before the first frame update
    void Start()
    {
        DamageDeal.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Hit());
        }
    }

    public IEnumerator Hit()
    {


        DamageDeal.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        DamageDeal.SetActive(false);
        // Spawn or activate hitbox

        // Access Hp value of hit object

        // 

        yield return null;
    }
}
