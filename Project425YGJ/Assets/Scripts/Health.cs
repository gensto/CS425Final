using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Using a integer point health system
    [SerializeField]
    private int health = 4;



    public int getHealth()
    {
        return health;
    }

    /**
     * Set value of health
     */
    public void setHealth(int value)
    {
        health = value;
    }

    /**
     * Add value to health
     */
    public void addHealth(int value)
    {
        if ((health + value) <= 0)
        {
            health = 0;
        }
        else
        {
            health += value;
        }
    }

    /**
     * Subtract value to health
     */
    public void subtractHealth(int value)
    {
        if ( (health - value) <= 0 )
        {
            health = 0;
        }
        else {
            health -= value;
        }
    }
}
