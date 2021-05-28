using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] Text moneyText;
    [SerializeField] Text waveText;
    [SerializeField] Text escapedText;
    public int escapedNumber = 0;
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject[] enemies;
    [SerializeField] int totalEnemies;
    [SerializeField] int maxEnemiesOnScreen;
    public int currentEnemiesOnScreen = 0;
    [SerializeField] float waveNumber = 1;
    [SerializeField] float spawnWait;
    [SerializeField] float waveWait;
    [SerializeField] int startMoney;
    public int maxMoney;
    public int currentMoney;

    public static List<GameObject> currentEnemies = new List<GameObject>();

    float spawnWaitPassed = 0;
    float waveWaitPassed = 0;
    bool isWaveFinished = false;
    int spawnedEnemiesCount = 0;
    int waveEnemiesCount = 0;
    int enemiesTopSortingLayer = 0;

	private void Start()
	{
        currentMoney = startMoney;

    }

	void Update()
    {
        SpawnEnemies();
        SetNextWave();
        AdjustMoney();
        moneyText.text = currentMoney.ToString();
        waveText.text = "Wave " + waveNumber.ToString();
        escapedText.text = "Escaped " + escapedNumber.ToString() + "/" + totalEnemies;
    }

    void SpawnEnemies()
	{
        if(currentEnemiesOnScreen < totalEnemies && !isWaveFinished)
		{
            spawnWaitPassed += Time.deltaTime;
            if (spawnWaitPassed >= spawnWait)
            {
                spawnWaitPassed = 0;
                if (currentEnemiesOnScreen < maxEnemiesOnScreen && spawnedEnemiesCount < totalEnemies)
                {
                    GameObject newEnemy = Instantiate(enemies[0], spawnPoint.position, Quaternion.identity);
                    newEnemy.GetComponent<SpriteRenderer>().sortingOrder = enemiesTopSortingLayer;
                    enemiesTopSortingLayer++;
                    currentEnemies.Add(newEnemy);
                    currentEnemiesOnScreen++;
                    spawnedEnemiesCount++;
                    waveEnemiesCount++;
                }
            }
		}
	}

    void SetNextWave()
	{
        if (waveEnemiesCount == maxEnemiesOnScreen)
        {
            isWaveFinished = true;
        }

		if (isWaveFinished && currentEnemiesOnScreen == 0)
		{
            waveWaitPassed += Time.deltaTime;
            if (waveWaitPassed >= waveWait - spawnWait)
			{
                waveEnemiesCount = 0;
                waveNumber++;
                waveWaitPassed = 0;
                isWaveFinished = false;
            }
		}
    }

    public void DecrementEnemies(GameObject enemyObject)
	{
        if(currentEnemiesOnScreen > 0)
		{
            currentEnemies.Remove(enemyObject);
            currentEnemiesOnScreen--;
		}
	}

    void AdjustMoney()
	{
        if(currentMoney > maxMoney)
		{
            currentMoney = maxMoney;
		}
	}
}
