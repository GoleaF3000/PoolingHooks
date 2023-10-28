using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HarpoonShooter))]
[RequireComponent(typeof(HarpoonReloader))]
[RequireComponent(typeof(SpringCreator))]
public class HarpoonManager : MonoBehaviour
{
    [SerializeField] private HooksManager _hooksManager;
    [SerializeField] private BungeeRenderCreator _bungeeCreator;   

    private HarpoonShooter _shooter;
    private HarpoonReloader _reloader;
    private SpringCreator _springCreator;    

    private void Awake()
    {
        _shooter = GetComponent<HarpoonShooter>();
        _reloader = GetComponent<HarpoonReloader>();
        _springCreator = GetComponent<SpringCreator>();
    }

    private void OnEnable()
    {
        _hooksManager.Restarted += Restart;
    }

    private void OnDisable()
    {
        _hooksManager.Restarted -= Restart;
    }

    private void Restart()
    {
        _bungeeCreator.Restart();
        _springCreator.Restart();
        _shooter.Restart();
        _reloader.Restart();  
    }
}