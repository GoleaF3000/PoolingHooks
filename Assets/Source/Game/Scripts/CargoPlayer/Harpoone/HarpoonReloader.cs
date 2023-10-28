using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(HarpoonShooter))]
[RequireComponent(typeof(Timer))]
public class HarpoonReloader : MonoBehaviour
{
    [SerializeField] private HooksManager _hooksManager;
    [SerializeField] private ShootPoint _shootPoint;
    [SerializeField] private Hook _prefabHook;
    [SerializeField] private float _shortDelay;
    [SerializeField] private float _longDelay;

    public event UnityAction<Hook> Created;    
   
    private HarpoonShooter _shooter;
    private Timer _timer;
    private Hook _createdHook;

    private void Awake()
    {        
        _shooter = GetComponent<HarpoonShooter>();
        _timer = GetComponent<Timer>();
    }

    private void Start()
    {        
        StartingTimer(_longDelay);
    }

    private void OnEnable()
    {
        _timer.Reached += OnCreate;
        _shooter.Shooted += OnStartTimer;
    }

    private void OnDisable()
    {
        _timer.Reached -= OnCreate;
        _shooter.Shooted -= OnStartTimer;
    }

    public void Restart()
    {
        if (_createdHook != null)
        {
            Destroy(_createdHook.gameObject);
        }

        StartingTimer(_shortDelay);
    }

    private void OnCreate()
    {        
        _createdHook = Instantiate(_prefabHook, _shootPoint.transform.position,
            _shootPoint.transform.rotation, gameObject.transform);
        _createdHook.Init(_hooksManager);

        Created?.Invoke(_createdHook);
    }

    private void OnStartTimer(int indexShot, Hook hook)
    {
        float time = 0;

        if (indexShot == IndexShot.First)
        {
            time = _shortDelay;
        }
        else if (indexShot == IndexShot.Second)
        {
            time = _longDelay;
        }

        StartingTimer(time);
    }

    private void StartingTimer(float delay)
    {
        _timer.enabled = true;
        _timer.Starting(delay);
    }
}