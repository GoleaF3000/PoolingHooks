using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BungeeRenderer : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private int _positionCount = 2;   
    private Vector3 _firstPosition = Vector3.zero;
    private Vector3 _secondPosition = Vector3.zero;
    private ShootPoint _shootPoint = null;
    private Hook _firstHook = null;
    private Hook _secondHook = null;
    private HookTrigger _firstHookTrigger;
    private HookTrigger _secondHookTrigger;
    private ObjectHit _firstObject;
    private ObjectHit _secondObject;
    private bool _isFirstBlock = false;
    private bool _isSecondBlock = false;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = _positionCount;        
    }

    private void Update()
    {
        if ((_isFirstBlock == true && _firstHook == null) ||
            (_isSecondBlock == true && _secondHook == null))
        {
            enabled = false;
        }

        if (_isFirstBlock == false && _firstHook != null && _firstHookTrigger.IsHit == false)
        {
            _firstPosition = _firstHook.transform.position;            
        }
        else if (_firstHook != null && _firstHookTrigger.IsHit == true)
        {
            _firstPosition = _firstObject.Subject.gameObject.transform.TransformPoint(_firstObject.HitPosition);
            _isFirstBlock = true;           
        }
       
        if (_isSecondBlock == false && _secondHook == null && _shootPoint != null)
        {       
            _secondPosition = _shootPoint.transform.position;           
        }
        else if (_isSecondBlock == false && _secondHook != null && _secondHookTrigger.IsHit == false)
        {
            _secondPosition = _secondHook.transform.position;            
        }
        else if (_secondHook != null && _secondHookTrigger.IsHit == true)
        {
            _secondPosition = _secondObject.Subject.gameObject.transform.TransformPoint(_secondObject.HitPosition);
            _isSecondBlock = true;           
        }

        _lineRenderer.SetPosition(0, _firstPosition);
        _lineRenderer.SetPosition(1, _secondPosition);
    }

    public void FirstInit(Hook firstHook, ObjectHit firstObject, ShootPoint shootPoint, float widthValue, Material material)
    {
        _firstHook = firstHook;
        _firstHookTrigger = firstHook.GetComponent<HookTrigger>();
        _firstObject = firstObject;
        _shootPoint = shootPoint;        
        _lineRenderer.startWidth = widthValue;
        _lineRenderer.endWidth = widthValue;
        _lineRenderer.material = material;
    }

    public void SecondInit(Hook secondHook, ObjectHit secondObject)
    {
        _secondHook = secondHook;
        _secondHookTrigger = secondHook.GetComponent<HookTrigger>();
        _secondObject = secondObject;
    }    
}