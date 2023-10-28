using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _delayToDestroy;

    private void Start()
    {
        Destroy(gameObject, _delayToDestroy);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<Player>(out Player player) == true)
        {
            player.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
