using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManagerV2 : MonoBehaviour
{
    [SerializeField] BattleCharacter[] players, enemies;
    [SerializeField] List<BattleCharacter> activePlayers = new List<BattleCharacter>();
    [SerializeField] List<BattleCharacter> activeEnemies = new List<BattleCharacter>();

    List<Vector2> playerPositions = new List<Vector2>() { new Vector2(-2, 1), new Vector2(-4, -1), new Vector2(-5, -1) };
    List<Vector2> enemyPositions = new List<Vector2>() { new Vector2(2, 1), new Vector2(4, -1), new Vector2(5, -1) };

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
            foreach (var player in activePlayers)
            {
                if (!playerCouroutineStarted && activeEnemies.Count != 0)
                {
                    StartCoroutine(player.AttackContinuously(activeEnemies[0]));
                    playerCouroutineStarted = true;
                }
            }
            foreach (var enemy in activeEnemies)
            {
                if (!enemyCouroutineStarted && activePlayers.Count != 0)
                {
                    StartCoroutine(enemy.AttackContinuously(activePlayers[0]));
                    enemyCouroutineStarted = true;
                }
            }
        }

        // Use special attack on enemy 1
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(!activePlayers[0].animator.GetCurrentAnimatorStateInfo(0).IsName(activePlayers[0].basicAtkAnim))
            {
                StopCoroutine(activePlayers[0].AttackContinuously(activeEnemies[0]));
                StartCoroutine(activePlayers[0].SpecialAttack(activeEnemies[0], 0));
            } else {
                // Wait for attack to complete before using special attack
                StopCoroutine(activePlayers[0].AttackContinuously(activeEnemies[0]));
                StartCoroutine(activePlayers[0].SpecialAttack(activeEnemies[0], activePlayers[0].animator.GetCurrentAnimatorStateInfo(0).length));
            }
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
            StopAllCoroutines();
        }
        if (activeEnemies.Count.Equals(0))
        {
            Debug.Log("WON");
            gameOver = true;
            StopAllCoroutines();
        }
    }

}
