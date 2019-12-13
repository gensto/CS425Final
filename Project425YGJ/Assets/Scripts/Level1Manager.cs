using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Manager : MonoBehaviour
{
    GameObject enemies;
    GameObject key;
    GameObject door;
    bool levelFinished = false;
    public bool unlockDoor = false;
    public bool obtainedKey = false;
    public bool enteredDoor = false;
    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.Find("Enemies");
        key = GameObject.Find("Key");
        door = GameObject.Find("Door");
        door.SetActive(false);
        key.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(enemies.transform.childCount == 0 && !levelFinished)
        {
            key.SetActive(true);
            levelFinished = true;
            Debug.Log("Finished level");
        }

        if(enteredDoor)
        {
            enteredDoor = false;
            //load next scene (level)
            Debug.Log("Loading next scene");
        }

    }


}
