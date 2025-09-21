using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float orbitSpeed = 2f;
    public float collisionAvoidance = 1f; // How strongly to push away from other meteors/player
    public float playerRepulsionMultiplier = 2f; // Stronger push away from player
    private GameObject player;
    private IGameEvents gameEvents;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameEvents = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (player == null) return;

        Vector2 toPlayer = player.transform.position - transform.position;

        // Orbit movement
        Vector2 perpendicular = new Vector2(-toPlayer.y, toPlayer.x).normalized;
        float distance = toPlayer.magnitude;
        float dynamicOrbitSpeed = distance * orbitSpeed * Time.deltaTime;

        Vector3 movement = perpendicular * dynamicOrbitSpeed;

        // Collision avoidance with meteors and player
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        foreach (var hit in hits)
        {
            if (hit != null && hit.gameObject != this.gameObject)
            {
                float multiplier = 1f;

                if (hit.tag == "Player")
                    multiplier = playerRepulsionMultiplier; // Stronger repulsion from player

                if (hit.tag == "Meteor" || hit.tag == "Player")
                {
                    Vector2 away = (Vector2)(transform.position - hit.transform.position).normalized;
                    movement += (Vector3)(away * collisionAvoidance * multiplier * Time.deltaTime);
                }
            }
        }

        transform.position += movement;
    }

    private void OnTriggerEnter2D(Collider2D whatIHit)
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (whatIHit.tag == "Player")
        {
            gameEvents.OnPlayerHit();
            Destroy(whatIHit.gameObject);
            Destroy(gameObject);
        }
        else if (whatIHit.tag == "Laser")
        {
            gameEvents.OnMeteorDestroyed();
            Destroy(whatIHit.gameObject);
            Destroy(gameObject);
        }
    }
}
