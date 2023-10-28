using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTracker3D : MonoBehaviour
{
    [SerializeField] [Range(0.1f, 5f)] private float _speedRotation;
    [SerializeField] [Range(0f, -1f)] private float _offsetLeft;
    [SerializeField] [Range(0f, 1f)] private float _offsetRight;

    private Camera _camera;
    private Quaternion _quaternion;
    private Vector3 _worldPoint;    

    public Vector3 WorldPoint => _worldPoint;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        Ray rayMouse = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(rayMouse, out RaycastHit rayMouseHit))
        {
            Vector3 mouseInWorld = rayMouseHit.point;      

            _quaternion = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(mouseInWorld - transform.position), _speedRotation * Time.deltaTime);

            if (_quaternion.y <= _offsetLeft)
            {
                _quaternion.y = _offsetLeft;
            }

            if (_quaternion.y >= _offsetRight)
            {
                _quaternion.y = _offsetRight;
            }

            transform.rotation = new Quaternion(0f, _quaternion.y, 0, _quaternion.w);

            _worldPoint = mouseInWorld;           
        }
    }
}