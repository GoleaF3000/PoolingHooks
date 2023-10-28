using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Hook))]
[RequireComponent(typeof(HookRotator))]
[RequireComponent(typeof(MeshRenderer))]
public class HookTrigger : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Hook _hook;
    private HookRotator _rotator;
    private HooksManager _manager;
    private MeshRenderer _mesh;
    private SurfaceForHook _connectedSurface = null;
    private Enemy _connectedEnemy = null;
    private bool _isHit = false;
    private bool _isNeedRestart = false;    

    public SurfaceForHook ConnectedSurface => _connectedSurface;
    public Enemy ConnectedEnemy => _connectedEnemy;
    public bool IsHit => _isHit;
    public bool IsNeedRestart => _isNeedRestart;   

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _hook = GetComponent<Hook>();
        _rotator = GetComponent<HookRotator>();
        _mesh = GetComponent<MeshRenderer>();
        enabled = false;
    }

    private void OnEnable()
    {
        _manager = _hook.Manager;
        _manager.TwoHooksHitted += OnStartingEvent;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<SurfaceForHook>(out SurfaceForHook surface))
        {
            _connectedSurface = surface;
        }
        else if (collider.TryGetComponent<Enemy>(out Enemy enemy))
        {
            _connectedEnemy = enemy;
        }

        if (_connectedSurface != null || _connectedEnemy != null)
        {           
            if (gameObject.TryGetComponent<HookDestroyer>(out HookDestroyer destroyer) == false)
            {
                gameObject.AddComponent<HookDestroyer>();
            }

            _rigidbody.velocity = new Vector3(0, 0, 0);
            _isHit = true;
            _rotator.enabled = false;
            _mesh.enabled = false;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent<ShootArea>(out ShootArea shootArea))
        {
            _isNeedRestart = true;
        }
    }

    private void OnDisable()
    {
        _manager.TwoHooksHitted -= OnStartingEvent;
    }

    private void OnStartingEvent()
    {
        if (_connectedSurface != null)
        {
            _connectedSurface.StartingEventConnecting();
        }
        else if (_connectedEnemy != null)
        {
            _connectedEnemy.StartingEventConnecting();
        }
    }
}