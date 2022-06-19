using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpPointCreator : MonoBehaviour
{
    [HideInInspector]
    public LerpController _LerpController;

    public void CreatePath()
    {
        _LerpController = new LerpController(transform.position);
    }
}
