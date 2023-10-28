using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour 
{
    [SerializeField] float _delayBeforeDestroy;

    private void Start()
    {
        Destroy(gameObject, _delayBeforeDestroy);
    }
}