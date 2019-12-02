﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Using a integer point health system
    [SerializeField]
    private int health = 4;


    // When hit flash variables
    public float flashTime;
    Color originalColor;
    public SpriteRenderer mRenderer;

    private void Start()
    {
        mRenderer = GetComponent<SpriteRenderer>();
        originalColor = mRenderer.color;
    }

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
        health += value;
        FlashGreen();
    }

    /**
     * Subtract value to health
     */
    public void subtractHealth(int value)
    {
        if ( (health - value) <= 0 )
        {
            health = 0;
            invokeDeath();
        }
        else {
            health -= value;
            invokeHit();
        }
    }

    void invokeDeath()
    {
        Destroy(gameObject);
    }

    void invokeHit()
    {
        FlashRed();
    }


    void FlashRed()
    {
        mRenderer.color = Color.red;
        Invoke("ResetColor", flashTime);
    }

    void FlashGreen()
    {
        mRenderer.color = Color.green;
        Invoke("ResetColor", flashTime);
    }

    void ResetColor()
    {
        mRenderer.color = originalColor;
    }
}
