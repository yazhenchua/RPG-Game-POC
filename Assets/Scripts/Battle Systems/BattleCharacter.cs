using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacter : MonoBehaviour
{
    [SerializeField] int maxHP = 50;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] public int basicAtkDamage = 5;
    [SerializeField] public float basicAtkSpeed = 0.5f;
    [SerializeField] public int specialAtkDamage = 20;
    [SerializeField] public GameObject basicAtkEffect;
    [SerializeField] public GameObject specialAtkEffect;

    private Shooter shooter;
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

    public void TakeDamage(int damage)
    {
        maxHP -= damage;
        if(maxHP <= 0)
        {
            Destroy(gameObject);
            isDead = true;
        }
    }

    public void PlayerHitEffect()
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
