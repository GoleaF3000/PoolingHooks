using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyDestroyer))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyStarter _enemyStarter;
    [SerializeField] private int _rewardPoints;

    public event UnityAction Connected;

    private EnemyDestroyer _destroyer;
    private bool _isConnected = false;
    
    public EnemyDestroyer Destroyer => _destroyer;
    public bool IsConnected => _isConnected;

    private void Awake()
    {
        _destroyer = GetComponent<EnemyDestroyer>();
        OnSetIgnoreRaycast();
    }

    private void OnEnable()
    {
        _enemyStarter.Reached += OnSetDefaultLayer;
        Connected += OnSetIgnoreRaycast;
    }

    private void OnDisable()
    {
        _enemyStarter.Reached += OnSetDefaultLayer;
        Connected -= OnSetIgnoreRaycast;
    }

    public void StartingEventConnecting()
    {
        if (_isConnected == false)
        {
            Connected?.Invoke();
            _isConnected = true;
        }
    }

    private void OnSetIgnoreRaycast()
    {
        gameObject.layer = EnemyConstants.LayerNumber.IgnoreRaycast;
    }

    private void OnSetDefaultLayer()
    {
        gameObject.layer = EnemyConstants.LayerNumber.Default;
    }
}