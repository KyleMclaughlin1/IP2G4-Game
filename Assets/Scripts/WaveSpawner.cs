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

    public Transform[] spawnPoints;

    public float timer;

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
        else
        {
            StartCoroutine(waveTimeOut());
        }


    }

    void spawnWave()
    {
        for (int i = 0; i < waves[currentWave].GetMonsterSpawnList().Length; i++)
        {
            Instantiate(waves[currentWave].GetMonsterSpawnList()[i],findSpawnLoc(),Quaternion.identity);
            print(i);
        }
    }

    public Vector3 findSpawnLoc()
    {
        var SpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        return new Vector3(SpawnPoint.position.x, SpawnPoint.position.y, SpawnPoint.position.z);
    }

    IEnumerator waveTimeOut()
    {
        yield return new WaitForSeconds(45f);
        spawnWave();
    }
}
