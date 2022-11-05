using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    GameObject bottleRoyal;
    Status.Type wallStatus;
    SpriteRenderer wallSprite;

    private void Start()
    {
        wallSprite = GetComponent<SpriteRenderer>();
        wallStatus = Status.Type.empty;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bottle"))
        {
            bottleRoyal = collision.gameObject;
            if (wallStatus == Status.Type.empty)
            {
                ChangeWallStatus(bottleRoyal.GetComponent<Bottle>().GetBottleType());
            }
            else
            {
                Destroy(this.gameObject); // or something different
            }
        }
    }

    void ChangeWallStatus(Status.Type newStatus)
    {
        wallStatus = newStatus;
        if(wallStatus == Status.Type.blue)
        {
            wallSprite.color = Color.blue;
        }
        if (wallStatus == Status.Type.red)
        {
            wallSprite.color = Color.red;
        }
        if (wallStatus == Status.Type.yellow)
        {
            wallSprite.color = Color.yellow;
        }
    }
}