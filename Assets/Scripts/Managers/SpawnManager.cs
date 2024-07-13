using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float spawnRate = 5;
    private float lastSpawnTime;
    public List<Transform> spawnPoints = new List<Transform>();
    public GameObject spawnObject;
    void Start()
    {
        Transform[] point = GetComponentsInChildren<Transform>();
        spawnPoints.AddRange(point);

        lastSpawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
       if(Time.time > lastSpawnTime + spawnRate && spawnObject)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        lastSpawnTime = Time.time;
        Transform spawnPos = spawnPoints[Random.Range(1, spawnPoints.Count)];
        Instantiate(spawnObject, spawnPos.transform.position, spawnPos.transform.rotation);
    }
}
