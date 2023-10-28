using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Timer))]
public class RoadCreator : MonoBehaviour
{
    [SerializeField] private Road _prefabRoad;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private CargoTrigger _cargoTrigger;

    private Timer _timer;
    private float _delay = 1f;
    private bool _isAllowCreate = true;

    private void Awake()
    {
        _timer = GetComponent<Timer>();       
        OnCreate();
    }

    private void OnEnable()
    {
        _cargoTrigger.EndRoadReached += OnCreate;
        _timer.Reached += OnAllowingCreate;
    }

    private void OnDisable()
    {
        _cargoTrigger.EndRoadReached -= OnCreate;
        _timer.Reached -= OnAllowingCreate;
    }

    private void OnCreate()
    {  
        if (_isAllowCreate == true)
        {
            Road road = Instantiate(_prefabRoad, transform.position + _offset, Quaternion.identity);
            
            _isAllowCreate = false;
            _timer.enabled = true;
            _timer.Starting(_delay);            
        }        
    }

    private void OnAllowingCreate()
    {
        _isAllowCreate = true;       
    }
}