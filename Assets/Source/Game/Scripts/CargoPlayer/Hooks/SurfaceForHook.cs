using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SurfaceForHook : MonoBehaviour
{
    [SerializeField] private bool _isFixed;

    public event UnityAction Connected;
    public event UnityAction Destroyed;

    private bool _isReached = false;

    private void OnDestroy()
    {
        Destroyed?.Invoke();
    }

    public void StartingEventConnecting()
    {
        if (_isFixed == false && _isReached == false)
        {
            Connected?.Invoke();
            _isReached = true;
        }
    }
}