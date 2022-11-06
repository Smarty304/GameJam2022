using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private Chemical.Type _currentType;
    private bool _freezed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ChemicalSerum"))
        {
            _currentType = other.GetComponent<ChemicalSerum>().Type;
            Debug.Log(_currentType);
        }
    }
}
