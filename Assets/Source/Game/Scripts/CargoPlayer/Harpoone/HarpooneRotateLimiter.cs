using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpooneRotateLimiter : MonoBehaviour
{
    [SerializeField] private Vector2 _minValue;
    [SerializeField] private Vector2 _maxValue;

    private float _valueX;
    private float _valueY;
    private float _valueZ;
    private float _correctionValue = 50;


    private void Update()
    {
       
        _valueX = transform.localEulerAngles.x;
        _valueY = transform.localEulerAngles.y;
        _valueZ = transform.localEulerAngles.z;

        
        if (_valueX >= 10 && _valueX < 10 + _correctionValue)
        {
            Debug.Log($"Low! {10 + _correctionValue} > {_valueX} >= 10");
        }
        else if (_valueX <= 350 && _valueX > 350 - _correctionValue)
        {
            Debug.Log($"UP! 350 >= {_valueX} > {350 - _correctionValue}");
        }

        if (_valueY >= _maxValue.y || _valueY <= _minValue.y)
        {
            _valueY = Mathf.Clamp(_valueY, _minValue.y, _maxValue.y);
        }
        


        Debug.Log($"X = {_valueX}; Y = {_valueY}");
        //Debug.Log($"X = {transform.eulerAngles.x}; Y = {transform.eulerAngles.y}");

        transform.localEulerAngles = new Vector3(_valueX, _valueY, _valueZ);
        
    }
}