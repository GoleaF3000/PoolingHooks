using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CreatorSpringDestroyer))]
public class SpringDestroyer : MonoBehaviour
{
    private CreatorSpringDestroyer _creator;
    private EnemyDestroyer _enemyDestroyerTrigger;
    private SpringJoint _spring;

    private void Awake()
    {
        _creator = GetComponent<CreatorSpringDestroyer>();

        if (_creator.EnemyDestroyerTrigger != null)
        {
            _enemyDestroyerTrigger = _creator.EnemyDestroyerTrigger;
        }
    }

    private void OnEnable()
    {
        if (_enemyDestroyerTrigger != null)
        {
            _enemyDestroyerTrigger.Exploded += OnDestroySpring;
        }
    }

    private void OnDisable()
    {
        if (_enemyDestroyerTrigger != null)
        {
            _enemyDestroyerTrigger.Exploded -= OnDestroySpring;
        }
    }

    public void Init(SpringJoint spring)
    {
        _spring = spring;
    }

    private void OnDestroySpring()
    {
        Destroy(_spring);
        enabled = false;
    }
}