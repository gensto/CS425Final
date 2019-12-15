using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject level1Manager;

    void Start()
    {
        level1Manager = GameObject.Find("Level1Manager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            level1Manager.GetComponent<Level1Manager>().enteredDoor = true;
            Debug.Log("Entered door");
        }
    }
}
