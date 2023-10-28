using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }
}