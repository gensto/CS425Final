using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    MyBehaviorTree behavior = null;

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

        return true;
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

        return true;
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

        return true;
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

        return true;
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

        return true;
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

        return true;
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

        return true;
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

        return true;
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

        return true;
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

        return true;
    }
}
