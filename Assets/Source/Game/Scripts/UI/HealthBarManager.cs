using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Heart[] _heartsFilled;
    [SerializeField] private Heart[] _heartsEmpty;

    private void Start()
    {
        Restart();
    }

    private void OnEnable()
    {
        _player.TakedDamage += LosingOneHeart;
        _player.Restarted += Restart;
    }

    private void OnDisable()
    {
        _player.TakedDamage -= LosingOneHeart;
        _player.Restarted -= Restart;
    }

    private void LosingOneHeart()
    {
        if (_heartsFilled.Length == _heartsEmpty.Length)
        {
            for (int i = _heartsFilled.Length - 1; i >= 0; i--)
            {
                if (_heartsFilled[i].Picture.enabled == true)
                {
                    _heartsEmpty[i].Picture.enabled = true;
                    _heartsFilled[i].Picture.enabled = false;

                    break;
                }
            }
        }        
    }

    private void Restart()
    {
        if (_heartsFilled.Length == _heartsEmpty.Length)
        {
            for (int i = 0; i < _heartsFilled.Length; i++)
            {
                _heartsFilled[i].Picture.enabled = true;
                _heartsEmpty[i].Picture.enabled = false;
            }
        }
    }
}