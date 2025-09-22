using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : MonoBehaviour, IGameEvents
{
    public GameObject playerPrefab;
    public GameObject meteorPrefab;
    public GameObject bigMeteorPrefab;

    public CinemachineVirtualCamera vcam;
    private CinemachineImpulseSource impulseSource;

    public bool gameOver = false;
    public int meteorCount = 0;

    [Header("Meteor Spawn Settings")]
    public float spawnRadius = 5f; // Distance around the player to spawn meteors
    public float spawnInterval = 2f; // Seconds between meteor spawns

    private GameObject player;
    private ISpawner meteorSpawner;
    private ISpawner bigMeteorSpawner;

    void Start()
    {
        // Spawn the player
        player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

        impulseSource = GetComponent <CinemachineImpulseSource>();

        // Make the virtual camera follow the spawned player
        if (vcam != null && player != null)
        {
            vcam.Follow = player.transform;
        }

        meteorSpawner = new MeteorSpawner(meteorPrefab, player.transform, spawnRadius);
        bigMeteorSpawner = new BigMeteorSpawner(bigMeteorPrefab);

        // Start spawning meteors
        InvokeRepeating("SpawnMeteor", 1f, spawnInterval);
    }

    public void OnPlayerHit()
    {
        gameOver = true;
    }

    public void OnMeteorDestroyed()
    {
        meteorCount++;
        if (meteorCount >= 5)
        {
            meteorCount = 0;
            bigMeteorSpawner.Spawn();
        }
    }

    public void OnBigMeteorDestroyed()
    {
        CameraShakeManager.instance.CameraShake(impulseSource);
        Debug.Log("Future explosion effect");
    }

    void Update()
    {
        if (gameOver)
        {
            CancelInvoke();
        }

        if (Input.GetKeyDown(KeyCode.R) && gameOver)
            SceneManager.LoadScene("Week5Lab");
    }

    void SpawnMeteor()
    {
        if (player == null) return;

        Vector2 spawnPos;
        int attempts = 0;

        do
        {
            spawnPos = (Vector2)player.transform.position + Random.insideUnitCircle * spawnRadius;
            attempts++;
        }
        while (Vector2.Distance(spawnPos, player.transform.position) < 1.5f && attempts < 10); // keep at least 1.5 units away

        Instantiate(meteorPrefab, spawnPos, Quaternion.identity);
    }

    void SpawnBigMeteor()
    {
        float xPos = Random.Range(-8f, 8f);
        Vector2 spawnPos = new Vector2(xPos, 7.5f);
        Instantiate(bigMeteorPrefab, spawnPos, Quaternion.identity);
    }
}
