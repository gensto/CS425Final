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
