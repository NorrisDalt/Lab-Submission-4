using UnityEngine;
using Cinemachine;

public class BigMeteorSpawner : ISpawner
{
    private GameObject bigMeteorPrefab;
    private CinemachineVirtualCamera vcam;

    public BigMeteorSpawner(GameObject prefab, CinemachineVirtualCamera camera)
    {
        bigMeteorPrefab = prefab;
        vcam = camera;
    }

    public void Spawn()
    {
        float xPos = Random.Range(-8f, 8f);
        Vector2 spawnPos = new Vector2(xPos, 7.5f);
        Object.Instantiate(bigMeteorPrefab, spawnPos, Quaternion.identity);

        CameraShakeManager.instance.ZoomOut(vcam, 80f);
    }
}
