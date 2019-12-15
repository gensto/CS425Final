using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    Sprite[] bombSprites = { null, null };

    private SpriteRenderer mRenderer;

    [SerializeField]
    GameObject explosionRemains;

    [SerializeField]
    float switchTime = 2f;
    float subtractTime = 0.1f;


    public float time = 4f;
    public float maxInterval = 1.2f;
    public float minInterval = 0.2f;

    private float interval = 1.0f;
    private float timer = 30.0f;

    void Start()
    {
        mRenderer = GetComponent<SpriteRenderer>();
        timer = time;
    }

    void Update()
    {
        interval = minInterval + timer / time * (maxInterval - minInterval);
        timer -= Time.deltaTime;
        if (timer < 0.0f)
        {
            timer = 0.0f;
            Vector3 position = this.transform.position;

            GameObject bombexplosion = Instantiate(explosionRemains, position, Quaternion.Euler(0, 0, Random.Range(0, 360)));

            Destroy(gameObject);
        }

        if (Mathf.PingPong(Time.time, interval) > (interval / 2.0f))
        {
            mRenderer.sprite = bombSprites[0];
        } else
        {
            mRenderer.sprite = bombSprites[1];
        }
    }
}
