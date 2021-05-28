using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] Button playButton;
    [SerializeField] float playButtonYStart;
    [SerializeField] float playButtonYMid;
    [SerializeField] Image defeatBanner;
    [SerializeField] Image victoryBanner;
    [SerializeField] Text moneyText;
    [SerializeField] Text waveText;
    [SerializeField] Text escapedText;
    public int escapedNumber = 0;
    public int maxEscaped;
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject[] enemies;
    [SerializeField] int totalEnemies;
    [SerializeField] int maxEnemiesOnScreen;
    public int currentEnemiesOnScreen = 0;
    [SerializeField] int totalWaves;
    [SerializeField] int waveNumber = 1;
    [SerializeField] int waveMultiplier;
    [SerializeField] float spawnWait;
    [SerializeField] float waveWait;
    [SerializeField] int startMoney;
    public int maxMoney;
    public int currentMoney;
    public bool canPlay = false;

    public static List<GameObject> currentEnemies = new List<GameObject>();

    float spawnWaitPassed = 0;
    float waveWaitPassed = 0;
    bool isWaveFinished = false;
    bool didLose = false;
    bool didWin = false;
    bool allWavesEnded = false;
    bool hasEndBanner = false;
    
    int spawnedEnemiesCount = 0;
    int waveEnemiesCount = 0;
    int enemiesTopSortingLayer = 0;
    int rangeFrom = 0;
    int rangeTo = 0;

	private void Start()
	{
		defeatBanner.gameObject.SetActive(false);
        victoryBanner.gameObject.SetActive(false);
		currentMoney = startMoney;
        maxEnemiesOnScreen = waveNumber * waveMultiplier;
    }

	void Update()
    {
        if (canPlay)
        {
            SpawnEnemies();
            SetNextWave();
            AdjustMoney();
            moneyText.text = currentMoney.ToString();
            waveText.text = "Wave " + waveNumber.ToString() + "/" + totalWaves;
            escapedText.text = "Escaped " + escapedNumber.ToString() + "/" + maxEscaped;
            CheckLoss();
            CheckWin();
        }
    }

    void SpawnEnemies()
	{
        if(currentEnemiesOnScreen < totalEnemies && !isWaveFinished && waveNumber <= totalWaves && !didWin && !didLose)
		{
            spawnWaitPassed += Time.deltaTime;
            if (spawnWaitPassed >= spawnWait)
            {
                spawnWaitPassed = 0;
                if (currentEnemiesOnScreen < maxEnemiesOnScreen && spawnedEnemiesCount < totalEnemies)
                {
                    int enemyType = Random.Range(0, enemies.Length);
                    GameObject newEnemy = Instantiate(enemies[enemyType], spawnPoint.position, Quaternion.identity);
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
            if(waveNumber == totalWaves)
			{
                allWavesEnded = true;
			}

            waveWaitPassed += Time.deltaTime;
            if (waveWaitPassed >= waveWait - spawnWait)
			{
                waveEnemiesCount = 0;
                waveNumber++;
                maxEnemiesOnScreen = waveNumber * waveMultiplier;
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

    void CheckWin()
	{
        if (currentEnemies.Count == 0 && !didLose && allWavesEnded)
        {
            waveNumber = totalWaves;
            didWin = true;
            print("You Won");
            EndGame();
        }
    }

    void CheckLoss()
	{
        if(escapedNumber >= maxEscaped)
		{
            print("You Lost");
            didLose = true;
            EndGame();
		}
	}

    public void StartGame()
	{
        canPlay = true;
        playButton.gameObject.SetActive(false);
        hasEndBanner = false;
    }

    void EndGame()
	{
        if (!hasEndBanner)
        {
            hasEndBanner = true;
            canPlay = false;
            playButton.gameObject.SetActive(true);
            playButton.gameObject.GetComponent<RectTransform>().position = new Vector3(0, playButtonYMid, 0);
            if (didLose)
            {
                defeatBanner.gameObject.SetActive(false);
            }
            else if (didWin)
            {
                victoryBanner.gameObject.SetActive(false);
            }
        }
    }
}
