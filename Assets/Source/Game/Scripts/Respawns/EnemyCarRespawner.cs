using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCarRespawner : MonoBehaviour
{
    [SerializeField] private EnemyCar _car;
    [SerializeField] private EnemyPosition _carMainPosition;
    [SerializeField] private float _durationStarting;
    [SerializeField] private Player _stickmanPlayer;
    [SerializeField] private Vector3 _ofsetBullet;
    [SerializeField] private float _forceBullet;

    private float _directionY = 180f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            EnemyCar car = Instantiate(_car, transform.position, Quaternion.Euler(0, _directionY, 0));
            car.GetComponent<StarterEnemyCar>().
                Init(_stickmanPlayer, transform.position, _carMainPosition.transform.position, 
                _ofsetBullet, _durationStarting, _forceBullet);
        }
    }
}