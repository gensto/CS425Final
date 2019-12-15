using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchProjectiles : MonoBehaviour
{

	[SerializeField]
	int numberOfProjectiles;

	[SerializeField]
	GameObject projectile;

	Vector2 startPoint;

	float radius, moveSpeed;

	// Use this for initialization
	void Start()
	{
		radius = 20f;
		moveSpeed = 5f;
	}

	// Update is called once per frame
	void Update()
	{
		//if (Input.GetButtonDown("Fire1"))
		//{
		//	startPoint = gameObject.transform.position;
  //          ShootProjectilesInCircle(numberOfProjectiles, moveSpeed);
  //          ShootProjectileAtObject(GameObject.Find("Player"), moveSpeed);
		//}
	}

	void ShootProjectilesInCircle(int numOfProjectiles, float speed)
	{
		float angleStep = 360f / numOfProjectiles;
		float angle = 0f;

		for (int i = 0; i <= numOfProjectiles - 1; i++)
		{

			float projectileDirXposition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
			float projectileDirYposition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

			Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
			Vector2 projectileMoveDirection = (projectileVector - startPoint).normalized * speed;

			var proj = Instantiate(projectile, startPoint, Quaternion.identity);
			proj.GetComponent<Rigidbody2D>().velocity =
				new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

			angle += angleStep;
		}
	}

    void ShootProjectileAtObject(GameObject _target, float speed)
    {
        Rigidbody2D bullet = Instantiate(projectile, transform.position, transform.rotation).GetComponent<Rigidbody2D>();
        bullet.velocity = (_target.transform.position - bullet.transform.position).normalized * speed;
    }

}