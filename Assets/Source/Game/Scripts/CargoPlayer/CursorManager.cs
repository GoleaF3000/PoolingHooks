using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D _mainTexture;
    [SerializeField] private Vector2 _hotstopMainTexture;
    [SerializeField] private Texture2D _inactiveTexture;
    [SerializeField] private Vector2 _hotstopInactiveTexture;

    private Camera _camera;

    void Start()
    {
        _camera = Camera.main;
        Cursor.SetCursor(_mainTexture, _hotstopMainTexture, CursorMode.ForceSoftware);
    }

    private void FixedUpdate()
    {
        if (TryRaycastShootArea(out bool isHook) == true && isHook == false)
        {
            Cursor.SetCursor(_inactiveTexture, _hotstopInactiveTexture, CursorMode.ForceSoftware);
        }
        else if (TryRaycastShootArea(out isHook) == false && isHook == false)
        {
            Cursor.SetCursor(_mainTexture, _hotstopMainTexture, CursorMode.ForceSoftware);
        }        
    }

    private bool TryRaycastShootArea(out bool isHook)
    {
        bool result = false;
        isHook = false;

        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent<ShootArea>(out ShootArea shootArea))
            {                
                result = true;                  
            }
            else if (hit.collider.TryGetComponent<HookTrigger>(out HookTrigger hook))
            {
                isHook = true;
            }
        }
        
        return result;
    }
}