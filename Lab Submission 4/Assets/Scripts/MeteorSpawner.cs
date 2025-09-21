using UnityEngine;

public class MeteorSpawner : ISpawner
{
    private GameObject meteorPrefab;
    private Transform player;
    private float spawnRadius;

    public MeteorSpawner(GameObject prefab, Transform player, float radius)
    {
        meteorPrefab = prefab;
        this.player = player;
        spawnRadius = radius;
    }

    public void Spawn()
    {
        if (player == null) return;

        Vector2 spawnPos;
        int attempts = 0;

        do
        {
            spawnPos = (Vector2)player.position + Random.insideUnitCircle * spawnRadius;
            attempts++;
        }
        while (Vector2.Distance(spawnPos, player.position) < 1.5f && attempts < 10);

        Object.Instantiate(meteorPrefab, spawnPos, Quaternion.identity);
    }
}
