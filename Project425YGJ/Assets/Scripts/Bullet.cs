using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    int vertical = -1;

    [SerializeField]
    float bulletSpeed = 0.4f;

    [Tooltip("How the bullet will affect a gameobjects health when it hits.")][SerializeField]
    int healthAffect = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.Translate(new Vector2(0, vertical * Time.deltaTime * bulletSpeed));
    }

    public void ShootLeft()
    {
        transform.Rotate(new Vector3(0, 0, 270));
    }

    public void ShootRight()
    {
        transform.Rotate(new Vector3(0, 0, 90));
    }

    public void ShootUp()
    {
        transform.Rotate(new Vector3(0, 0, 180));
    }

    public void ShootDown()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        Health objectHealth = collision.gameObject.GetComponent<Health>();

        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Hit enemy");
            Vector3 moveDirection = transform.position - collision.transform.position;
            collision.transform.gameObject.GetComponent<Rigidbody2D>().AddForce(moveDirection.normalized * -50f);
            collision.gameObject.GetComponent<EnemyLevel1>().isShotByPlayer = true;
        }

        // If a health component is present affect their health
        if (objectHealth != null)
        {
            objectHealth.subtractHealth(healthAffect);
        }
    }
}
