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

    [SerializeField]
    float radius = 10f;
    [SerializeField]
    float power = 10f;

    public float time = 4f;
    public float maxInterval = 1.2f;
    public float minInterval = 0.2f;

    private float interval = 1.0f;
    private float timer = 30.0f;

    public AudioSource myBombFx;
    [SerializeField]
    private AudioClip fuse;
    [SerializeField]
    private AudioClip boom;

    public void FuseSound()
    {
        myBombFx.volume = 0.1f;
        myBombFx.PlayOneShot(fuse);
    }

    public void BoomSound()
    {
        myBombFx.volume = 0.1f;
        myBombFx.PlayOneShot(boom);
    }

    void Start()
    {
        mRenderer = GetComponent<SpriteRenderer>();
        timer = time;
        FuseSound();
    }

    public void AddExplosionForce(Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius)
    {
        var dir = (body.transform.position - explosionPosition);
        float wearoff = 1 - (dir.magnitude / explosionRadius);
        Debug.Log(dir.normalized * explosionForce);

        body.AddForce(dir.normalized * explosionForce);
    }

    void Update()
    {
        RunDown();
    }

    void RunDown()
    {
        Debug.DrawRay(transform.position, Vector2.one * radius, Color.red);
        interval = minInterval + timer / time * (maxInterval - minInterval);
        timer -= Time.deltaTime;
        if (timer < 0.0f)
        {
            BoomSound();
            timer = 0.0f;
            Vector2 position = this.transform.position;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(position, radius);
            foreach (Collider2D hit in colliders)
            {
                Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();

                if (rb != null)
                {
                    Debug.Log(hit.transform.name);
                    AddExplosionForce(rb, power, position, radius);
                }

                Health objectHealth = hit.gameObject.GetComponent<Health>();
                if (objectHealth != null)
                {
                    objectHealth.subtractHealth(1);
                }

            }
            GameObject bombexplosion = Instantiate(explosionRemains, position, Quaternion.Euler(0, 0, Random.Range(0, 360)));

            Destroy(gameObject);
        }

        if (Mathf.PingPong(Time.time, interval) > (interval / 2.0f))
        {
            mRenderer.sprite = bombSprites[0];
        }
        else
        {
            mRenderer.sprite = bombSprites[1];
        }
    }

}
