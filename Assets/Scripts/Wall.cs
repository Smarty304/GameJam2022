using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("ChemicalReaction"))
        {
            var reactionType = other.GetComponent<ChemicalSerum>().ReactionType;

            if (reactionType == ChemicalSerum.ChemicalReactionType.explosion)
            {
                Destroy(other.gameObject);
                Destroy(this.gameObject);    
            }
        }
    }
}