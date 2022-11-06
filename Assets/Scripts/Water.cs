using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private Chemical.Type _currentType;
    private bool _freezed;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Bottle"))
        {
            if (_currentType == Chemical.Type.empty)
            {
                _currentType = other.transform.GetComponent<Bottle>().GetBottleType();
            }
            else
            {
                var reaction = ChemicalSerum.CreateReaction(_currentType,
                    other.transform.GetComponent<Bottle>().GetBottleType());

                if (reaction == ChemicalSerum.ChemicalReactionType.freeze)
                {
                    Freeze();
                }

                _currentType = Chemical.Type.empty;
            }
        }

        if (other.transform.CompareTag("Player"))
        {
            if (!_freezed)
            {
                other.transform.GetComponent<PlayerController>().ResetPlayer();
            }        
        }
        
    }

    private void Freeze()
    {
        _freezed = true;
        _animator.SetTrigger("Ice");
        Bottle._player.PlayFreezeClip();
    }
    
}