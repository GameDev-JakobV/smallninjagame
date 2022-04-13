using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] Image Heart;
    [SerializeField] GameObject Parent;
    private List<Image> hearts;


    [SerializeField] int health = 8;
    [SerializeField] [Range(0, 10)] int maxHealth = 10;
    
    void Start()
    {
        hearts = new List<Image>();
        RectTransform rectTransformParent = Parent.GetComponent<RectTransform>(); 
        RectTransform rectTransform = Heart.GetComponent<RectTransform>();
        //Debug.Log(rectTransformParent.sizeDelta.y);
        if (hearts.Count < health)
        {
            for (int i = hearts.Count; i < health; i++)
            {
                Heart.transform.position = new Vector3 (rectTransform.sizeDelta.x * hearts.Count + rectTransformParent.sizeDelta.x * 0.05f, rectTransformParent.sizeDelta.y * 0.9f, 0f);
                Image image = Instantiate(Heart, Heart.transform.position, Quaternion.identity);
                image.transform.parent = gameObject.transform;
                image.color = new Color(255, 255, 255, 255);
                hearts.Add(image);
            };
        }
    }


    public void Death()
    {
        //Kill player object

        //Show deathScreen

        //Return to last checkpoint
    }

    // should maybe not be able to into MINUS but idc at this moment
    public void TakeDamage()
    {
        health--;
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < health)
            {
                hearts[i].color = new Color(255, 255, 255, 255);
            }
            else
            {
                hearts[i].color = new Color(255, 255, 255, 0);
            }
        }
    }

    public void GainHealth()
    {
        if (health >= maxHealth) { return; }
        health++;
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < health)
            {
                hearts[i].color = new Color(255, 255, 255, 255);
            }
            else
            {
                hearts[i].color = new Color(255, 255, 255, 0);
            }
        }
    }
}
