using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyRagdollManager : RagdollManager
{
    [SerializeField] private SpringboardActivator _springboard;
    [SerializeField] private EnemyCar _enemyCar;    

    public event UnityAction Activated;

    private void OnEnable()
    {
        _enemyCar.Connected += OnActivating;
    }

    private void OnDisable()
    {
        _enemyCar.Connected -= OnActivating;
    }

    private void OnActivating()
    {
        Activating();

        _springboard.enabled = true;

        Activated?.Invoke();
    }
}