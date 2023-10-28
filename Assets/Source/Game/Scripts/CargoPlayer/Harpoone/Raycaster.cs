using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(HarpoonShooter))]
[RequireComponent(typeof(MouseTracker3D))]
public class Raycaster : MonoBehaviour
{    
    [SerializeField] private float _maxDistance;
    [SerializeField] private ShootPoint _shootPoint;

    public event UnityAction<int, Hook, ObjectHit> Raycasted;

    private HarpoonShooter _shooter;
    private MouseTracker3D _tracker;
    private ObjectHit _objectHit;

    private void Awake()
    {
        _shooter = GetComponent<HarpoonShooter>();
        _tracker = GetComponent<MouseTracker3D>();
    }

    private void OnEnable()
    {
        _shooter.Shooted += OnRaycasting;
    }

    private void OnDisable()
    {
        _shooter.Shooted -= OnRaycasting;
    }

    private void OnRaycasting(int indexShot, Hook hook)
    {
        Physics.Raycast(_shootPoint.transform.position, _tracker.WorldPoint - _shootPoint.transform.position, 
            out RaycastHit hit, _maxDistance);

        _objectHit = new ObjectHit(hit.collider.gameObject, 
            hit.collider.gameObject.transform.InverseTransformPoint(hit.point));

        Raycasted?.Invoke(indexShot, hook, _objectHit); 
    }
}