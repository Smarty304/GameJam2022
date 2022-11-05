using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * An npc is a non-player-character, which can move around the world and interact with the player.
 * It has some basic characteristic stats.
 */
public class Enemy : MonoBehaviour
{
    public float horizontalMoveSpeed = 200;
    public float minForce = 2, maxForce = 3;
    public bool hasGravity;
    public int hp;
    public int damage;
    public GameObject player;

    protected Chemical.Type CurrentType;
    
    // Start is called before the first frame update
    void Start()
    {
        CurrentType = Chemical.Type.empty;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasGravity)
        {
            if (GetComponent<Rigidbody2D>().velocity.y < 0)
            {
                GetComponent<Rigidbody2D>().gravityScale = PlayerController.FALLING_GRAVITY_SCALE;
            }
            else
            {
                GetComponent<Rigidbody2D>().gravityScale = PlayerController.GRAVITY_SCALE;
            }
        }
    }

    /**
     * Moves the npc horizontally by simply adding force
     * Use direction to move left or right.
     * Left : -1
     * Right : 1
     */
    protected void MoveHorizontal(int direction)
    {
        var rb = GetComponent<Rigidbody2D>();
        var force = new Vector2(horizontalMoveSpeed * Time.fixedDeltaTime * direction, 0);

        rb.AddForce(force, ForceMode2D.Force);

        if (direction < 0)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = true;

            if (rb.velocity.x < -maxForce)
            {
                rb.velocity = new Vector2(-maxForce, rb.velocity.y);
            }

            if (rb.velocity.x < -minForce)
            {
                rb.velocity = new Vector2(-minForce, rb.velocity.y);
            }
        }
        else if (direction > 0)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = false;

            if (rb.velocity.x > maxForce)
            {
                rb.velocity = new Vector2(maxForce, rb.velocity.y);
            }

            if (rb.velocity.x < minForce)
            {
                rb.velocity = new Vector2(minForce, rb.velocity.y);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void InflictDamage(int dmg)
    {
        hp -= dmg;

        if (hp <= 0)
        {
            Die();
        }
    }

    public virtual void AddChemical(Chemical.Type type)
    {
        throw new NotImplementedException();
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}