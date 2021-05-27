using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject[] enemies;
    [SerializeField] int maxEnemiesOnScreen;
    [SerializeField] int totalEnemies;
    [SerializeField] int enemiesPerSpawn;
    [SerializeField] float spawnWait;
    [SerializeField] float waveWait;

    public static List<GameObject> currentEnemies = new List<GameObject>();

    int currentEnemiesOnScreen = 0;
    float spawnWaitPassed = 0;
    float waveWaitPassed = 0;
    bool isWaveFinished = false;
    int spawnedEnemiesCount = 0;

    void Update()
    {
        SpawnEnemies();
        SetNextWave();
    }

    void SpawnEnemies()
	{
        if(enemiesPerSpawn > 0 && currentEnemiesOnScreen < totalEnemies && !isWaveFinished)
		{
            spawnWaitPassed += Time.deltaTime;
            if (spawnWaitPassed >= spawnWait)
            {
                spawnWaitPassed = 0;
                if (currentEnemiesOnScreen < maxEnemiesOnScreen && spawnedEnemiesCount < totalEnemies)
                {
                    GameObject newEnemy = Instantiate(enemies[0], spawnPoint.position, Quaternion.identity);
                    currentEnemies.Add(newEnemy);
                    currentEnemiesOnScreen++;
                    spawnedEnemiesCount++;
                }
            }
		}
	}

    void SetNextWave()
	{
        if (currentEnemiesOnScreen == maxEnemiesOnScreen)
        {
            isWaveFinished = true;
        }

		if (isWaveFinished && currentEnemiesOnScreen == 0)
		{
            waveWaitPassed += Time.deltaTime;
            if (waveWaitPassed >= waveWait - spawnWait)
			{
                waveWaitPassed = 0;
                isWaveFinished = false;
            }
		}
    }

    public void DecrementEnemies()
	{
        if(currentEnemiesOnScreen > 0)
		{
            currentEnemies.RemoveAt(0);
            currentEnemiesOnScreen--;
		}
	}
}
