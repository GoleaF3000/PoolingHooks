using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BungeeRenderCreator : MonoBehaviour
{
    [SerializeField] private Raycaster _raycaster;
    [SerializeField] private ShootPoint _shootPoint;
    [SerializeField] private float _widthValue;
    [SerializeField] private Material _material;
    
    private BungeeRenderer _renderer;    

    private void OnEnable()
    {
        _raycaster.Raycasted += OnWorking;
    }

    private void OnDisable()
    {
        _raycaster.Raycasted -= OnWorking;
    }

    public void Restart()
    {
        
    }

    private void OnWorking(int indexShot, Hook hook, ObjectHit objectHit)
    {     
        if (indexShot == IndexShot.First)
        {           
            Create(hook, objectHit);
        }
        else if (indexShot == IndexShot.Second)
        {
            _renderer.SecondInit(hook, objectHit);
            _renderer = null;
        }
    }

    private void Create(Hook firstHook, ObjectHit firstObject)
    {      
        _renderer = firstHook.gameObject.AddComponent<BungeeRenderer>();        
        
        _renderer.FirstInit(firstHook, firstObject, _shootPoint, _widthValue, _material);        
    }      
}