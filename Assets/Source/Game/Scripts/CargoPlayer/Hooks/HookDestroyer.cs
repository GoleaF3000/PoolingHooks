using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HookTrigger))]
public class HookDestroyer : MonoBehaviour
{
    private SurfaceForHook _surface;
    private HookTrigger _trigger;
    private Enemy _enemy;

    private void Awake()
    {
        _trigger = GetComponent<HookTrigger>();

        if (_trigger.ConnectedEnemy != null)            
        {
            _enemy = _trigger.ConnectedEnemy;            
        }
        else if (_trigger.ConnectedSurface != null)
        {
            _surface = _trigger.ConnectedSurface;
        }
    }

    private void OnEnable()
    {
        if (_enemy != null)
        {
            //_enemy.Destroyer.Destroyed += OnDestroying;
            _enemy.Destroyer.Exploded += OnDestroying;
        }
        else if (_surface != null)
        {
            _surface.Destroyed += OnDestroying;
        }
    }

    private void OnDisable()
    {
        if (_enemy != null)
        {
            //_enemy.Destroyer.Destroyed -= OnDestroying;
            _enemy.Destroyer.Exploded -= OnDestroying;
        }
        else if (_surface != null)
        {
            _surface.Destroyed -= OnDestroying;
        }
    }

    private void OnDestroying()
    {
        Destroy(gameObject);
    }
}