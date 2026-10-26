using UnityEngine;
using System.Collections.Generic;  // 유니티 안에 있는 여러개의 배열을 다루는 시스템.

public class ZombieSpawner : MonoBehaviour
{
    public Zombie zombiePrefab;
    public ZombieData[] zombieDatas;
    public Transform[] spawnPoints;

    private readonly List<Zombie> zombies = new List<Zombie>();
    private int wave = 0;

    private void Update()
    {
        if (zombies.Count == 0)
        {
            SpawnWave();
        }
    }

    private void SpawnWave()
    {
        wave++;

        int count = Mathf.RoundToInt(wave * 1.5f);
        for (int i = 0; i < count; i++)
        {
            CreateZombie();
        }
    }

    private void CreateZombie()
    {
        Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];
        ZombieData data = zombieDatas[Random.Range(0, zombieDatas.Length)];
        Zombie zombie = Instantiate(zombiePrefab, point.position, point.rotation);
        zombie.Setup(data);
        zombies.Add(zombie);
        zombie.OnDead.AddListener(() => zombies.Remove(zombie));
        zombie.OnDead.AddListener(() => Destroy(zombie.gameObject, 5f));
    }
}
