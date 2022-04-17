using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacter : MonoBehaviour
{
    [SerializeField] int maxHP;
    [SerializeField] public int basicAtkDamage;
    [SerializeField] public float basicAtkSpeed;
    [SerializeField] public int specialAtkDamage;
    //[SerializeField] public GameObject basicAtkEffect;
    //[SerializeField] public GameObject specialAtkEffect;
    [SerializeField] private string basicAtkAnim;

    public bool isDead;
    public Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Take damage at the end of enemy's attack animation
    public IEnumerator TakeDamage(int damage, float delay)
    {
        yield return new WaitForSeconds(delay);
        maxHP -= damage;
        if(maxHP <= 0)
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
}
