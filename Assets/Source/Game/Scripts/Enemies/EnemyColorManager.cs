using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyDestroyer))]
public class EnemyColorManager : MonoBehaviour
{
    [SerializeField] private Renderer _baseCar;
    [SerializeField] private Renderer[] _otherDetails;
    [SerializeField] private Color[] _possibleColors;

    private MaterialPropertyBlock _materialBlock;
    private EnemyDestroyer _destroyer;
    private string _nameColorInShader = "_Color";    
    private float _explosionBrightness = 0.3f;    

    private void Awake()
    {
        _destroyer = GetComponent<EnemyDestroyer>();

        _materialBlock = new MaterialPropertyBlock();
        _materialBlock.SetColor(_nameColorInShader, 
            _possibleColors[Random.Range(0, _possibleColors.Length)]);

        _baseCar.SetPropertyBlock(_materialBlock);
    }

    private void OnEnable()
    {
        _destroyer.Exploded += Changing;
    }

    private void OnDisable()
    {
        _destroyer.Exploded -= Changing;
    }

    private void Changing()
    {
        _materialBlock.SetColor(_nameColorInShader, Color.HSVToRGB(0f, 0f, _explosionBrightness));
        
        _baseCar.SetPropertyBlock(_materialBlock);

        foreach (var detail in _otherDetails)
        {
            detail.SetPropertyBlock(_materialBlock);
        }
    }
}