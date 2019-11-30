using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevel1 : MonoBehaviour
{
    GameObject p = null;
    float speed = 1f;

	// Use this for initialization
	void Start ()
    {
        p = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update ()
    {
        isPlayerNear();
        chasePlayer();
	}

    public bool isPlayerNear()
    {
        float distAwayFromPlayer = Vector2.Distance(p.transform.position, this.transform.position);

        if(distAwayFromPlayer > 3.0f)
        {
            return false;
        }

        return true;
    }

    public void chasePlayer()
    {
        float step = speed * Time.deltaTime;
        this.transform.position = Vector2.MoveTowards(this.transform.position, p.transform.position, step);
    }

}

public class chaseEnemyNode : MyTaskNode
{
    EnemyLevel1 e = null;

    public chaseEnemyNode(EnemyLevel1 e)
    {
        this.e = e;
    }
    //Chases enemy
    public override bool run()
    {
        e.chasePlayer();
        return true;
    }
}

public class checkIfEnemyIsNearNode : MyTaskNode
{
    EnemyLevel1 e = null;

    public checkIfEnemyIsNearNode(EnemyLevel1 e)
    {
        this.e = e;
    }
    //Checks if enemy is near
    public override bool run()
    {
        if (e != null)
        {
            return e.isPlayerNear();
        }
        else
        {
            Debug.Log("Enemy is null");
            return false;
        }
    }
}

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
        Debug.Log("This should not print");
        return false;
    }

    void add_kid(MyTaskNode node)
    {
        children.Add(node);
    }
}