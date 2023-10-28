using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{   
    public event UnityAction Reached;

    private float _responseTime;
    private bool _isStart = false;

    private void Update()
    {
        if (_isStart == true)
        {
            _responseTime -= Time.deltaTime;            

            if (_responseTime <= 0)
            {
                _isStart = false;
                enabled = false;
                Reached?.Invoke();
            }
        }
    }

    public void Starting(float time)
    {
        _responseTime = time;
        _isStart = true;
    }
}