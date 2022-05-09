using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// handles the spawning of enemy waves
// needs refactoring, with proper gameobject self destruction after death, along with the healer script
public class WaveSpawner : MonoBehaviour
{
    // struct for enemy waves
    [System.Serializable]
    struct EnemyWave
    {
        public GameObject[] enemyList;
        public bool healerInWave;
    }

    [SerializeField] GameObject potion;
    
    [SerializeField] EnemyWave[] waves;
    public List<GameObject> enemiesAlive;

    int waveNumber = 0;

    Boundary bounds;

    private void Start()
    {
        bounds = GameObject.Find("Level Information").GetComponent<LevelInformation>().Boundary;

        SpawnWave(waves[waveNumber++]);
        Invoke("WaveRefresher", 3);
    }

    void SpawnWave(EnemyWave wave)
    {
        foreach (GameObject enemy in wave.enemyList)
        {
            // instantiates and saves the instances in a separate list
            enemiesAlive.Add(
                Instantiate(
                    enemy,
                    RandomPosition(),
                    enemy.transform.rotation
                    )
                )            ;
        }
        
        // Healer must be set as element 0 of a wave if present
        if (wave.healerInWave)
        {
            HealerEnemy healer = enemiesAlive[0].GetComponent<HealerEnemy>();

            // populates the healer's target list
            foreach (GameObject activeEnemy in enemiesAlive)
            {
                healer.enemiesAlive.Add(activeEnemy);
            }
        }

        Instantiate(potion, RandomPosition(), potion.transform.rotation);
    }

    // generates a random position within bounds
    Vector3 RandomPosition()
    {
        float randomX = Random.Range(bounds.xMin, bounds.xMax);
        float randomZ = Random.Range(bounds.zMin, bounds.zMax);

        return new Vector3(randomX, 1, randomZ);
    }

    // to be called every 3 seconds or so
    void WaveRefresher()
    {
        if (WaveOver())
        {
            foreach (GameObject enemy in enemiesAlive)
            {
                Destroy(enemy);
            }

            enemiesAlive.Clear();

            if (waveNumber < waves.Length)
            {
                SpawnWave(waves[waveNumber++]);
            }
            else
            {
                Debug.Log("Congratulations, all waves cleared!");
                return;
            }
        }

        Invoke("WaveRefresher", 3);
    }
    
    // checks if the current wave is over
    bool WaveOver()
    {
        foreach (GameObject enemy in enemiesAlive)
        {
            if (enemy.activeSelf) { return false; }
        }

        return true;
    }
}
