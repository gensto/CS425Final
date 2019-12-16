using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    MyBehaviorTree behavior = null;
    public AnimationCurve slamCurve;
    public GameObject Enemy1;

    public bool isEnraged = false;
    public bool stageOneIsDone = false;
    public bool stageTwoIsDone = false;
    public bool stageThreeIsDone = false;

    public bool doneSlamming = true;
    public bool damagedOnce = false;

    GameObject p = null;
    Vector2 startPoint;
    Health myHealth;

    public bool circleShotTimerDone = false;
    public bool spawnEnemyTimerDone = false;
    float spawnEnemyTimer = 4.0f;
    float circleShotTimer = 3.0f;
    double fireRate = 0.4;
    double nextFire = 0.0;
    float radius;
    [SerializeField]
    float slamRadius = 2;
    bool stopChasing = true;
    float stopChasingTimer = 1.0f;
    bool currentlySlamming = false;

    [SerializeField]
    private AudioClip shoot;
    [SerializeField]
    private AudioClip shootCircle;
    [SerializeField]
    GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {
        radius = 20f;
        p = GameObject.Find("Player");
        myHealth = GetComponent<Health>();
        myHealth.setHealth(100);
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

        chasePlayer();
        isBossEnraged();
        stageStatus();
        checkTimers();
        
        if (myHealth.getHealth() <= 0)
        {
            Debug.Log("Dead");
        }
       // Debug.DrawRay(transform.position-Vector3.up, Vector2.one * slamRadius, Color.red);

    }

    public void checkTimers()
    {
        if (!circleShotTimerDone)
        {
            circleShotCountdown();
        }

        if (!spawnEnemyTimerDone)
        {
            spawnEnemyCountdown();
        }
    }


    public void chasePlayer()
    {
        //Debug.Log("Move towards playerrrr");
        if (doneSlamming)
        {
            float step = 1 * Time.deltaTime;
            this.transform.position = Vector2.MoveTowards(this.transform.position, p.transform.position, step);
        }
    }

    public void ShootProjectilesInCircle(int numOfProjectiles, float speed)
    {
        circleShotTimerDone = false;
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
        ShootCircleSound();
    }

    public void ShootProjectileAtObject(GameObject _target, float speed)
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Rigidbody2D bullet = Instantiate(projectile, transform.position + Vector3.up, transform.rotation).GetComponent<Rigidbody2D>();
            bullet.velocity = (_target.transform.position - bullet.transform.position).normalized * speed;
            ShootSound();
        }
    }

    public void ShootSound()
    {
        //myShootFx.volume = 0.2f;
        AudioSource.PlayClipAtPoint(shoot, transform.position);
    }

    public void ShootCircleSound()
    {
        //myShootFx.volume = 0.2f;
        //AudioSource.PlayClipAtPoint(shootCircle, transform.position);
    }

    public void circleShotCountdown()
    {
        circleShotTimer -= Time.deltaTime;
        //Debug.Log("Chasing enemy");

        if (circleShotTimer <= 0)
        {
            //Debug.Log("No longer chasing");
            circleShotTimerDone = true;
            circleShotTimer = Random.Range(2.5f, 4.0f);
        }
    }

    public void spawnEnemyCountdown()
    {
        spawnEnemyTimer -= Time.deltaTime;
        //Debug.Log("Chasing enemy");

        if (spawnEnemyTimer <= 0)
        {
            //Debug.Log("No longer chasing");
            spawnEnemyTimerDone = true;
            spawnEnemyTimer = 4.0f;
        }
    }

    void stageStatus()
    {
        Debug.Log("Enemy health: " + myHealth.getHealth());
        if (myHealth.getHealth() < 70 )
        {
            stageOneIsDone = true;
        }
        else if (myHealth.getHealth() < 40)
        {
            stageTwoIsDone = true;
        }
    }

    void isBossEnraged()
    {
        if (myHealth.getHealth() >= 70 && myHealth.getHealth() <= 84)
        {
            isEnraged = true;
        }
        else if (myHealth.getHealth() >= 40 && myHealth.getHealth() <= 54)
        {
            isEnraged = true;
        }
        else if (myHealth.getHealth() >= 0 && myHealth.getHealth() <= 16)
        {
            isEnraged = true;
        }
        else
        {
            isEnraged = false;
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

    IEnumerator SlamCoroutine()
    {
        GetComponent<Animator>().SetTrigger("DoSlam");
        yield return new WaitForSeconds(1.12f);
        EndSlam();
    }

    /**
     * CALL THIS
     */
    public void StartSlam()
    {
        doneSlamming = false;
        StartCoroutine(SlamCoroutine());
    }

    public void AddExplosionForce(Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius)
    {
        var dir = (body.transform.position - Vector3.up - explosionPosition);
        float wearoff = 1 - (dir.magnitude / explosionRadius);
        Debug.Log(dir.normalized * explosionForce);

        body.AddForce(dir.normalized * explosionForce);
    }

    public bool insideCircle()
    {
        Vector2 position = this.transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position - Vector2.up, slamRadius); // 2 is radius of effect for slam
        foreach (Collider2D hit in colliders)
        {
            if (hit.gameObject.tag == "Player")
            {
                Debug.Log("Player in circle");
                return true;
            }

        }

        return false;
    }

    void EndSlam()
    {
        Vector2 position = this.transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position - Vector2.up, slamRadius); // 2 is radius of effect for slam
        foreach (Collider2D hit in colliders)
        {
            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                Debug.Log(hit.transform.name);
                AddExplosionForce(rb, 1000, position, slamRadius);
            }

            Health objectHealth = hit.gameObject.GetComponent<Health>();
            if (objectHealth != null && !hit.gameObject.GetComponent<Boss>() && !damagedOnce)
            {
                objectHealth.subtractHealth(1);
                damagedOnce = true;
            }

        }

        damagedOnce = false;
        doneSlamming = true;
        //GameObject bombexplosion = Instantiate(explosionRemains, position, Quaternion.Euler(0, 0, Random.Range(0, 2) == 1 ? 0 : 180));
    }



    public void SpawnDustStorms()
    {
        spawnEnemyTimerDone = false;

        Vector2 pos = transform.position;
        pos.y -= 1;
        Vector2 pos2 = transform.position;
        pos2.x -= 1;
        pos2.y -= 1;
        Vector2 pos3 = transform.position;
        pos3.x += 1;
        pos3.y -= 1;
        GameObject dustStorm = Instantiate(Enemy1, pos, gameObject.transform.rotation);
        GameObject dustStorm2 = Instantiate(Enemy1, pos2, gameObject.transform.rotation);
        GameObject dustStorm3 = Instantiate(Enemy1, pos3, gameObject.transform.rotation);

    }

    public int getBossCurrHealth()
    {
        return myHealth.getHealth();
    }

    void BuildBehaviorTree()
    {
        
        MySelectorNode rootSelector = new MySelectorNode();
        MySequenceNode sequenceNode1 = new MySequenceNode();
        MySequenceNode sequenceNode2 = new MySequenceNode();
        MySequenceNode sequenceNode3 = new MySequenceNode();
        MySequenceNode sequenceNode4 = new MySequenceNode();
        MySelectorNode selectorNode1 = new MySelectorNode();

        StageOneDone stageOneDone = new StageOneDone(this);
        StageOneIsNotDone stageOneIsNotDone = new StageOneIsNotDone(this);
        StageTwoDone stageTwoDone = new StageTwoDone(this);
        StageTwoIsNotDone stageTwoIsNotDone = new StageTwoIsNotDone(this);
        StageThreeIsNotDone stageThreeIsNotDone = new StageThreeIsNotDone(this);
        Enraged enraged = new Enraged(this);
        SprayBulletsTimerAtZeroNode sprayBulletsTimerAtZeroNode = new SprayBulletsTimerAtZeroNode(this);
        SprayBulletsNode sprayBulletsNode = new SprayBulletsNode(this);
        SpawnEnemyTimerAtZeroNode spawnEnemyTimerAtZeroNode = new SpawnEnemyTimerAtZeroNode(this);
        SpawnEnemiesNode spawnEnemiesNode = new SpawnEnemiesNode(this);
        PlayerNearBossNode playerNearBossNode = new PlayerNearBossNode(this);
        BodySlamNode bodySlamNode = new BodySlamNode(this);
        ShootAtPlayerBossNode shootAtPlayerBossNode = new ShootAtPlayerBossNode(this);

        sequenceNode1.add_kid(stageOneIsNotDone);
        sequenceNode1.add_kid(enraged);
        sequenceNode1.add_kid(sprayBulletsTimerAtZeroNode);
        sequenceNode1.add_kid(sprayBulletsNode);

        sequenceNode2.add_kid(stageOneDone);
        sequenceNode2.add_kid(stageTwoIsNotDone);
        sequenceNode2.add_kid(enraged);
        sequenceNode2.add_kid(spawnEnemyTimerAtZeroNode);
        sequenceNode2.add_kid(spawnEnemiesNode);

        sequenceNode3.add_kid(stageTwoDone);
        sequenceNode3.add_kid(stageThreeIsNotDone);
        sequenceNode3.add_kid(enraged);
        sequenceNode3.add_kid(sprayBulletsTimerAtZeroNode);
        sequenceNode3.add_kid(sprayBulletsNode);
        sequenceNode3.add_kid(spawnEnemyTimerAtZeroNode);
        sequenceNode3.add_kid(spawnEnemiesNode);

        sequenceNode4.add_kid(playerNearBossNode);
        sequenceNode4.add_kid(bodySlamNode);

        selectorNode1.add_kid(sequenceNode4);
        selectorNode1.add_kid(shootAtPlayerBossNode);

        rootSelector.add_kid(sequenceNode1);
        rootSelector.add_kid(sequenceNode2);
        rootSelector.add_kid(sequenceNode3);
        rootSelector.add_kid(selectorNode1);

        behavior = new MyBehaviorTree(rootSelector);
   
    }
}

public class StageOneDone : MyTaskNode
{
    Boss e = null;

    public StageOneDone(Boss e)
    {
        this.e = e;
    }

    public override bool run()
    {
        if(e.stageOneIsDone)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

public class StageOneIsNotDone : MyTaskNode
{
    Boss e = null;

    public StageOneIsNotDone(Boss e)
    {
        this.e = e;
    }

    public override bool run()
    {
        if (e.stageOneIsDone)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}

public class StageTwoDone : MyTaskNode
{
    Boss e = null;

    public StageTwoDone(Boss e)
    {
        this.e = e;
    }

    public override bool run()
    {
        if(e.stageTwoIsDone)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

public class StageTwoIsNotDone : MyTaskNode
{
    Boss e = null;

    public StageTwoIsNotDone(Boss e)
    {
        this.e = e;
    }

    public override bool run()
    {
        if (e.stageTwoIsDone)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}

public class StageThreeIsNotDone : MyTaskNode
{
    Boss e = null;

    public StageThreeIsNotDone(Boss e)
    {
        this.e = e;
    }

    public override bool run()
    {
        if (e.stageThreeIsDone)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}

public class Enraged : MyTaskNode
{
    Boss e = null;

    public Enraged(Boss e)
    {
        this.e = e;
    }

    public override bool run()
    {
        Debug.Log("Is enraged: " + e.isEnraged);
        return e.isEnraged;
    }
}

public class SprayBulletsTimerAtZeroNode : MyTaskNode
{
    Boss e = null;

    public SprayBulletsTimerAtZeroNode(Boss e)
    {
        this.e = e;
    }

    public override bool run()
    {
        return e.circleShotTimerDone;
    }
}


public class SprayBulletsNode : MyTaskNode
{
    Boss e = null;

    public SprayBulletsNode(Boss e)
    {
        this.e = e;
    }

    public override bool run()
    {
        e.ShootProjectilesInCircle(6, 3f);
        return true;
    }
}

public class SpawnEnemyTimerAtZeroNode : MyTaskNode
{
    Boss e = null;

    public SpawnEnemyTimerAtZeroNode(Boss e)
    {
        this.e = e;
    }

    public override bool run()
    {
        return e.spawnEnemyTimerDone;
    }
}

public class SpawnEnemiesNode : MyTaskNode
{
    Boss e = null;

    public SpawnEnemiesNode(Boss e)
    {
        this.e = e;
    }

    public override bool run()
    {
        e.SpawnDustStorms();
        return true;
    }
}

public class PlayerNearBossNode : MyTaskNode
{
    Boss e = null;

    public PlayerNearBossNode(Boss e)
    {
        this.e = e;
    }

    public override bool run()
    {
        //return e.isPlayerNear();
        return e.insideCircle();
    }
}

public class BodySlamNode : MyTaskNode
{
    Boss e = null;

    public BodySlamNode(Boss e)
    {
        this.e = e;
    }

    public override bool run()
    {
        if (e.doneSlamming)
        {
            e.StartSlam();
        }
        return true;
    }
}

public class ShootAtPlayerBossNode : MyTaskNode
{
    Boss e = null;

    public ShootAtPlayerBossNode(Boss e)
    {
        this.e = e;
    }

    public override bool run()
    {
        GameObject player = GameObject.Find("Player");
        e.ShootProjectileAtObject(player, 2.5f);
        return true;
    }
}
