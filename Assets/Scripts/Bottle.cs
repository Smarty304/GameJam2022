using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Bottle Script
 */
public class Bottle : MonoBehaviour
{
    [SerializeField]
    private Chemical.Type _bottleType;
        
    private bool _pickedUp; // if the bottle was picked up by the player or still lays on the ground
    
    void Start()
    {
        _pickedUp = false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        #region BottleOnGround

        if (!_pickedUp) // Bottle has not been collected by the player yet
        {
            if (other.transform.CompareTag("Player"))
            {
                // Player needs to collect bottle
                // TODO other.GetComponent<PlayerController>().Collect(this);
            }
        }

        #endregion

        #region BottleThrown

        else // Bottle was thrown by the player
        {
            if (other.transform.CompareTag("Wall"))
            {
                // Bottle hit another wall
                // TODO Add impact on wall
                Destroy(this.gameObject);
            }

            if (other.transform.CompareTag("Enemy"))
            {
                // Bottle hit an enemy
                // TODO Add impact on enemy
                Destroy(this.gameObject);
            }
        }

        #endregion
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