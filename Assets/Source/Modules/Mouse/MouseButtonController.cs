using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseButtonController : MonoBehaviour
{
    public event UnityAction ButtonPressed;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ButtonPressed?.Invoke();
        }
    }
}