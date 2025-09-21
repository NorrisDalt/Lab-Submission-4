using UnityEngine;

public class BigMeteorSpawner : ISpawner
{
    private GameObject bigMeteorPrefab;

    public BigMeteorSpawner(GameObject prefab)
    {
        bigMeteorPrefab = prefab;
    }

    public void Spawn()
    {
        float xPos = Random.Range(-8f, 8f);
        Vector2 spawnPos = new Vector2(xPos, 7.5f);
        Object.Instantiate(bigMeteorPrefab, spawnPos, Quaternion.identity);
    }
}
