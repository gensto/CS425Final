using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    int vertical = -1;

    [SerializeField]
    float bulletSpeed = 0.4f;
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

        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Hit enemy");

        }
    }
}
