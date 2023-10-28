using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerRagdollManager _ragdoll;
    [SerializeField] private Collider _colliderForBullets;
    [SerializeField] private MouseTracker3D _mouseTracker;
    [SerializeField] private HarpoonShooter _harpoonShooter;

    //public event UnityAction HealthEnded;
    public event UnityAction TakedDamage;
    public event UnityAction Restarted;

    private int _initialHealth;
    private bool _isDied = false;

    public bool IsDead => _isDied;

    private void Awake()
    {
        _initialHealth = _health;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isDied == false)
        {
            Hiding();
        }
        else if (Input.GetKeyUp(KeyCode.Space) && _isDied == false)
        {
            ShowingUp();
        }
    }

    public void TakeDamage(int damage)
    {
        _animator.SetTrigger(PlayerConstants.AnimationTriggers.Hurted);
        _health -= damage;
        TakedDamage?.Invoke();

        if (_health <= 0)
        {
            _isDied = true;
            //HealthEnded?.Invoke();
            Dying();
        }
    }

    private void Dying()
    {
        _ragdoll.Activating();
        _colliderForBullets.enabled = false;
        BlockingHarpooneControl();
    }

    private void Hiding()
    {
        _colliderForBullets.enabled = false;
        _animator.SetTrigger(PlayerConstants.AnimationTriggers.SitDown);
        BlockingHarpooneControl();
    }

    private void ShowingUp()
    {
        _colliderForBullets.enabled = true;
        _animator.SetTrigger(PlayerConstants.AnimationTriggers.StandUp);
        UnblockingHarpooneControl();
    }

    private void BlockingHarpooneControl()
    {
        _mouseTracker.enabled = false;
        _harpoonShooter.BlockingShoot();
    }

    private void UnblockingHarpooneControl()
    {
        _mouseTracker.enabled = true;
        _harpoonShooter.UnblockingShoot();
    }

    private void Restart()
    {
        _health = _initialHealth;
        _isDied = false;
        _ragdoll.Deactivating();
        _colliderForBullets.enabled = true;
        UnblockingHarpooneControl();
        Restarted?.Invoke();
    }
}