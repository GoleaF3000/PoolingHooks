using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatorSpringDestroyer : MonoBehaviour
{
    private SpringJoint _spring;
    private ObjectHit _firstObject;
    private EnemyDestroyer _enemyDestroyerTrigger;

    public EnemyDestroyer EnemyDestroyerTrigger => _enemyDestroyerTrigger;

    public void Init(SpringJoint spring ,ObjectHit firstObject)
    {
        _spring = spring;
        _firstObject = firstObject;

        TryCreate();
    }

    private bool TryCreate()
    {
        bool result = true;

        if (TryDefineTriggerObject() == true)
        {
            SpringDestroyer destroyer = gameObject.AddComponent<SpringDestroyer>();
            destroyer.Init(_spring);
        }
        else
        {
            result = false;
        }

        return result;
    }

    private bool TryDefineTriggerObject()
    {
        bool result = true;

        if (gameObject.
            TryGetComponent<Enemy>(out Enemy enemySecondObject) == true)
        {
            _enemyDestroyerTrigger = enemySecondObject.Destroyer;
        }
        else if (_firstObject.Subject.
            TryGetComponent<Enemy>(out Enemy enemyFirstObject) == true)
        {
            _enemyDestroyerTrigger = enemyFirstObject.Destroyer;
        }
        else
        {
            result = false;
        }

        return result;
    }
}