using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(WheelsManager))]
[RequireComponent(typeof(MoverEnemy))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class EnemyDestroyer : MonoBehaviour
{
    [SerializeField] private Enemy _enemyScript;    
    [SerializeField] private ParticleSystem _explodeEffect;
    [SerializeField] private AudioSource _explosionSound;   
    [SerializeField] private float _exploseForce;
    [SerializeField] private float _exploseRadius;
    [SerializeField] private float _timeToExplose;
    [SerializeField] private float _timeToDestroy;

    public event UnityAction Exploded;
    public event UnityAction Destroyed;

    private WheelsManager _wheelsManager;
    private MoverEnemy _mover;
    private Animator _animator;

    public float TimeToExplose => _timeToExplose;
    public float TimeToDestroy => _timeToDestroy;

    private void Awake()
    {
        _wheelsManager = GetComponent<WheelsManager>();
        _mover = GetComponent<MoverEnemy>();
        _animator = GetComponent<Animator>();        
    }

    private void OnEnable()
    {
        _enemyScript.Connected += OnStarting;
    }

    private void OnDisable()
    {
        _enemyScript.Connected += OnStarting;
    }

    private void OnDestroy()
    {
        Destroyed?.Invoke();
    }

    private void OnStarting()
    {
        BanningMove();
        StartCoroutine(StartingDestruction(_timeToExplose, _timeToDestroy));
    }

    private void BanningMove()
    {
        _mover.enabled = false;        
        _animator.enabled = false;
    }

    private void Exploding()
    {
        Exploded?.Invoke();

        _explodeEffect.transform.parent = null;
        _explodeEffect.Play();
        _explosionSound.Play();        
      
        gameObject.GetComponent<Rigidbody>().AddExplosionForce
            (_exploseForce, _explodeEffect.transform.position, _exploseRadius);

        foreach (var wheel in _wheelsManager.Wheels)
        {
            wheel.gameObject.GetComponent<Rigidbody>().AddExplosionForce
            (_exploseForce, _explodeEffect.transform.position, _exploseRadius);
        }       
    }
    
    private IEnumerator StartingDestruction(float timeExplose, float timeDestroy)
    {
        float currentTime = 0f;
        bool isExploded = false;
        bool isDestroyed = false;

        while (isDestroyed == false)
        {
            currentTime += Time.deltaTime;

            if (isExploded == false && currentTime >= timeExplose)
            {
                Exploding();
                isExploded = true;
            }
           
            if (currentTime >= timeDestroy)
            {                         
                isDestroyed = true;
                Destroy(_explodeEffect.gameObject);
                Destroy(gameObject);
            }

            yield return null;
        }
    }    
}