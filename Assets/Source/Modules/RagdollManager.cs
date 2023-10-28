using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollManager : MonoBehaviour
{       
    [SerializeField] private Animator _animator;
    [SerializeField] private Collider[] _colliders;
    [SerializeField] private Rigidbody[] _rigidbodies;

    protected void Awake()
    {
        Deactivating();
    }

    public void Activating()
    {
        if (_animator != null)
        {
            _animator.enabled = false;
        }

        foreach (var collider in _colliders)
        {
            collider.enabled = true;
        }

        foreach (var rigidbody in _rigidbodies)
        {
            rigidbody.isKinematic = false;
        }        
    }

    public void Deactivating()
    {
        if (_animator != null)
        {
            _animator.enabled = true;
        }

        foreach (var collider in _colliders)
        {
            collider.enabled = false;
        }

        foreach (var rigidbody in _rigidbodies)
        {
            rigidbody.isKinematic = true;
        }
    }
}
