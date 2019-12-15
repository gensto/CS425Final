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

    MyBehaviorTree behavior = null;

    GameObject p = null;
    float circleShotTimer = 3.0f;
    public bool timerDone = true;
    public bool shotProjectile = false;

    double fireRate = 0.4;
    double nextFire = 0.0;
    bool didCircleShot = false;
    // Start is called before the first frame update
    void Start()
    {
        p = GameObject.Find("Player");
        radius = 20f;
        moveSpeed = 5f;
    }


    // Update is called once per frame
    void Update()
    {
        if (behavior != null)
        {
            behavior.run();
        }
        else
        {
            BuildBehaviorTree();
        }

        if (!timerDone)
        {
            circleShotCountdown();
        }
    }

    public void ShootProjectilesInCircle(int numOfProjectiles, float speed)
    {
        timerDone = false;
        float angleStep = 360f / numOfProjectiles;
        float angle = 0f;
        startPoint = this.transform.position;

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

    public void ShootProjectileAtObject(GameObject _target, float speed)
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Rigidbody2D bullet = Instantiate(projectile, transform.position, transform.rotation).GetComponent<Rigidbody2D>();
            bullet.velocity = (_target.transform.position - bullet.transform.position).normalized * speed;
        }
    }

    public bool isPlayerNear()
    {
        float distAwayFromPlayer = Vector2.Distance(p.transform.position, this.transform.position);

        if (distAwayFromPlayer > 4.0f)
        {
            return false;
        }

        return true;
    }

    public void circleShotCountdown()
    {
        circleShotTimer -= Time.deltaTime;
        //Debug.Log("Chasing enemy");

        if (circleShotTimer <= 0)
        {
            //Debug.Log("No longer chasing");
            timerDone = true;
            circleShotTimer = Random.Range(2.5f, 4.0f);
        }
    }

    void BuildBehaviorTree()
    {
        MySelectorNode rootSelector = new MySelectorNode();
        MySequenceNode sequenceNode1 = new MySequenceNode();
        MySequenceNode sequenceNode2 = new MySequenceNode();
        CheckIfPlayerIsNearEnemyLevel2Node checkIfPlayerIsNearEnemyLevel2Node = new CheckIfPlayerIsNearEnemyLevel2Node(this);
        ShootAtPlayerNode shootAtPlayerNode = new ShootAtPlayerNode(this);
        TimerAtZeroNode timerAtZeroNode = new TimerAtZeroNode(this);
        ShootInAllDirectionsNode shootInAllDirectionsNode = new ShootInAllDirectionsNode(this);

        sequenceNode1.add_kid(checkIfPlayerIsNearEnemyLevel2Node);
        sequenceNode1.add_kid(shootAtPlayerNode);
        sequenceNode2.add_kid(timerAtZeroNode);
        sequenceNode2.add_kid(shootInAllDirectionsNode);
        rootSelector.add_kid(sequenceNode1);
        rootSelector.add_kid(sequenceNode2);

        behavior = new MyBehaviorTree(rootSelector);
        
    }
}

public class ShootInAllDirectionsNode : MyTaskNode
{
    EnemyLevel2 e = null;

    public ShootInAllDirectionsNode(EnemyLevel2 e)
    {
        this.e = e;
    }
    
    public override bool run()
    {
        e.ShootProjectilesInCircle(8, 2);

        return true;
    }
}

public class ShootAtPlayerNode : MyTaskNode
{
    EnemyLevel2 e = null;

    public ShootAtPlayerNode(EnemyLevel2 e)
    {
        this.e = e;
    }

    //shoot at player
    public override bool run()
    {
        GameObject player = GameObject.Find("Player");
        e.ShootProjectileAtObject(player, 3);

        return true;
    }
}

public class CheckIfPlayerIsNearEnemyLevel2Node : MyTaskNode
{
    EnemyLevel2 e = null;
    string typeOfEnemy = "";

    public CheckIfPlayerIsNearEnemyLevel2Node(EnemyLevel2 e)
    {
        this.e = e;
    }
    //Checks if enemy is near
    public override bool run()
    {
        return e.isPlayerNear();
    }
}

public class TimerAtZeroNode : MyTaskNode
{
    EnemyLevel2 e = null;
    string typeOfEnemy = "";

    public TimerAtZeroNode(EnemyLevel2 e)
    {
        this.e = e;
    }

    public override bool run()
    {
        return e.timerDone;
    }
}