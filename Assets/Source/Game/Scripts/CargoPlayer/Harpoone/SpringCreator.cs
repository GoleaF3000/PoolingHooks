using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Raycaster))]
public class SpringCreator : MonoBehaviour
{
    [SerializeField] private HooksManager _hooksManager;
    [SerializeField] private float _damperValue;
    [SerializeField] private float _springValue;
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _toleranceValue;
    
    private Raycaster _raycaster;  
    private SpringJoint _springJoint;   
    private ObjectHit _firstObjectHit;
    private ObjectHit _secondObjectHit;
    private float _minDistance = 0;   

    private void Awake()
    {        
        _raycaster = GetComponent<Raycaster>();
    }

    private void OnEnable()
    {
        _raycaster.Raycasted += OnInit;
        _hooksManager.TwoHooksHitted += OnCreatingSpring;
    }

    private void OnDisable()
    {
        _raycaster.Raycasted -= OnInit;
        _hooksManager.TwoHooksHitted -= OnCreatingSpring;
    }

    public void Restart()
    {        
        _firstObjectHit = null;
        _secondObjectHit = null;
    }

    private void OnInit(int indexShot, Hook hook, ObjectHit objectHit)
    {      
        if (indexShot == IndexShot.First)
        {           
            _firstObjectHit = objectHit;            
        }
        else if (indexShot == IndexShot.Second)
        {              
            _secondObjectHit = objectHit;            
        }
    }

    private void CreatingSpring(ObjectHit firstObject, ObjectHit secondObject)
    {        
        _springJoint = secondObject.Subject.gameObject.AddComponent<SpringJoint>();

        CreatorSpringDestroyer creatorDestroyer = secondObject.Subject.
            gameObject.AddComponent<CreatorSpringDestroyer>();
        creatorDestroyer.Init(_springJoint, firstObject);
        //secondObject.Subject.gameObject.AddComponent<SpringDestroyer>(); //Вызовет Inizializator, чтобы поля заполнились во время Awake

        _springJoint.enableCollision = true;
        _springJoint.autoConfigureConnectedAnchor = false;
        _springJoint.anchor = secondObject.HitPosition;

        _springJoint.connectedBody = firstObject.Subject.gameObject.GetComponent<Rigidbody>();
        _springJoint.connectedAnchor = firstObject.HitPosition;

        _springJoint.maxDistance = _maxDistance;
        _springJoint.minDistance = _minDistance;
        _springJoint.tolerance = _toleranceValue;

        _springJoint.damper = _damperValue;
        _springJoint.spring = _springValue;
    }   
    
    private void OnCreatingSpring()
    {
        CreatingSpring(_firstObjectHit, _secondObjectHit);
        Restart();
    }
}