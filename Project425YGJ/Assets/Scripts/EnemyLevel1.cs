using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevel1 : MonoBehaviour
{
    GameObject p = null;
    float speed = 2f;
    [SerializeField]
    int hitDamage = 1;
    public bool isShotByPlayer = false;
    private Rigidbody2D _rigidbody;
    float chaseTimer = 3.0f;

    MyBehaviorTree behavior = null;

    float distanceTraveled = 0;
    bool goLeft = true;

    Health myHealth;

    // Use this for initialization
    void Start ()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        p = GameObject.Find("Player");
        goLeft = (Random.value > 0.5f);

        distanceTraveled = goLeft ? 3 : 0;

        myHealth = GetComponent<Health>();

        //Debug.Log("Go left: " + goLeft);
    }

    void BuildBehaviorTree()
    {
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
        //behavior = new MyBehaviorTree();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(behavior != null)
        {
            behavior.run();
        }
        else
        {
            BuildBehaviorTree();
        }

        isPlayerNear();
        //chasePlayer();

        if(isShotByPlayer)
        {
            chaseDownTimer();
        }
        //patrol();



        if (myHealth.getHealth() <= 0)
        {
            Debug.Log("Dead");
        }
    }

    public bool isPlayerNear()
    {
        float distAwayFromPlayer = Vector2.Distance(p.transform.position, this.transform.position);

        if(distAwayFromPlayer > 4.0f)
        {
            return false;
        }

        return true;
    }

    public void chasePlayer()
    {
        //Debug.Log("Move towards playerrrr");
        float step = speed * Time.deltaTime;
        this.transform.position = Vector2.MoveTowards(this.transform.position, p.transform.position, step);
    }

    //Enemy chases player until countdown reaches 0
    public void chaseDownTimer()
    {
        chaseTimer -= Time.deltaTime;
        //Debug.Log("Chasing enemy");

        if(chaseTimer <= 0)
        {
            //Debug.Log("No longer chasing");
            isShotByPlayer = false;
            chaseTimer = 3.0f;
        }
    }

    public void patrol()
    {
        /*
        float currPositionX = this.transform.position.x;
        float currPositionY = this.transform.position.y;
        float time = Mathf.PingPong(Time.time, 1);
        this.transform.position = new Vector2(Mathf.Lerp(currPositionX, currPositionY + 2, Mathf.PingPong(Time.time*0.5f, 0.5f)), this.transform.position.y);
        */
        float step = speed * Time.deltaTime;

        if (distanceTraveled > 0 && goLeft)
        {
            distanceTraveled -= step;
            this.transform.Translate(new Vector2(-step, 0));
            goLeft = true;
        }
        else
        {
            distanceTraveled += step;
            this.transform.Translate(new Vector2(step, 0));
            goLeft = false;
        }

        if (distanceTraveled > 3)
        {
            goLeft = true;
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        Health objectHealth = collision.gameObject.GetComponent<Health>();
        Vector3 moveDirection = _rigidbody.transform.position - collision.transform.position;
        _rigidbody.AddForce(moveDirection.normalized * 500f);
        if (collision.gameObject.tag == "Player")
        {



            Debug.Log("Hit player");
            // If a health component is present affect their health
            if (objectHealth != null)
            {
                objectHealth.subtractHealth(hitDamage);
            }
        }
    }

}

public class ShotByPlayerNode : MyTaskNode
{
    EnemyLevel1 e = null;

    public ShotByPlayerNode(EnemyLevel1 e)
    {
        this.e = e;
    }
    //Chases enemy
    public override bool run()
    {
        if(e.isShotByPlayer)
        {
            return true;
        }

        //Debug.Log("Enemy is not shot by player");
        return false;
    }
}

public class PatrolNode : MyTaskNode
{
    EnemyLevel1 e = null;

    public PatrolNode(EnemyLevel1 e)
    {
        this.e = e;
    }
    //Patrol
    public override bool run()
    {
        e.patrol();
        return true;
    }
}

public class ChasePlayerNode : MyTaskNode
{
    EnemyLevel1 e = null;

    public ChasePlayerNode(EnemyLevel1 e)
    {
        this.e = e;
    }
    //Chases enemy
    public override bool run()
    { 
        //Debug.Log("chasssing player");
        e.chasePlayer();
        return true;
    }
}

public class CheckIfEnemyIsNearNode : MyTaskNode
{
    EnemyLevel1 e = null;

    public CheckIfEnemyIsNearNode(EnemyLevel1 e)
    {
        this.e = e;
    }
    //Checks if enemy is near
    public override bool run()
    {
        if (e != null)
        {
            //Debug.Log("Is player near: " + e.isPlayerNear());
            return e.isPlayerNear();
        }
        else
        {
            //Debug.Log("Enemy is null");
            return false;
        }
    }
}

/*
public class MySelectorNode : MyTaskNode
{
    public override bool run()
    {
        foreach(MyTaskNode c in children)
        {
            if(c.run())
            {
                return true;
            }
        }

        return false;
    }
}

public class MySequenceNode : MyTaskNode
{
    public override bool run()
    {
        foreach (MyTaskNode c in children)
        {
            if (!c.run())
            {
                //Debug.Log("Sequence node returns false");
                return false;
            }
        }

        return true;
    }
}

public class MyTaskNode
{
    public List<MyTaskNode> children = new List<MyTaskNode>();

    public virtual bool run()
    {
        //Debug.Log("This should not print");
        return false;
    }

    public void add_kid(MyTaskNode node)
    {
        children.Add(node);
    }
}
*/