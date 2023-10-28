using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HookRotator))]
[RequireComponent(typeof(HookTrigger))]
public class Hook : MonoBehaviour 
{   
    private HooksManager _manager;
    private HookTrigger _trigger;

    public HooksManager Manager => _manager;

    private void Awake()
    {
        _trigger = GetComponent<HookTrigger>();
    }

    public void Init(HooksManager manager)
    {
        _manager = manager;
        _trigger.enabled = true;        
    }
}