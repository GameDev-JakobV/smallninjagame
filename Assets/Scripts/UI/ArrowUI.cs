using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowUI : MonoBehaviour
{
    [SerializeField] Image Arrow;
    [SerializeField] GameObject Parent;
    private List<Image> Arrows;

    public float MaxArrowCharges { get; set; }
    [SerializeField] private float CurrentArrowCharges { get; set; }
    [SerializeField] public float CostOfArrow { get; set; }

    void Start()
    {

    }

    public void LoadUI()
    {
        CurrentArrowCharges = MaxArrowCharges;
        Arrows = new List<Image>();
        RectTransform rectTransformParent = Parent.GetComponent<RectTransform>();
        RectTransform rectTransform = Arrow.GetComponent<RectTransform>();
        if (Arrows.Count < MaxArrowCharges)
        {
            for (int i = Arrows.Count; i < MaxArrowCharges / CostOfArrow; i++)
            {
                Arrow.transform.position = new Vector3((rectTransform.sizeDelta.x + 20f) * Arrows.Count + rectTransformParent.sizeDelta.x * 0.05f, rectTransformParent.sizeDelta.y * 0.8f, 0f);
                Image image = Instantiate(Arrow, Arrow.transform.position, Quaternion.identity);
                image.transform.parent = gameObject.transform;
                image.color = new Color(255, 255, 255, 1f);
                Arrows.Add(image);
            };
        }
        CurrentArrowCharges = Arrows.Count;
    }

    private void Update()
    {
        //RechargeArrows();
        ArrowVisual();
    }

    private void FixedUpdate()
    {
        //Debug.Log(CurrentArrowCharges);
        if (CurrentArrowCharges < MaxArrowCharges)
        {
            CurrentArrowCharges += 0.02f;
        }
    }

    private void ArrowVisual()
    {
        int i = Mathf.FloorToInt(CurrentArrowCharges);
        if (i == Arrows.Count)
        {
            return;
        }
        if (i < 0)
        {
            i = 0;
        }
        if (i != 0)
        {
            Arrows[i - 1].color = new Color(255, 255, 255, 1f);
        }
        Arrows[i].color = new Color(255, 255, 255, CurrentArrowCharges - i);
        if (i == Arrows.Count - 1)
        {
            return;
        }
        Arrows[i + 1].color = new Color(255, 255, 255, 0f);
    }

    public void ArrowFired()
    {
        CurrentArrowCharges -= CostOfArrow;
    }

    public bool CanFire()
    {
        if (CurrentArrowCharges - CostOfArrow < 0)
        {
            return false;
        }
        return true;
    }

    private void RechargeArrows()
    {
        if (CurrentArrowCharges < MaxArrowCharges)
        {
            CurrentArrowCharges += Time.deltaTime;
        }
    }
}
