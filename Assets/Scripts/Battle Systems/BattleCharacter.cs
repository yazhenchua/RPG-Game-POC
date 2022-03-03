using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacter : MonoBehaviour
{
    [SerializeField] int health = 50;
    private Shooter shooter;
    [SerializeField] ParticleSystem hitEffect;
    public bool isDead;

    private void Awake()
    {
        shooter = GetComponent<Shooter>();
    }

    private void Start()
    {
        if (shooter != null)
        {
            shooter.isFiring = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();

        if(damageDealer != null)
        {
            TakeDamage(damageDealer.getDamage());
            PlayerHitEffect();
            damageDealer.Hit();
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void PlayerHitEffect()
    {
        // Hit effect should be based on opponent's attack
        // Currently based on own attack
        if(hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }
}
