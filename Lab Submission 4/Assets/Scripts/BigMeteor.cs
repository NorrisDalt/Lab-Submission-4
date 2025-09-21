using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMeteor : MonoBehaviour
{
    private int hitCount = 0;
    private IGameEvents gameEvents;

    // Start is called before the first frame update
    void Start()
    {
        gameEvents = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * 0.5f);

        if (transform.position.y < -11f)
        {
            Destroy(gameObject);
        }

        if (hitCount >= 5)
        {
            gameEvents.OnBigMeteorDestroyed();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D whatIHit)
    {
        if (whatIHit.tag == "Player")
        {
            gameEvents.OnPlayerHit();
            Destroy(whatIHit.gameObject);
        }
        else if (whatIHit.tag == "Laser")
        {
            hitCount++;
            Destroy(whatIHit.gameObject);
        }
    }
}
