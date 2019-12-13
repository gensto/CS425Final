using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBehaviorTree
{

    MyTaskNode root;

    public MyBehaviorTree(MyTaskNode root)
    {
        Debug.Log("sup hoess");
        this.root = root;
    }

    public bool run()
    {
        if(root == null)
        {
            return false;
        }
        else
        {
            return root.run();
        }
    }
}

public class MySelectorNode : MyTaskNode
{
    public override bool run()
    {
        foreach (MyTaskNode c in children)
        {
            if (c.run())
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
