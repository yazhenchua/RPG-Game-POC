using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleCharacter : MonoBehaviour
{
    [SerializeField] int maxHP;
    [SerializeField] public int basicAtkDamage;
    [SerializeField] public float basicAtkSpeed;
    [SerializeField] public int specialAtkDamage;
    //[SerializeField] public GameObject basicAtkEffect;
    //[SerializeField] public GameObject specialAtkEffect;
    [SerializeField] public string basicAtkAnim;
    [SerializeField] private string specialAtkAnim;

    public bool isDead;
    public Animator animator;
    public int currentHP;
    public HealthBar healthBar;
    public GameObject damageText;

    public void Start()
    {
        animator = GetComponent<Animator>();
        currentHP = maxHP;
        healthBar.SetMaxHP(maxHP);
    }

    // Take damage at the end of enemy's attack animation
    public IEnumerator TakeDamage(int damage, float delay)
    {
        yield return new WaitForSeconds(delay);
        
        currentHP -= damage;
        healthBar.SetHP(currentHP);
        DamageIndicator damageIndicator = Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamageIndicator>();
        damageIndicator.SetText(damage);
        if(currentHP <= 0)
        {
            isDead = true;
            Destroy(gameObject);
        }
    }

    public IEnumerator AttackContinuously(BattleCharacter target)
    {
        while (true && !isDead)
        {
            animator.Play(basicAtkAnim);
            StartCoroutine(target.TakeDamage(basicAtkDamage, animator.GetCurrentAnimatorStateInfo(0).length));
            yield return new WaitForSeconds(basicAtkSpeed);
        }
    }

    public IEnumerator SpecialAttack(BattleCharacter target, float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.Play(specialAtkAnim);
        StartCoroutine(target.TakeDamage(specialAtkDamage, animator.GetCurrentAnimatorStateInfo(0).length));
    }
}
