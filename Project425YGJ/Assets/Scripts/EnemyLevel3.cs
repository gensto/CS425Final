using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevel3 : MonoBehaviour
{
    [SerializeField]
    GameObject bombPrefab;
    bool placeBombs = false;

    float spawnBombTimer = 2.0f;
    bool spawnBombTimerDone = true;
    bool spawningBomb = false;

    double fireRate = 0.4;
    double nextFire = 0.0;

    public AnimationCurve myCurve;

    GameObject p = null;
    void Start()
    {
        p = GameObject.Find("Player");
    }

    void Update()
    {
        //transform.position = new Vector3(transform.position.x, myCurve.Evaluate((Time.time % myCurve.length)), transform.position.z);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBomb();
        }

        if(!spawnBombTimerDone)
            spawnBombCountdown();

        if(spawnBombTimerDone)
            SpawnBomb();
    }

    public void spawnBombCountdown()
    {
        spawnBombTimer -= Time.deltaTime;
        //Debug.Log("Chasing enemy");

        if (spawnBombTimer <= 0)
        {
            //Debug.Log("No longer chasing");
            spawnBombTimerDone = true;
            spawningBomb = false;
            spawnBombTimer = 2.0f;
        }
    }

    void SpawnBomb()
    {
        spawnBombTimerDone = false;
        spawningBomb = true;

        for (int i = 0; i < 3; i++)
        {
            Vector2 newPos = new Vector2(Random.Range(-4f, 4f), Random.Range(-4f, 4f));
            GameObject bomb = Instantiate(bombPrefab, newPos, gameObject.transform.rotation);
        }
    }
}
