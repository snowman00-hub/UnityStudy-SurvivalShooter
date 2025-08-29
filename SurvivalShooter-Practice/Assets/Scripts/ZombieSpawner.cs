using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public Zombie[] prefabs;
    public Transform[] spawnPoints;

    public GameManager gameManager;

    private float[] respawnIntervals;
    private float[] lastSpawnTimes;
    private int typeCount;

    private void Awake()
    {
        typeCount = prefabs.Length;
        respawnIntervals = new float[typeCount];
        lastSpawnTimes = new float[typeCount];
        for (int i = 0; i < typeCount; i++)
        {
            respawnIntervals[i] = prefabs[i].zombieData.respawnInterval;
            lastSpawnTimes[i] = Time.time;
        }
    }

    private void Update()
    {
        for (int i = 0; i < typeCount; i++)
        {
            if (lastSpawnTimes[i] + respawnIntervals[i] < Time.time)
            {
                lastSpawnTimes[i] = Time.time;
                var zombie = Instantiate(prefabs[i], spawnPoints[i].position,
                    Quaternion.identity, transform);
                zombie.OnDeath += () => gameManager.AddScore(zombie.zombieData.score);
            }
        }
    }
}
