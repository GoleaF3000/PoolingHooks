using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHit
{
    public GameObject Subject;
    public Vector3 HitPosition;

    public ObjectHit(GameObject subject, Vector3 hitPosition)
    {
        Subject = subject;
        HitPosition = hitPosition;
    }
}