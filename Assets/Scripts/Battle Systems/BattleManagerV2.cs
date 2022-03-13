using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManagerV2 : MonoBehaviour
{
    [SerializeField] BattleCharacter[] players, enemies;
    [SerializeField] List<BattleCharacter> activePlayers = new List<BattleCharacter>();
    [SerializeField] List<BattleCharacter> activeEnemies = new List<BattleCharacter>();

    List<Vector2> playerPositions = new List<Vector2>() { new Vector2(-2, 0), new Vector2(-4, 2), new Vector2(-5, -2) };
    List<Vector2> enemyPositions = new List<Vector2>() { new Vector2(2, 0), new Vector2(4, 2), new Vector2(5, -2) };

    bool playerCouroutineStarted = false;
    bool enemyCouroutineStarted = false;
    bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        AddPlayers();
        AddEnemies();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameOver) {
            CheckBattleStatus();
        }

        foreach (var player in activePlayers)
        {
            if (!playerCouroutineStarted && activeEnemies.Count != 0)
            {
                StartCoroutine(AttackContinuously(player, activeEnemies[0]));
                playerCouroutineStarted = true;
            }
        }
        foreach (var enemy in activeEnemies)
        {
            if (!enemyCouroutineStarted && activePlayers.Count != 0)
            {
                StartCoroutine(AttackContinuously(enemy, activePlayers[0]));
                enemyCouroutineStarted = true;
            }
        }

        // Use special attack on enemy 1
        if (Input.GetKeyDown(KeyCode.Q))
        {
            int damage = activePlayers[0].specialAtkDamage;
            activeEnemies[0].TakeDamage(damage);
            Animation specialAtkEffectAnimation;
            GameObject obj = Instantiate(activePlayers[0].specialAtkEffect, activeEnemies[0].transform.position, Quaternion.identity) as GameObject;
            specialAtkEffectAnimation = obj.GetComponent<Animation>();
            Destroy(obj, obj.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        }

        // Use special attack on enemy 2
        if (Input.GetKeyDown(KeyCode.W))
        {
            int damage = activePlayers[0].specialAtkDamage;
            activeEnemies[1].TakeDamage(damage);
            Animation specialAtkEffectAnimation;
            GameObject obj = Instantiate(activePlayers[0].specialAtkEffect, activeEnemies[1].transform.position, Quaternion.identity) as GameObject;
            specialAtkEffectAnimation = obj.GetComponent<Animation>();
            Destroy(obj, obj.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        }

        // Use special attack on enemy 3
        if (Input.GetKeyDown(KeyCode.E))
        {
            int damage = activePlayers[0].specialAtkDamage;
            activeEnemies[2].TakeDamage(damage);
            Animation specialAtkEffectAnimation;
            GameObject obj = Instantiate(activePlayers[0].specialAtkEffect, activeEnemies[2].transform.position, Quaternion.identity) as GameObject;
            specialAtkEffectAnimation = obj.GetComponent<Animation>();
            Destroy(obj, obj.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        }
    }

    private void AddPlayers()  
    {
        for(int i = 0; i < players.Length; i++)
        {
            BattleCharacter instance = Instantiate(players[i], playerPositions[i], Quaternion.identity);
            activePlayers.Add(instance);
        }
    }
    private void AddEnemies()
    {
        for(int i =0; i < enemies.Length; i++)
        {
            BattleCharacter instance = Instantiate(enemies[i], enemyPositions[i], Quaternion.identity);
            activeEnemies.Add(instance);
        }
    }

    IEnumerator AttackContinuously(BattleCharacter attacker, BattleCharacter target)
    {
        int damage = attacker.basicAtkDamage;
        float attackSpeed = attacker.basicAtkSpeed;
        while (true && !attacker.isDead)
        {
            target.TakeDamage(damage);
            Animation basicAtkEffectAnimation;
            GameObject obj = Instantiate(attacker.basicAtkEffect, target.transform.position, Quaternion.identity) as GameObject;
            basicAtkEffectAnimation = obj.GetComponent<Animation>();
            Destroy(obj, obj.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            yield return new WaitForSeconds(attackSpeed);
        }
    }


    private void CheckBattleStatus()
    {
        for(int i = 0; i < activePlayers.Count; i++)
        {
            if(activePlayers[i].isDead)
            {
                activePlayers.Remove(activePlayers[i]);
                enemyCouroutineStarted = false;
                playerCouroutineStarted = false;
            }
        }
        for(int i = 0; i < activeEnemies.Count; i++)
        {
            if(activeEnemies[i].isDead)
            {
                activeEnemies.Remove(activeEnemies[i]);
                enemyCouroutineStarted = false;
                playerCouroutineStarted = false;
            }
        }
        if(activePlayers.Count.Equals(0))
        {
            Debug.Log("LOST");
            gameOver = true;
        }
        if (activeEnemies.Count.Equals(0))
        {
            Debug.Log("WON");
            gameOver = true;
        }
    }

}
