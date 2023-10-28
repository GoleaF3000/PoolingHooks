using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyDestroyer))]
public class WheelsManager : MonoBehaviour
{
    [SerializeField] private Wheel[] _wheels;
    [SerializeField] private float _speed;
    
    private EnemyDestroyer _destroyer;
    private bool _isMoving = true;

    public Wheel[] Wheels => _wheels;

    private void Awake()
    {        
        _destroyer = GetComponent<EnemyDestroyer>();
    }

    private void OnEnable()
    {       
        _destroyer.Exploded += OnFell;
    }

    private void Update()
    {
        if (_isMoving == true)
        {
            foreach (var wheel in _wheels)
            {
                wheel.gameObject.transform.Rotate(_speed * Time.deltaTime, 0, 0);
            }
        }
    }

    private void OnDisable()
    {        
        _destroyer.Exploded -= OnFell;
    }

    private void OnFell()
    {
        _isMoving = false;

        foreach (var wheel in _wheels)
        {
            wheel.transform.parent = null;
            wheel.gameObject.AddComponent<Rigidbody>();
            Destroy(wheel.gameObject, _destroyer.TimeToDestroy - _destroyer.TimeToExplose);
        }
    }
}