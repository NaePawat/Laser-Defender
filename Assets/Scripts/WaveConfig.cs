using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefabs;
    [SerializeField] GameObject pathPrefabs;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed = 2f;

    public GameObject getEnemyPrefabs() { return enemyPrefabs; }
    public List<Transform> getWaypoints() 
    {
        var waveWayPoints = new List<Transform>();
        foreach (Transform child in pathPrefabs.transform)
        {
            waveWayPoints.Add(child);
        }
        return waveWayPoints; 
    }
    public float getTimeBetweenSpawns() { return timeBetweenSpawns; }
    public float getSpawnRandomFactor() { return spawnRandomFactor; }
    public float getMoveSpeed() { return moveSpeed; }
    public int getNumberOfEnemies() { return numberOfEnemies; }

}
