using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    // Movement variables
    [SerializeField] // Serialize Field will make it private but visible in the inspector
    float runSpeed = 20.0f;
    [SerializeField]
    float diagonalPenalty = 0.7f;
    float horizontal;
    float vertical;

    double fireRate = 0.4;
    double nextFire = 0.0;

    // Component Variables
    Rigidbody2D body;
    SpriteRenderer spRenderer;
    Animator animator;

    //Animator paramaters



    // Bullet Variables
    [SerializeField]
    public Bullet bulletPrefab;
    [SerializeField]
    float bulletSpawnOffset = 0.2f; // how far off the player to spawn a bullet


    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        HandleShooting();

        //animator.SetFloat("Strafe", h);
        //animator.SetBool("Fire", fire);
    }

    void FixedUpdate()
    {
        HandleMovement();

        if (body.velocity.y == 0 && body.velocity.x == 0)
        {
            //Debug.Log("Not moving");
            animator.SetBool("moving", false);
        } else
        {
            //Debug.Log("moving");
            animator.SetBool("moving", true);
        }
    }

    /**
     * Handle each key press with the appropriate movement
     */
    void HandleMovement()
    {
        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        {
            body.velocity = new Vector2(horizontal * runSpeed * diagonalPenalty, vertical * runSpeed * diagonalPenalty);
        }
        else
        {
            body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
        }

    }

    /**
     * Handle the shooting of projectiles
     */
    void HandleShooting()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            //GameObject clone = Instantiate(projectile, transform.position, transform.rotation) as GameObject;

            if (Input.GetKey(KeyCode.UpArrow))
            {
                animator.SetBool("shootingBack", true);
                animator.SetBool("shootingSide", false);
                // Instantiate the projectile at the position and rotation of this transform
                Bullet clone = Instantiate(bulletPrefab, transform.position + (Vector3.up * bulletSpawnOffset), transform.rotation);
                // Set the direction of bullet
                clone.ShootUp();
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                animator.SetBool("shootingBack", false);
                animator.SetBool("shootingSide", false);
                // Instantiate the projectile at the position and rotation of this transform
                Bullet clone = Instantiate(bulletPrefab, transform.position - (Vector3.up * bulletSpawnOffset), transform.rotation);
                // Set the direction of bullet
                clone.ShootDown();
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                animator.SetBool("shootingBack", false);
                animator.SetBool("shootingSide", true);
                spRenderer.flipX = false;
                // Instantiate the projectile at the position and rotation of this transform
                Bullet clone = Instantiate(bulletPrefab, transform.position + (Vector3.left * bulletSpawnOffset), transform.rotation);
                // Set the direction of bullet
                clone.ShootLeft();
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                animator.SetBool("shootingBack", false);
                animator.SetBool("shootingSide", true);
                // Instantiate the projectile at the position and rotation of this transform
                Bullet clone = Instantiate(bulletPrefab, transform.position - (Vector3.left * bulletSpawnOffset), transform.rotation);
                // Set the direction of bullet
                clone.ShootRight();
                spRenderer.flipX = true;
            }
            else
            {
                animator.SetBool("shootingBack", false);
                animator.SetBool("shootingSide", false);
            }
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
