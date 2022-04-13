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
    [SerializeField] GameObject PrefabBow;
    [SerializeField] GameObject Projectile;
    [SerializeField] float SpeedOfProj = 800f;
    [SerializeField] float DistanceFromPlayer = 1f;
    [SerializeField] Camera Cam;
    [SerializeField] float MaxArrowCharges = 4f;
    [SerializeField] float CostOfArrow = 3f;
    private bool IsAiming = false;


    [Header("UI")]
    private GameObject Bow;

    [Header("UI")]
    public GameObject ArrowUI;
    private ArrowUI ArrowUIScript;


    // Start is called before the first frame update
    void Start()
    {
        ArrowUIScript = ArrowUI.GetComponent<ArrowUI>();
        ArrowUIScript.CostOfArrow = CostOfArrow;
        ArrowUIScript.MaxArrowCharges = CostOfArrow * MaxArrowCharges;
        ArrowUIScript.LoadUI();

        Bow = Instantiate(PrefabBow, transform.position, Quaternion.identity);
        //Bow.transform.parent = gameObject.transform;
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
    
    private void ShootBow()
    {
        ArrowUIScript.ArrowFired();
        // Get UI ELEMENT

        // SHOW CHARGES

        // REGEN CHAGES

        // TAKE AWAY CHARGES
    }

    private void Aiming()
    {
        if (Input.GetKey(KeyCode.T))
        {
            Bow.SetActive(true);
            IsAiming = true;
            Transform TransformParent = GetComponentInParent<Transform>();
            Vector3 parentPos = TransformParent.transform.position;
            Vector3 targetPos = Bow.transform.position;


            Vector3 pos = Cam.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0f;
            //Debug.Log("Mouse Pos " + pos);
            //Debug.Log("ParentPos " + parentPos);
            //Debug.DrawLine(parentPos, pos);


            Vector3 direction = pos - parentPos;
            Vector3 BowPos = parentPos + direction.normalized * DistanceFromPlayer;
            Bow.transform.position = BowPos;

            targetPos.x = targetPos.x - parentPos.x;
            targetPos.y = targetPos.y - parentPos.y;

            float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
            Bow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            if (Input.GetKeyDown(KeyCode.Mouse0) && ArrowUIScript.CanFire() == true)
            {
                GameObject Arrow = Instantiate(Projectile, Bow.transform.position, Bow.transform.rotation);
                Arrow.GetComponent<Arrow>().GetDirection(direction.normalized * SpeedOfProj);
                ArrowUIScript.ArrowFired();
            }
        }
        else
        {
            IsAiming = false;
            Bow.SetActive(false);
        }
    }
}
