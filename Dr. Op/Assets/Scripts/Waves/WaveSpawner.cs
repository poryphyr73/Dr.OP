using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING }

    [Serializable]
    public class Wave
    {
        public string waveName;
        public Transform[] enemy;
        public Transform[] spawnpoints;
        public int amount;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public float timeBetweenWaves = 5f;
    public float waveCountDown;

    public GameObject shop, deadShop;
    public Transform shopSpawn;
    public int shopAppearances;

    private float searchCountDown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    private void Start()
    {
        for (int i = 0; i < waves.Length; i++) if (waves[i].spawnpoints.Length == 0) Debug.LogError("A wave is missing spawnpoints");

        waveCountDown = timeBetweenWaves;
    }

    private void Update()
    {
        if(state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if(waveCountDown <= 0)
        {
            if(state != SpawnState.SPAWNING)
            {
                StartCoroutine( SpawnWave ( waves [nextWave] ) );
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }
    }

    private void WaveCompleted()
    {
        state = SpawnState.COUNTING;
        waveCountDown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("ALL WAVES COMPLETE! Looping...");
        }
        else nextWave++;
    }

    private bool EnemyIsAlive()
    {
        searchCountDown -= Time.deltaTime;
        if (searchCountDown <= 0f) 
        {
            searchCountDown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null) return false;
        } 
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave: " + _wave.waveName);
        state = SpawnState.SPAWNING;

        for(int i = 0; i< _wave.amount; i++)
        {
            SpawnEnemy(_wave.enemy[UnityEngine.Random.Range(0, _wave.enemy.Length)], _wave);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        if (_wave.waveName.Contains("Shop"))
        {
            if (_wave.waveName.Contains("Dead")) SpawnShop(deadShop);
            else SpawnShop(shop);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy, Wave _wave)
    {
        Debug.Log("Spawning Enemy: " + _enemy);

        Transform _sp = _wave.spawnpoints[UnityEngine.Random.Range(0, _wave.spawnpoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
    }

    private void SpawnShop(GameObject shopType)
    {
        shopAppearances++;
        Instantiate(shopType, shopSpawn.transform.position, shopSpawn.transform.rotation);
    }
}
