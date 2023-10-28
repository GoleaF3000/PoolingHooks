using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CargoTrigger : MonoBehaviour
{
    public event UnityAction EndRoadReached;

    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent<Road>(out Road road))
        {
            EndRoadReached?.Invoke();           
        }
    }
}