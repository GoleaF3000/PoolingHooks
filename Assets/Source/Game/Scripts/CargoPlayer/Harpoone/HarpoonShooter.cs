using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MouseTracker3D))]
[RequireComponent(typeof(HarpoonReloader))]
public class HarpoonShooter : MonoBehaviour
{
    [SerializeField] private ParticleSystem _shotEffect;
    [SerializeField] private ShootPoint _shootPoint;
    [SerializeField] private float _force;

    public event UnityAction<int, Hook> Shooted;

    private MouseTracker3D _tracker;
    private HarpoonReloader _reloader;
    private Hook _currentHook;
    private bool _isCharged = false;
    private bool _isBlockedShoot = false;
    private bool _isFirstShoot = false;

    public Hook CurrentHook => _currentHook;

    private void Awake()
    {
        _tracker = GetComponent<MouseTracker3D>();
        _reloader = GetComponent<HarpoonReloader>();
    }

    private void OnEnable()
    {
        _reloader.Created += OnInitHook;
    }

    private void Update()
    {
        if (_isBlockedShoot == false && _isCharged == true
       && Input.GetMouseButtonDown(0))
        {
            Shooting();
        }
    }

    private void OnDisable()
    {
        _reloader.Created -= OnInitHook;
    }

    public void BlockingShoot()
    {
        _isBlockedShoot = true;
    }

    public void UnblockingShoot()
    {
        _isBlockedShoot = false;
    }

    public void Restart()
    {
        _isBlockedShoot = false;
        _isCharged = false;
        _isFirstShoot = false;
    }

    private void Shooting()
    {
        RegisteringShot();

        _currentHook.transform.SetParent(null);
        _currentHook.transform.rotation = Quaternion.LookRotation(_tracker.WorldPoint - _currentHook.transform.position);

        _currentHook.GetComponent<Rigidbody>().
             AddForce((_tracker.WorldPoint - _shootPoint.transform.position) * _force, ForceMode.Force);
        _currentHook.GetComponent<HookRotator>().enabled = true;

        _shotEffect.Play();

        _currentHook.enabled = true;
        _currentHook = null;
        _isCharged = false;


    }

    private void RegisteringShot()
    {
        if (_isFirstShoot == false)
        {
            Shooted.Invoke(IndexShot.First, _currentHook);
            _isFirstShoot = true;
        }
        else if (_isFirstShoot == true)
        {
            Shooted.Invoke(IndexShot.Second, _currentHook);
            _isFirstShoot = false;
        }
    }

    private void OnInitHook(Hook hook)
    {
        _currentHook = hook;
        _isCharged = true;
    }
}