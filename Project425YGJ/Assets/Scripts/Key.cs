using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    GameObject level1Manager;
    [SerializeField]
    GameObject door;
    // Start is called before the first frame update
    void Start()
    {
        level1Manager = GameObject.Find("Level1Manager");
        door = GameObject.Find("Door");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //player picks up key
        if (collision.gameObject.tag == "Player")
        {
            level1Manager.GetComponent<Level1Manager>().obtainedKey = true;
            level1Manager.GetComponent<Level1Manager>().unlockDoor = true;
            door = GameObject.Find("Door");
            door.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
            Debug.Log("Obtained the key");
        }
    }
}
