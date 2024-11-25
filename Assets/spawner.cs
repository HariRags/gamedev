using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab; // The enemy prefab to spawn
    [SerializeField] private Transform spawnLocation; // The location to spawn the enemies
    [SerializeField] private float initialSpawnInterval = 5f; // Initial time interval between spawns
    [SerializeField] private float minSpawnInterval = 1f; // Minimum time interval between spawns
    [SerializeField] private float spawnIntervalDecrement = 0.1f; // Amount to decrement the spawn interval each time
    [SerializeField] private Transform[] waypoints; // Waypoints for the enemies to follow

    private float currentSpawnInterval;

    // Start is called before the first frame update
    void Start()
    {
        currentSpawnInterval = initialSpawnInterval;
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(currentSpawnInterval);
            currentSpawnInterval = Mathf.Max(minSpawnInterval, currentSpawnInterval - spawnIntervalDecrement);
        }
    }

    private void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(enemyPrefab, spawnLocation.position, Quaternion.identity);
        enemyAI enemyAIComponent = newEnemy.GetComponent<enemyAI>();
        if (enemyAIComponent != null)
        {
            enemyAIComponent.SetWaypoints(waypoints);
        }
    }
}