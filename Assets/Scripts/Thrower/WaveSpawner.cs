using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{

    public enum SpawnState { SPAWNING, WAITING, COUNTING};
    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform[] enemy;
        public int amount;
        public float rate;

    }

    public Wave[] waves;
    private int nextWave = 0;

    private int whichEnemy;
    private int enemyArrayLength;

    public float timeBetwenWaves = 2.0f;
    public float waveCountdown;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    public Transform[] spawnPoints;


    public static WaveSpawner Instance { set; get; }

    public Text waveText, levelText;
    public int countLevel = 1;
    public int countWave = 1;


    private void Start()
    {
        Instance = this;
        waveCountdown = timeBetwenWaves;
    }

    private void Update()
    {
        ShowLevel();

            if (state == SpawnState.WAITING)
            {
                if (!enemyAlive())
                {
                    WaveCompleted();
                }
                else
                {
                    return;
                }
            }

            if (waveCountdown <= 0)
            {
                if (state != SpawnState.SPAWNING)
                {
                    StartCoroutine(SpawnWave(waves[nextWave]));
                }
            }
            else
            {
                waveCountdown -= Time.deltaTime;
            }

    }

   private bool enemyAlive()
    {
        searchCountdown -= Time.deltaTime;
        if(searchCountdown <= 0f)
        {
            searchCountdown = 1f;
        if(GameObject.FindGameObjectWithTag("Enemy") == null)
            return false;
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {

            state = SpawnState.SPAWNING;

            for (int i = 0; i < _wave.amount; i++)
            {
                SpawnEnemy(_wave.enemy[whichEnemy]);
            while (ThrowerMenager.Instance.gamePaused)
            {
                yield return null;
            }
            yield return new WaitForSeconds(1f / _wave.rate); // time to spawn
            }

            enemyArrayLength = _wave.enemy.Length;


            state = SpawnState.WAITING;

            yield break;

    }

    private void SpawnEnemy(Transform _enemy)
    {
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
    }

    private void WaveCompleted()
    {
        state = SpawnState.COUNTING;
        waveCountdown = timeBetwenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            //completed level / looping
            Sounds.Instance.PlayLevel();
            nextWave = 0;
            if (whichEnemy < enemyArrayLength - 1)
                whichEnemy++;
            countLevel++;
            ScoreThrower.Instance.multiplier += 4f;
        }
        else
        {
            nextWave++;
            Sounds.Instance.PlayWave();
        }
    }

    private void ShowLevel()
    {
        countWave = nextWave + 1;
        levelText.text = "Level " + countLevel.ToString();
        waveText.text = "Wave " + countWave.ToString();
    }
}
