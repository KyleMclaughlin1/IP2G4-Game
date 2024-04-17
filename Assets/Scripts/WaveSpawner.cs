using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class WaveContent
    {
        [SerializeField] GameObject[] monsterSpawn;

        public GameObject[] GetMonsterSpawnList()
        {
            return monsterSpawn;
        }
    }

    [SerializeField] WaveContent[] waves;
    int currentWave = 0;

    public Transform[] track1SpawnPoints;
    public Transform[] track2SpawnPoints;
    public Transform[] track3SpawnPoints;

    public LapChecker track;

    public float timer;
    public float timeBetweenWaves;


    // Start is called before the first frame update
    void Start()
    {
        spawnWave();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindWithTag("enemy") == null)
        {
            currentWave++;
            spawnWave();
        }

        timer += Time.deltaTime;

        if (timer > timeBetweenWaves)
        {
            timer = 0;
            spawnWave();
        }
    }

    void spawnWave()
    {
        if (track.currentTrack == 0)
        {
            for (int i = 0; i < waves[currentWave].GetMonsterSpawnList().Length; i++)
            {
                Instantiate(waves[currentWave].GetMonsterSpawnList()[i], findSpawnLoc(), Quaternion.identity);
                print(i);
            }
        }
        else if (track.currentTrack == 1)
        {
            for (int i = 0; i < waves[currentWave].GetMonsterSpawnList().Length; i++)
            {
                Instantiate(waves[currentWave].GetMonsterSpawnList()[i], findSpawnLoc2(), Quaternion.identity);
                print(i);
            }
        }
        else
        {
            for (int i = 0; i < waves[currentWave].GetMonsterSpawnList().Length; i++)
            {
                Instantiate(waves[currentWave].GetMonsterSpawnList()[i], findSpawnLoc3(), Quaternion.identity);
                print(i);
            }
        }
    }

    public Vector3 findSpawnLoc()
    {
        var SpawnPoint1 = track1SpawnPoints[Random.Range(0, track1SpawnPoints.Length)];

        return new Vector3(SpawnPoint1.position.x, SpawnPoint1.position.y, SpawnPoint1.position.z);
    }

    public Vector3 findSpawnLoc2()
    {
        var SpawnPoint2 = track2SpawnPoints[Random.Range(0, track2SpawnPoints.Length)];

        return new Vector3(SpawnPoint2.position.x, SpawnPoint2.position.y, SpawnPoint2.position.z);
    }

    public Vector3 findSpawnLoc3()
    {
        var SpawnPoint3 = track3SpawnPoints[Random.Range(0, track3SpawnPoints.Length)];

        return new Vector3(SpawnPoint3.position.x, SpawnPoint3.position.y, SpawnPoint3.position.z);
    }
}
