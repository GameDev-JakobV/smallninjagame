using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcMovePos : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.up;
    }
}
