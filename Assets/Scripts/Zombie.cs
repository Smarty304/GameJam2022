using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    public float aggroRange = 2;
    private bool _playerInRange; // if player is in aggro range
    public bool wanders;

    public Transform p1, p2;
    private Transform _currentPoint; // the point zombie is walking to

    private void Start()
    {
        _currentPoint = p1;
    }
    
    private void FixedUpdate()
    {
        var playerPos = base.player.transform.position;
        var zombiePos = this.transform.position;
        var distance = Vector2.Distance(playerPos, zombiePos);

        if (wanders)
        {
            float pointDistance = Vector2.Distance(_currentPoint.position, transform.position);

            if (pointDistance <= 2)
            {
                ChangePoint();
            }
            
            Vector2 direction = transform.position + _currentPoint.position;
            direction.Normalize();
            base.MoveHorizontal((int)direction.x);
            return;
        }
        
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

    public void ChangePoint()
    {
        if (_currentPoint.Equals(p1))
        {
            _currentPoint = p2;
        }
        else
        {
            _currentPoint = p1;
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