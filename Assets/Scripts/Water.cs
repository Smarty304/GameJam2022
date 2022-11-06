using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private Chemical.Type _currentType;
    private bool _freezed;

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
                // TODO Player reset
                Debug.Log("Player drowns");
            }        
        }
        
    }

    private void Freeze()
    {
        _freezed = true;
        GetComponent<SpriteRenderer>().color = Color.cyan;
    }
    
}