using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    // Movement variables
    [SerializeField] // Serialize Field will make it private but visible in the inspector
    float runSpeed = 20.0f;
    float horizontal;
    float vertical;

    // Component Variables
    Rigidbody2D body;

    // Bullet Variables
    [SerializeField]
    Bullet bulletPrefab;
    float bulletSpawnOffset = 0.2f; // how far off the player to spawn a bullet


    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        HandleShooting();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    /**
     * Handle each key press with the appropriate movement
     */
    void HandleMovement()
    {
        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        {
            // No diagonal movement.
        } else
        {
            body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
        }

    }

    /**
     * Handle the shooting of projectiles
     */
    void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // Instantiate the projectile at the position and rotation of this transform
            Bullet clone = Instantiate(bulletPrefab, transform.position + (Vector3.up * bulletSpawnOffset), transform.rotation);
            // Set the direction of bullet
            clone.ShootUp();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // Instantiate the projectile at the position and rotation of this transform
            Bullet clone = Instantiate(bulletPrefab, transform.position - (Vector3.up * bulletSpawnOffset), transform.rotation);
            // Set the direction of bullet
            clone.ShootDown();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Instantiate the projectile at the position and rotation of this transform
            Bullet clone = Instantiate(bulletPrefab, transform.position + (Vector3.left * bulletSpawnOffset), transform.rotation);
            // Set the direction of bullet
            clone.ShootLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Instantiate the projectile at the position and rotation of this transform
            Bullet clone = Instantiate(bulletPrefab, transform.position - (Vector3.left * bulletSpawnOffset), transform.rotation);
            // Set the direction of bullet
            clone.ShootRight();
        }
    }

    /**
     * Get the input and set variables for movement
     */
    void GetInput()
    {
        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down
    }
}
