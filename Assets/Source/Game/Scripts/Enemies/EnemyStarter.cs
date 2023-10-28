using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyStarter : MonoBehaviour
{
    [SerializeField] private MoverEnemy _mover;
    [SerializeField] private EnemyShotManager _shotManager;

    public event UnityAction Reached;

    private Vector3 _createPosition;
    private Vector3 _targetPosition;
    private bool _isInit = false;
    private float _timer = 0;
    private float _duration;

    private void Awake()
    {
        _mover.enabled = false;
    }

    private void Update()
    {
        if (_isInit == true && _timer <= _duration)
        {
            if (_timer > _duration)
            {
                _timer = 1;
            }

            transform.position = Vector3.Lerp(_createPosition, _targetPosition, _timer / _duration);

            _timer += Time.deltaTime;
        }
        else
        {
            Reached?.Invoke();
            _mover.enabled = true;
            enabled = false;
        }
    }

    public void Init(Player player, Vector3 createPosition, Vector3 targetPosition,
        Vector3 ofsetBullet, float durationStarting, float forceBullet)
    {
        _createPosition = createPosition;
        _targetPosition = targetPosition;
        _duration = durationStarting;

        _shotManager.Init(player, ofsetBullet, forceBullet);

        _isInit = true;
    }
}
