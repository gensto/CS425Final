using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevel2 : MonoBehaviour
{
    [SerializeField]
    int numberOfProjectiles;

    [SerializeField]
    GameObject projectile;

    Vector2 startPoint;

    float radius, moveSpeed;


    // Start is called before the first frame update
    void Start()
    {
        radius = 20f;
        moveSpeed = 5f;
    }


    // Update is called once per frame
    void Update()
    {
        
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

    void BuildBehaviorTree()
    {
        /*
        MySelectorNode rootSelector = new MySelectorNode();
        MySequenceNode sequenceNode1 = new MySequenceNode();
        MySequenceNode sequenceNode2 = new MySequenceNode();
        CheckIfEnemyIsNearNode checkIfEnemyIsNearNode = new CheckIfEnemyIsNearNode(this);
        ChasePlayerNode chasePlayerNode = new ChasePlayerNode(this);
        ShotByPlayerNode shotByPlayerNode = new ShotByPlayerNode(this);
        PatrolNode patrolNode = new PatrolNode(this);

        sequenceNode1.add_kid(checkIfEnemyIsNearNode);
        sequenceNode1.add_kid(chasePlayerNode);
        sequenceNode2.add_kid(shotByPlayerNode);
        sequenceNode2.add_kid(chasePlayerNode);
        rootSelector.add_kid(sequenceNode1);
        rootSelector.add_kid(sequenceNode2);
        rootSelector.add_kid(patrolNode);


        behavior = new MyBehaviorTree(rootSelector);
        */
    }
}

public class EnemyHealthAbove50 : MyTaskNode
{
    EnemyLevel2 e = null;

    public EnemyHealthAbove50(EnemyLevel2 e)
    {
        this.e = e;
    }
    
    //checks if enemy health above 50
    public override bool run()
    {
        return false;
    }
}

public class ShootAtPlayer : MyTaskNode
{
    EnemyLevel2 e = null;

    public ShootAtPlayer(EnemyLevel2 e)
    {
        this.e = e;
    }

    //shoot at player
    public override bool run()
    {
        return false;
    }
}