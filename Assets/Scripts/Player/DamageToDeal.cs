using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageToDeal : MonoBehaviour
{
    [SerializeField] private int SwordDamage = 10;

    public int MySwordDamage
    {
        get { return SwordDamage; }
        set { SwordDamage = value; }
    }
}
