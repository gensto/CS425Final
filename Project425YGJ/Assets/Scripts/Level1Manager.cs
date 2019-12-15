using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Manager : MonoBehaviour
{
    GameObject enemies;
    GameObject key;
    GameObject door;
    GameObject music;
    bool levelFinished = false;
    public bool unlockDoor = false;
    public bool obtainedKey = false;
    public bool enteredDoor = false;
    

    public AudioSource myVictoryFx;
    public AudioClip victory;
    public AudioSource myKeyFx;
    public AudioClip keyPickUp;
    public AudioSource myEnemiesClearedFx;
    public AudioClip enemiesCleared;
    

    public void EnemiesClearedSound()
    {
        myEnemiesClearedFx.volume = 0.2f;
        myEnemiesClearedFx.PlayOneShot(enemiesCleared);
    }

    public void KeyPickUpSound()
    {
        //myFx.volume = 1.0f;
        myKeyFx.PlayOneShot(keyPickUp);
    }

    public void VictorySound()
    {
        //myFx.volume = 0.05f;
        myVictoryFx.PlayOneShot(victory);
    }
    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.Find("Enemies");
        key = GameObject.Find("Key");
        door = GameObject.Find("Door");
        music = GameObject.Find("BackgroundMusic");
        //door.SetActive(false);
        key.SetActive(false);
    }
    /*
    int count = enemies.transform.childCount;
    public AudioSource myDeathFx;
    public AudioClip death;

    public void DeathSound()
    {
        //myDeathFx.volume = 0.2f;
        myDeathFx.PlayOneShot(death);
    }
    */
    // Update is called once per frame
    void Update()
    {
        /*
        if (enemies.transform.childCount < count)
        {
            //Play grunt sound effect
            count = enemies.transform.childCount;
        }
        */
        if(enemies.transform.childCount == 0 && !levelFinished)
        {
            EnemiesClearedSound();
            key.SetActive(true);
            levelFinished = true;
            Debug.Log("Finished level");
        }

        if(enteredDoor)
        {
            music.SetActive(false);
            enteredDoor = false;
            VictorySound();
            //load next scene (level)
            Debug.Log("Loading next scene");
        }

        if (obtainedKey)
        {
            obtainedKey = false;
            KeyPickUpSound();
            //Debug.Log("Loading next scene");
        }
    }


}
