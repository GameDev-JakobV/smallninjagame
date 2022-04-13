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

    float alpha = 0.05f;
    int whichArrow = 0;
    int temp3 = 0;

    void Start()
    {

        /*
        Arrows = new List<Image>();
        RectTransform rectTransformParent = Parent.GetComponent<RectTransform>();
        RectTransform rectTransform = Arrow.GetComponent<RectTransform>();
        //Debug.Log(rectTransformParent.sizeDelta.y);
        if (Arrows.Count < MaxArrowCharges)
        {
            for (int i = Arrows.Count; i < MaxArrowCharges/CostOfArrow; i++)
            {
                Arrow.transform.position = new Vector3((rectTransform.sizeDelta.x + 20f) * Arrows.Count + rectTransformParent.sizeDelta.x * 0.05f, rectTransformParent.sizeDelta.y * 0.8f, 0f);
                Image image = Instantiate(Arrow, Arrow.transform.position, Quaternion.identity);
                image.transform.parent = gameObject.transform;
                image.color = new Color(255, 255, 255, 255);
                Arrows.Add(image);
            };
        }
        */
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
    }

    private void Update()
    {
        RechargeArrows();
        ArrowVisual();
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

    private void ArrowVisual()
    {
        // TODO: Pretty sure this is all dumb so remove and start from the beginning
        float temp = CurrentArrowCharges / CostOfArrow;
        
        var temp1 = Mathf.Round(temp * 10.0f) * 0.1f;
        Debug.Log(temp1 % 1 == 0);
        if (temp1 % 1 == 0)
        {
            float temp2 = (temp1 / MaxArrowCharges);
            temp3 = Mathf.RoundToInt(temp2 * MaxArrowCharges);
            Debug.Log(temp3);
            whichArrow = temp3 * 100;
        }
        Debug.Log(whichArrow);
        switch (whichArrow)
        {
            case -1:
                break;
            case int n when n <= 100:
                Arrows[0].color = new Color(255, 255, 255, alpha += 0.25f);
                Arrows[1].color = new Color(255, 255, 255, 0);
                Arrows[2].color = new Color(255, 255, 255, 0);
                Arrows[3].color = new Color(255, 255, 255, 0);
                break;
            case int n when n <= 200:
                Arrows[0].color = new Color(255, 255, 255, 1);
                Arrows[1].color = new Color(255, 255, 255, 1);
                Arrows[2].color = new Color(255, 255, 255, 0);
                Arrows[3].color = new Color(255, 255, 255, 0);
                break;
            case int n when n <= 300:
                Arrows[0].color = new Color(255, 255, 255, 1);
                Arrows[1].color = new Color(255, 255, 255, 1);
                Arrows[2].color = new Color(255, 255, 255, 1);
                Arrows[3].color = new Color(255, 255, 255, 0);
                break;
            case int n when n <= 400:
                Arrows[0].color = new Color(255, 255, 255, 1);
                Arrows[1].color = new Color(255, 255, 255, 1);
                Arrows[2].color = new Color(255, 255, 255, 1);
                Arrows[3].color = new Color(255, 255, 255, 1);
                break;
            case int n when n < 500:
                break;
            case int n when n < 600:
                break;
            case int n when n < 700:
                break;
            default:
                break;
        }

    }

    public IEnumerator IEArrow(Image WhichArrow)
    {

        yield return null;
    }

    private void RechargeArrows()
    {
        if (CurrentArrowCharges < MaxArrowCharges)
        {
            CurrentArrowCharges += Time.deltaTime;
        }

    }

}
