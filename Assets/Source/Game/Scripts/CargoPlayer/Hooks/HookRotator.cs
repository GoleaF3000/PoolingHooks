using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookRotator : MonoBehaviour
{
    [SerializeField] private float _speedRotate;    

    private void Awake()
    {
        enabled = false;
    }

    private void Update()
    {
        transform.Rotate(0, 0, _speedRotate * Time.deltaTime);
    }
}