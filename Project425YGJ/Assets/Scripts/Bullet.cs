using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    int horizontal = 1;
    int vertical = 1;

    [SerializeField]
    float bulletSpeed = 0.4f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.Translate(new Vector2(horizontal * Time.deltaTime * bulletSpeed, vertical * Time.deltaTime * bulletSpeed));
    }

    public void ShootLeft()
    {
        horizontal = -1;
        vertical = 0;
    }

    public void ShootRight()
    {
        horizontal = 1;
        vertical = 0;
    }

    public void ShootUp()
    {
        horizontal = 0;
        vertical = 1;
    }

    public void ShootDown()
    {
        horizontal = 0;
        vertical = -1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Destroy(gameObject);
    }
}
