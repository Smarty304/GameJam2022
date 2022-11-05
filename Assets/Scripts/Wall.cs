using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    GameObject bottleRoyal;
    Chemical.Type wallStatus;
    SpriteRenderer wallSprite;

    private void Start()
    {
        wallSprite = GetComponent<SpriteRenderer>();
        wallStatus = Chemical.Type.empty;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bottle"))
        {
            bottleRoyal = collision.gameObject;
            if (wallStatus == Chemical.Type.empty)
            {
                ChangeWallStatus(bottleRoyal.GetComponent<Bottle>().GetBottleType());
                Destroy(collision.gameObject);
            }
            else
            {
                Destroy(this.gameObject); // or something different
            }
        }
    }

    void ChangeWallStatus(Chemical.Type newStatus)
    {
        wallStatus = newStatus;
        if (wallStatus == Chemical.Type.blue)
        {
            wallSprite.color = Color.blue;
        }

        if (wallStatus == Chemical.Type.red)
        {
            wallSprite.color = Color.red;
        }

        if (wallStatus == Chemical.Type.yellow)
        {
            wallSprite.color = Color.yellow;
        }
    }
}