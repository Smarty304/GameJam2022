using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalSerum : MonoBehaviour
{
    public Chemical.Type Type { get; set; }
    public ChemicalReactionType ReactionType { get; set; }
    public GameObject ExplosionParticle;

    public enum ChemicalReactionType
    {
        nothing,
        explosion,
        freeze
    }

    private void Start()
    {
        GetComponentInChildren<Light>().color = Chemical.GetColor(Type);
        Destroy(this.gameObject, 10);
    }

    public void OnCollisionWithBottle(Bottle bottle)
    {
        this.tag = "ChemicalReaction"; // change tag
        ReactionType = ChemicalReactionType.nothing;
        //var comp = gameObject.AddComponent<Rigidbody2D>();
        //comp.bodyType = RigidbodyType2D.Kinematic;

        // Disable / enable to trigger collision
        // GetComponent<Collider2D>().enabled = false;
        // GetComponent<Collider2D>().enabled = true;

        ReactionType = CreateReaction(Type, bottle.GetComponent<Bottle>().GetBottleType());
        
        if (ReactionType == ChemicalReactionType.explosion)
        {
            var particles = Instantiate(ExplosionParticle, transform.position, Quaternion.identity);
            foreach (var particle in particles.GetComponentsInChildren<ParticleSystem>())
            {
                Bottle._player.PlayExplosionClip();
                particle.Play();
            }
        }
        
        Destroy(this.gameObject, 1);
    }

    public static ChemicalReactionType CreateReaction(Chemical.Type t1, Chemical.Type t2)
    {
        if ((t1 == Chemical.Type.red && t2 == Chemical.Type.yellow) ||
            (t1 == Chemical.Type.yellow && t2 == Chemical.Type.red))
        {
            return ChemicalReactionType.explosion;
        }

        if (t1 == Chemical.Type.blue && t2 == Chemical.Type.white ||
            t1 == Chemical.Type.white && t2 == Chemical.Type.blue)
        {
            return ChemicalReactionType.freeze;
        }
        
        return ChemicalReactionType.nothing;
    }
}