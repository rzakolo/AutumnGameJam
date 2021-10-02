using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    public Transform[] spawnPositions;
    public Wave[] waves;
    public int currentDay = 1;
    public bool canSpawn = false;

    public List<EnemyBase> enemyList = new List<EnemyBase>();
    private bool updateValues = true;
    private float spawnInterval;
    private int enemyType = 0;
    private int currentIndex = 0;
    private int enemyCounter = 0;
    private int enemyCounterGoal;

    private void Start()
    {
        InvokeRepeating(nameof(CheckEnemys), 5, 1);
    }

    private void Update()
    {
        if (updateValues && currentDay - 1 < waves.Length)
        {
            enemyCounterGoal = 0;
            foreach (var item in waves[currentDay - 1].enemyTypes)
            {
                enemyCounterGoal += item.numberOfEnemy;
            }
            spawnInterval = waves[currentDay - 1].spawnInterval;
            updateValues = false;
        }
        if (canSpawn)
        {
            //TODO: передать ui название волны. Например: Ночь 1 "Атака зомби"???
            gameManager.DayInformer(waves[currentDay - 1].waveName);
            StartSpawn();
        }
        if (currentDay >= 12)
        {
            gameManager.WinGame();
        }
    }
    public void StartSpawn()
    {
        if (spawnInterval < 0)
        {
            if (currentDay - 1 < waves.Length
                && currentIndex < waves[currentDay - 1].enemyTypes[enemyType].numberOfEnemy
                && enemyType < waves[currentDay - 1].enemyTypes.Length)
            {
                Spawn(waves[currentDay - 1].enemyTypes[enemyType].enemyType);
                spawnInterval = waves[currentDay - 1].spawnInterval;
                enemyCounter++;
                currentIndex++;
            }
            else
            {
                enemyType++;
                currentIndex = 0;
            }

        }
        spawnInterval -= Time.deltaTime;
        if (enemyCounterGoal == enemyCounter)
        {
            canSpawn = false;
            updateValues = true;
            enemyCounter = 0;
            enemyType = 0;
            currentIndex = 0;
            currentDay++;
        }
    }

    private void Spawn(EnemyBase enemyToSpawn)
    {
        int index = Random.Range(0, spawnPositions.Length);
        enemyList.Add(Instantiate(enemyToSpawn, spawnPositions[index]));
    }

    private void CheckEnemys()
    {
        if (!canSpawn)
            if (enemyList.Count > 0)
                for (int i = 0; i < enemyList.Count; i++)
                {
                    if (enemyList[i] == null)
                        enemyList.RemoveAt(i);
                    if (enemyList.Count == 0)
                        gameManager.allEnemyDead = true;
                }
    }
}


[System.Serializable]
public class Wave
{
    public string waveName;
    public EnemyTypes[] enemyTypes;
    public float spawnInterval;
}
[System.Serializable]
public class EnemyTypes
{
    public int numberOfEnemy;
    public EnemyBase enemyType;
}
