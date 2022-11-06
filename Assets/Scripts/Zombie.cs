using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    public float aggroRange = 2;
    private bool _playerInRange; // if player is in aggro range

    private void FixedUpdate()
    {
        var playerPos = base.player.transform.position;
        var zombiePos = this.transform.position;
        var distance = Vector2.Distance(playerPos, zombiePos);

        _playerInRange = Math.Abs(distance) <= aggroRange;

        if (!_playerInRange) // Zombie just stays on spot when player is not in range
        {
            base.MoveHorizontal(0);
            return;
        }

        if (playerPos.x < zombiePos.x) // Player is positioned left of the zombie
        {
            base.MoveHorizontal(-1);
        }
        else if (playerPos.x > zombiePos.x) // Player is positioned right of the zombie
        {
            base.MoveHorizontal(1);
        }
    }

    /**
     * When the enemy is hit directly by the bottle
     */
    public override void AddChemical(Chemical.Type type)
    {
        if (base.CurrentType == Chemical.Type.empty) // Zombie has not been hit by a bottle yet
        {
            base.CurrentType = type;
        }
        else
        {
            var reactionType = ChemicalSerum.CreateReaction(base.CurrentType, type);

            if (reactionType == ChemicalSerum.ChemicalReactionType.explosion)
            {
                base.InflictDamage(1);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("ChemicalReaction"))
        {
            // TODO Add reaction to chemical reaction
            if (other.GetComponent<ChemicalSerum>().ReactionType == ChemicalSerum.ChemicalReactionType.explosion)
            {
                base.InflictDamage(1);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.transform.GetComponent<PlayerController>().ResetPlayer();
        }
    }
}