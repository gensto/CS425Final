using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevel2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
