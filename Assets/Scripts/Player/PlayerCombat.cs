using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    // Melee combat
    [Header("Melee")]
    [SerializeField] [Range(0f, 0.5f)] float AttackTiming = 0.2f;
    public GameObject DamageDeal;

    // Ranged combat
    [Header("Ranged")]
    public GameObject Bow;

    // Start is called before the first frame update
    void Start()
    {
        
        DamageDeal.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        Aiming();

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!DamageDeal.activeSelf)
            {
                StartCoroutine(Hit());
            }
        }
    }


    private IEnumerator Hit()
    {
        DamageDeal.SetActive(true);
        yield return new WaitForSeconds(AttackTiming);
        DamageDeal.SetActive(false);
        yield return null;
    }


    private void Aiming()
    {
        if (Input.GetKey(KeyCode.T))
        {
            Bow.SetActive(true);
            
        }
        else
        {
            Bow.SetActive(false);
        }
    }

}
