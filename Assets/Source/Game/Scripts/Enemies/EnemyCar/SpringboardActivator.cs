using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringboardActivator : MonoBehaviour
{
    [SerializeField] private EnemyRagdollManager _ragdollActivator;
    [SerializeField] private Spine _shooterSpine;
    [SerializeField] private Rifle _rifle;
    [SerializeField] private float _force;
    [SerializeField] private float _radius;

    private void Start()
    {
        _shooterSpine.gameObject.GetComponent<Rigidbody>().
            AddExplosionForce(_force, transform.position, _radius);

        _rifle.gameObject.GetComponent<Rigidbody>().
            AddExplosionForce(_force, transform.position, _radius);
    }
}