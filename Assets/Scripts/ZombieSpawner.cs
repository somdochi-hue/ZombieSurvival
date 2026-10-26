using UnityEngine;
using System.Collections.Generic;// 유니티 안에 있는 여러개의 배열을 다루는 시스템.
using System.Collections;


public class ZombieSpawner : MonoBehaviour
{
    public UIManager uiManager;
    public Zombie zombiePrefab;
    public ZombieData[] zombieDatas;
    public Transform[] spawnPoints;

    private readonly List<Zombie> zombies = new List<Zombie>();
    private readonly List<Zombie> zombiePool = new List<Zombie>();

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
        uiManager.SetwaveTaxt(wave, zombies.Count);
    }

    private void CreateZombie()
    {
        Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];
        ZombieData data = zombieDatas[Random.Range(0, zombieDatas.Length)];     

        Zombie zombie;
        if (zombiePool.Count == 0)
        {
            zombie = Instantiate(zombiePrefab);
            zombie.gameObject.SetActive(false);
            zombiePool.Add(zombie);
        }

        zombie = zombiePool[0];
        zombie.Setup(data);
        zombie.transform.position = point.position;
        zombie.transform.rotation = point.rotation;
        zombie.gameObject.SetActive(true);
        zombies.Add(zombie);
        zombiePool.Remove(zombie);

        zombie.OnDead.AddListener(() => zombies.Remove(zombie));
        zombie.OnDead.AddListener(() => uiManager.SetwaveTaxt(wave, zombies.Count));
        zombie.OnDead.AddListener(() => StartCoroutine(CoReturnToPoolZombie(zombie)));
    }

    private IEnumerator CoReturnToPoolZombie(Zombie zombie)
    {
        yield return new WaitForSeconds(5f);
        zombie.gameObject.SetActive(false);
        zombiePool.Add(zombie);
    }
}
