﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacters : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] string[] attacksAvailable;
    [SerializeField] public Animator charactersAnimator;

    public string characterName;
    public int currentHP, maxHP, currentMana, maxMana, dexterity, defence, wpnPower, armorDefence;
    public bool isDead;

    public ParticleSystem deathParticles;
    private Animator anim;

    Shooter shooter;
    private void Awake()
    {
        shooter = GetComponent<Shooter>();
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Console.WriteLine(anim);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            FadeOut();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            anim.SetBool("Attack1", true);
            anim.SetBool("Idling", false);
            
            if(shooter != null)
            {
                shooter.isFiring = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetBool("Attack1", false);
            anim.SetBool("Idling", true);
            if (shooter != null)
            {
                shooter.isFiring = false;
            }
        }
    }

    public void FadeOut()
    {
        GetComponent<SpriteRenderer>().color = new Color(
            Mathf.MoveTowards(GetComponent<SpriteRenderer>().color.r, 1f, 0.6f * Time.deltaTime),
            Mathf.MoveTowards(GetComponent<SpriteRenderer>().color.g, 0f, 0.6f * Time.deltaTime),
            Mathf.MoveTowards(GetComponent<SpriteRenderer>().color.b, 0f, 0.6f * Time.deltaTime),
            Mathf.MoveTowards(GetComponent<SpriteRenderer>().color.a, 0f, 0.6f * Time.deltaTime)
            );

        if(GetComponent<SpriteRenderer>().color.a == 0)
        {
            gameObject.SetActive(false);
        }

    }

    public bool IsPlayer()
    {
        return this.isPlayer;
    }

    public string[] AttackMovesAvailable()
    {
        return attacksAvailable;
    }

    public void TakeHPDamage(int damageToReceive)
    {
        currentHP -= damageToReceive;
        if(currentHP <= 0)
        {
            currentHP = 0;
        }
    }

    public void KillCharacter()
    {
        Instantiate(deathParticles, transform.position, transform.rotation);
        isDead = true;
    }

    public void IsAttacking()
    {
    }
}
