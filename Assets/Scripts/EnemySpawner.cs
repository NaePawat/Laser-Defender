using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] List<WaveConfig> waveConfig;
    [SerializeField] int startingWave = 0; //index
    [SerializeField] bool looping = false;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);
        
    }

    private IEnumerator SpawnAllWaves()
    {
        for(int waveIndex = startingWave;waveIndex<waveConfig.Count;waveIndex++)
        {
            var currentWave = waveConfig[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }
    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for(int i=0;i<waveConfig.getNumberOfEnemies();i++)
        {
            var newEnemy = Instantiate(
            waveConfig.getEnemyPrefabs(),
            waveConfig.getWaypoints()[0].transform.position,
            Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.getTimeBetweenSpawns());
        }
    }
}
