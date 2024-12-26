using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    private int enemiesAlive = 0;
    [SerializeField] private Wave[] waves;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float countDown = 2f;
    private Bounds bounds;
    public static int WaveNumber;
    public static int MaxWaves;

    private void Start()
    {
        bounds = GetComponent<Collider>().bounds;
        GlobalEventManager.OnEnemyKilled.AddListener(OnEnemyKilled);
    }

    private void OnEnable()
    {
        WaveNumber = 0;
        MaxWaves = waves.Length;
    }

    private void Update()
    {
        if (enemiesAlive > 0) { return; }

        if (WaveNumber == waves.Length)
        {
            GameManager.WinLevel();
            enabled = false;
        }

        if (countDown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countDown = timeBetweenWaves;
            return;
        }

        countDown -= Time.deltaTime;
        countDown = Mathf.Clamp(countDown, 0f, Mathf.Infinity);
    }
    private IEnumerator SpawnWave()
    {
        // PlayerStats.Rounds++;

		Wave wave = waves[WaveNumber];
        WaveNumber++;

		enemiesAlive = wave.enemiesCount;

		for (int i = 0; i < wave.enemiesCount; i++)
		{
			SpawnEnemy(wave.enemyPrefab);
			yield return new WaitForSeconds(1f / wave.spawnRate);
		}
    }
    
    private void SpawnEnemy(GameObject enemy)
    {
        Vector3 spawnPosition = new(
            bounds.center.x,
            bounds.center.y,
            Random.Range(bounds.min.z + 0.5f, bounds.max.z - 0.5f)
        );

        Instantiate(enemy, spawnPosition, Quaternion.identity, transform);
    }

    private void OnEnemyKilled(Transform _) { enemiesAlive--; }
}
