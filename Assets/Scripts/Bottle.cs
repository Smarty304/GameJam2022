using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Bottle Script
 */
public class Bottle : MonoBehaviour
{
    [SerializeField] private Chemical.Type _bottleType;

    [SerializeField] private GameObject _chemicalSerum;
    private bool _pickedUp; // if the bottle was picked up by the player or still lays on the ground
    private static AudioPlayer _player;

    private void Awake()
    {
        _player = FindObjectOfType<AudioPlayer>();
    }

    void Start()
    {
        _pickedUp = false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!_pickedUp) // Bottle was thrown by the player
        {
            if (other.transform.CompareTag("Enemy"))
            {
                // Bottle hit an enemy
                _player.PlayBottleBreakClip();
                other.transform.GetComponent<Enemy>().AddChemical(_bottleType);
                Destroy(this.gameObject);
            }

            if (other.transform.CompareTag("SolidObject") || other.transform.CompareTag("Wall"))
            {
                _player.PlayBottleBreakClip();
                Collider2D[] colliders = new Collider2D[10];
                for (int i = 0;
                    i < Physics2D.OverlapCollider(GetComponent<Collider2D>(), new ContactFilter2D().NoFilter(),
                        colliders);
                    i++)
                {
                    if (colliders[i].CompareTag("ChemicalSerum"))
                    {
                        colliders[i].GetComponent<ChemicalSerum>().OnCollisionWithBottle(this);
                        Destroy(this.gameObject);
                        return;
                    }
                }

                // Create serum
                var serum = Instantiate(_chemicalSerum, transform.position, Quaternion.identity);
                serum.GetComponent<ChemicalSerum>().Type = _bottleType;
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        /*   if (other.CompareTag("ChemicalSerum"))
           {
               other.GetComponent<ChemicalSerum>().OnCollisionWithBottle(this);
           } */
    }

    /**
     * Needs to called when the bottle is thrown
     */
    void OnThrow()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
    }

    public Chemical.Type GetBottleType()
    {
        return _bottleType;
    }

    /**
     * When the player collides with the bottle trigger
     */
    public void OnPickup()
    {
        // Collider and Renderer get disabled on pickup but the item GameObject is not removed
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        _pickedUp = true;
    }
}
