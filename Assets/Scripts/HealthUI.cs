using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    /// <summary>
    /// REMEBER TO PUT NEW HEARTS IN TO THE LIST IF YOU ADD MORE OF THEM IN THE INSPECTOR
    /// </summary>

    // Maybe should be private and a function to find them instead
    public Image[] hearts;
    public GameObject player;
    public PlayerMovement playerMovement;

    [SerializeField] int health = 3;
    [SerializeField] [Range(0, 5)] int maxHealth = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        for (int i = 0; i < hearts.Length; i++)
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
        for (int i = 0; i < hearts.Length; i++)
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
        for (int i = 0; i < hearts.Length; i++)
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
