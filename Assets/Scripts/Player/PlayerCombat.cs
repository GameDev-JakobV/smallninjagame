using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using static UnityEngine.GraphicsBuffer;

public class PlayerCombat : MonoBehaviour
{
    // Melee combat
    [Header("Melee")]
    [SerializeField] [Range(0f, 0.5f)] float AttackTiming = 0.2f;
    public GameObject DamageDeal;

    // Ranged combat
    [Header("Ranged")]
    [SerializeField] GameObject PrefabBow;
    [SerializeField] GameObject Projectile;
    [SerializeField] float SpeedOfProj = 800f;
    [SerializeField] float DistanceFromPlayer = 1f;

    [SerializeField] float EndTimeScale = 0.5f;
    [SerializeField] float TimeTakenToFullySlow = 3f;
    [SerializeField] float TimeTakenToFullySpeed = 1.5f;
    float TimeElapsed = 0f;
    public Camera Cam;
    private GameObject Bow;
    private bool IsAiming = false;

    public GameObject test;

    // Start is called before the first frame update
    void Start()
    {
        Bow = Instantiate(PrefabBow, transform.position, Quaternion.identity);
        Bow.SetActive(false);
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
            IsAiming = true;
            Transform TransformParent = GetComponentInParent<Transform>();
            var parentPos = TransformParent.transform.position;
            var pos = Cam.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0f;
            //Debug.Log("Mouse Pos " + pos);
            //Debug.Log("ParentPos " + parentPos);
            //Debug.DrawLine(parentPos, pos);


            Vector3 direction = pos - parentPos;
            Debug.DrawRay(parentPos, direction.normalized * DistanceFromPlayer);
            RaycastHit2D ray = Physics2D.Raycast(parentPos, direction.normalized * DistanceFromPlayer);


            Vector3 BowPos = parentPos + direction.normalized * DistanceFromPlayer;
            Bow.transform.position = BowPos;

            Vector3 targetPos = Bow.transform.position;
            Vector3 thisPos = TransformParent.transform.position;

            targetPos.x = targetPos.x - thisPos.x;
            targetPos.y = targetPos.y - thisPos.y;

            float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
            Bow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                GameObject Arrow = Instantiate(Projectile, Bow.transform.position, Bow.transform.rotation);
                //Arrow.GetComponent<Rigidbody2D>().AddForce(direction.normalized * SpeedOfProj);
                
                Arrow.GetComponent<Arrow>().addforce(direction.normalized * SpeedOfProj);
            }
        }
        else
        {
            IsAiming = false;
            Bow.SetActive(false);
        }
        
    }
}
