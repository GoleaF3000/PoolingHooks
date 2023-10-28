using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HooksManager : MonoBehaviour
{
    [SerializeField] private Raycaster _raycaster;

    public event UnityAction Restarted;
    public event UnityAction TwoHooksHitted;

    private Hook _firstHook = null;
    private Hook _secondHook = null;
    private HookTrigger _firstHookTrigger;
    private HookTrigger _secondHookTrigger;
    private ObjectHit _firstObjectHit;
    private ObjectHit _secondObjectHit;    

    private void OnEnable()
    {
        _raycaster.Raycasted += OnInit;
    }

    private void FixedUpdate()
    {
        if ((_firstHook != null && _firstHookTrigger.IsNeedRestart == true) ||
            (_secondHook != null && _secondHookTrigger.IsNeedRestart == true))
        {
            Restart();
        }

        if ((_firstHook != null && _firstHookTrigger.IsHit == true) &&
            (_secondHook != null && _secondHookTrigger.IsHit == true))
        {
            if (_firstObjectHit.Subject == _secondObjectHit.Subject
                || (_firstObjectHit.Subject.TryGetComponent<Road>(out Road roadOne)
                && _secondObjectHit.Subject.TryGetComponent<Road>(out Road roadTwo)))
            {
                Restart();
            }
            else if (_firstObjectHit.Subject != _secondObjectHit.Subject)
            {
                float timeDestroy = 0f;

                if (_firstHookTrigger.ConnectedEnemy != null)
                {
                    timeDestroy = _firstHookTrigger.ConnectedEnemy.
                        GetComponent<EnemyDestroyer>().TimeToExplose;
                }
                else if (_secondHookTrigger.ConnectedEnemy != null)
                {
                    timeDestroy = _secondHookTrigger.ConnectedEnemy.
                        GetComponent<EnemyDestroyer>().TimeToExplose;
                }

                TwoHooksHitted?.Invoke();
                Cleaning();
            }   
        }
    }

    private void OnDisable()
    {
        _raycaster.Raycasted -= OnInit;
    }

    private void OnInit(int indexShot, Hook hook, ObjectHit objectHit)
    {
        if (indexShot == IndexShot.First)
        {
            _firstHook = hook;
            _firstHookTrigger = hook.GetComponent<HookTrigger>();
            _firstObjectHit = objectHit;
        }
        else if (indexShot == IndexShot.Second)
        {
            _secondHook = hook;
            _secondHookTrigger = hook.GetComponent<HookTrigger>();
            _secondObjectHit = objectHit;
        }
    }

    private void Cleaning()
    {
        _firstHook = null;
        _firstObjectHit = null;

        _secondHook = null;
        _secondObjectHit = null;
    }

    private void Restart()
    {  
        if (_firstHook != null)
        {
            Destroy(_firstHook.gameObject);
        }

        if (_secondHook != null)
        {
            Destroy(_secondHook.gameObject);
        }

        Cleaning();
        Restarted?.Invoke();        
    }
}