using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class IKController : MonoBehaviour
{  
    [SerializeField] private Transform _lookObject;
    [SerializeField] private Transform _rightHandObject;
    [SerializeField] private Transform _lefthandObject;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK()
    {
        if (_lookObject != null)
        {
            _animator.SetLookAtWeight(1);
            _animator.SetLookAtPosition(_lookObject.position);
        }

        if (_rightHandObject != null)
        {
            _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            //_animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
            _animator.SetIKPosition(AvatarIKGoal.RightHand, _rightHandObject.position);
            //_animator.SetIKRotation(AvatarIKGoal.RightHand, _rightHandObject.rotation);            
            _animator.SetBoneLocalRotation(HumanBodyBones.RightRingIntermediate, _rightHandObject.rotation);
        }

        if (_lefthandObject != null)
        {
            _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
            _animator.SetIKPosition(AvatarIKGoal.LeftHand, _lefthandObject.position);
            _animator.SetIKRotation(AvatarIKGoal.LeftHand, _lefthandObject.rotation);
        }
    }
}