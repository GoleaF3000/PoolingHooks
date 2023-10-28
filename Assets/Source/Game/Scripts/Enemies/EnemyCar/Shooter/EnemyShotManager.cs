using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Timer))]
public class EnemyShotManager : MonoBehaviour
{
    [SerializeField] private Enemy _car;
    [SerializeField] private Animator _animator;
    [SerializeField] private Bullet _prefabBullet;
    [SerializeField] private ShootPoint _shootPoint;
    [SerializeField] private float _delayOfStart;
    [SerializeField] private float _interval;

    private Player _targetPlayer;
    private Timer _timer;    
    private Vector3 _ofsetBullet;    
    private float _forceBullet;

    private void Awake()
    {
        _timer = GetComponent<Timer>();
    }

    private void Start()
    {
        StartingTimer(_delayOfStart);
    }

    private void OnEnable()
    {
        _timer.Reached += OnShooting;
        _car.Connected += OnTurnOff;
    }

    private void OnDisable()
    {
        _timer.Reached -= OnShooting;
        _car.Connected -= OnTurnOff;
    }

    public void Init(Player player, Vector3 ofsetBullet, float forceBullet)
    {
        _targetPlayer = player;
        _ofsetBullet = ofsetBullet;
        _forceBullet = forceBullet;
    }

    private void OnShooting()
    {
        Debug.Log($"Player dead = {_targetPlayer.IsDead}");

        if (_targetPlayer.IsDead == false)
        {
            Vector3 playerWorld = _targetPlayer.transform.position;
            Vector3 shootPointWorld = _shootPoint.transform.position;
            Vector3 targetWorld = playerWorld + _ofsetBullet - shootPointWorld;

            Bullet bullet = Instantiate(_prefabBullet, _shootPoint.transform.position,
                Quaternion.identity);
            bullet.transform.LookAt(_targetPlayer.transform.position);
            bullet.GetComponent<Rigidbody>().AddForce(targetWorld * _forceBullet, ForceMode.Force);

            _animator.SetTrigger(EnemyConstants.AnimatorTriggers.Shooted);

            StartingTimer(_interval);
        }
        else
        {
            enabled = false;
        }                   
    }

    private void StartingTimer(float time)
    {
        _timer.enabled = true;
        _timer.Starting(time);
    }

    private void OnTurnOff()
    {
        enabled = false;
    }
}