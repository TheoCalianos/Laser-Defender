using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float SpawnRanfomFactor = 0.3f;
    [SerializeField] float numberOfEnemies = 5f;
    [SerializeField] float moveSpeed = 50f;

    public GameObject GetEnemyPrefab() { return enemyPrefab;}
   
    public List<Transform> GetWaypoints()
    {
        var waveWaypoints = new List<Transform>();
        foreach(Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }
        return waveWaypoints;
    }

    public float GettimeBetweenSpawns() { return timeBetweenSpawns; }

    public float GetSpawnRanfomFactor() { return SpawnRanfomFactor; }

    public float GetnumberOfEnemies() { return numberOfEnemies; }

    public float GetmoveSpeed() { return moveSpeed; }
}
