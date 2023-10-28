using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Heart : MonoBehaviour
{
    private Image _picture;

    public Image Picture => _picture;

    private void Start()
    {
        _picture = GetComponent<Image>();
    }
}