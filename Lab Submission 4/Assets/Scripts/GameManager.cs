using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public CinemachineVirtualCamera vcam;
    public GameObject meteorPrefab;
    public GameObject bigMeteorPrefab;
    public bool gameOver = false;

    public int meteorCount = 0;

    void Start()
    {
        // Spawn the player
        GameObject playerInstance = Instantiate(playerPrefab, transform.position, Quaternion.identity);

        // Make the virtual camera follow the spawned player
        if (vcam != null && playerInstance != null)
        {
            vcam.Follow = playerInstance.transform;
        }

        // Start spawning meteors
        InvokeRepeating("SpawnMeteor", 1f, 2f);
    }

    void Update()
    {
        if (gameOver)
        {
            CancelInvoke();
        }

        if (Input.GetKeyDown(KeyCode.R) && gameOver)
        {
            SceneManager.LoadScene("Week5Lab");
        }

        if (meteorCount == 5)
        {
            BigMeteor();
        }
    }

    void SpawnMeteor()
    {
        Instantiate(meteorPrefab, new Vector3(Random.Range(-8f, 8f), 7.5f, 0), Quaternion.identity);
    }

    void BigMeteor()
    {
        meteorCount = 0;
        Instantiate(bigMeteorPrefab, new Vector3(Random.Range(-8f, 8f), 7.5f, 0), Quaternion.identity);
    }
}

