using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacter : MonoBehaviour
{
    [SerializeField] int maxHP = 50;
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
            damageDealer.Hit();
        }
    }

    public void TakeDamage(int damage)
    {
        maxHP -= damage;
        if(maxHP <= 0)
        {
            isDead = true;
            Destroy(gameObject);
        }
    }
}
