using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManagerV2 : MonoBehaviour
{
    [SerializeField] BattleCharacter[] players, enemies;
    List<BattleCharacter> activePlayers = new List<BattleCharacter>();
    List<BattleCharacter> activeEnemies = new List<BattleCharacter>();
    // Start is called before the first frame update
    void Start()
    {
        AddPlayers();
        AddEnemies();
        StartCoroutine(AttackContinuously());
    }

    // Update is called once per frame
    void Update()
    {
        CheckBattleStatus();

        // Use special attack
        if (Input.GetKeyDown(KeyCode.Q))
        {
            int damage = activePlayers[0].specialAtkDamage;
            activeEnemies[0].TakeDamage(damage);
        }
    }

    private void AddPlayers()  
    {
        for(int i = 0; i < players.Length; i++)
        {
            BattleCharacter instance = Instantiate(players[i], new Vector2(-2*i-2,0), Quaternion.identity);
            activePlayers.Add(instance);
        }
    }
    private void AddEnemies()
    {
        for(int i =0; i < enemies.Length; i++)
        {
            BattleCharacter instance = Instantiate(enemies[i], new Vector2(2*i+2, 0), Quaternion.identity);
            activeEnemies.Add(instance);
        }
    }

    // Method is now hardcoded for player 1 to hit enemy 1
    IEnumerator AttackContinuously()
    {
        int damage = activePlayers[0].basicAtkDamage;
        float attackSpeed = activePlayers[0].basicAtkSpeed;
        while (true)
        {
            activeEnemies[0].TakeDamage(damage);
            Animation basicAtkEffectAnimation;
            GameObject obj = Instantiate(activePlayers[0].basicAtkEffect, activeEnemies[0].transform.position, Quaternion.identity) as GameObject;
            basicAtkEffectAnimation = obj.GetComponent<Animation>();
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
            }
        }
        for(int i = 0; i < activeEnemies.Count; i++)
        {
            if(activeEnemies[i].isDead)
            {
                activeEnemies.Remove(activeEnemies[i]);
            }
        }
        if(activePlayers.Count.Equals(0))
        {
            Debug.Log("LOST");
        }
        if (activeEnemies.Count.Equals(0))
        {
            Debug.Log("WON");
        }
    }

}
