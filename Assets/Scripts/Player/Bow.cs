using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] float MaxArrowCharges = 2f;
    [SerializeField] float ArrowCD = 0.2f;
    [SerializeField] float SpeedOfProj = 800f;
    [SerializeField] GameObject Projectile;

    public PlayerMovement player;
    //called Bow1 cause class is named Bow
    public GameObject Bow1;
    public Camera Cam;
    public float DistanceFromZero = 12.2f;
    [Header("UI")]
    public GameObject ArrowUI;
    private ArrowUI ArrowUIScript;
   
    static float CostOfArrow = 1f;
    private bool IsAiming = false;
    private bool Fired = false;

    // Start is called before the first frame update
    void Start()
    {
        ArrowUIScript = ArrowUI.GetComponent<ArrowUI>();
        ArrowUIScript.CostOfArrow = CostOfArrow;
        ArrowUIScript.MaxArrowCharges = CostOfArrow * MaxArrowCharges;
        ArrowUIScript.LoadUI();

        transform.position = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        FireAndRotateBow();
    }

    void FireAndRotateBow()
    {
        if (Input.GetKey(KeyCode.T))
        {
            Transform TransformParent = GetComponentInParent<Transform>();
            Vector3 parentPos = TransformParent.transform.position;
            Vector3 targetPos = Bow1.transform.position;


            Vector3 pos = Cam.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0f;
            //Debug.Log("Mouse Pos " + pos);
            //Debug.Log("ParentPos " + parentPos);
            //Debug.DrawLine(parentPos, pos);


            Vector3 direction = pos - parentPos;
            Vector3 BowPos = parentPos + direction.normalized * DistanceFromZero;
            Bow1.transform.position = BowPos;

            targetPos.x = targetPos.x - parentPos.x;
            targetPos.y = targetPos.y - parentPos.y;

            float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
            Bow1.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            if (Input.GetKeyDown(KeyCode.Mouse0) && ArrowUIScript.CanFire() == true && Fired == false)
            {
                StartCoroutine(HaveFired());
                GameObject Arrow = Instantiate(Projectile, Bow1.transform.position, Bow1.transform.rotation);
                Arrow.GetComponent<Arrow>().GetDirection(direction.normalized * SpeedOfProj);
                ArrowUIScript.ArrowFired();
            }
        }
    }

    private IEnumerator HaveFired()
    {
        Fired = true;
        yield return new WaitForSeconds(ArrowCD);
        Fired = false;
    }
}
