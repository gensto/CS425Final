using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Manager : MonoBehaviour
{
    GameObject enemies;
    GameObject key;
    GameObject door;
    GameObject closedDoor;
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
        myEnemiesClearedFx.volume = 0.5f;
        myEnemiesClearedFx.PlayOneShot(enemiesCleared);
    }

    public void KeyPickUpSound()
    {
        myKeyFx.volume = 0.8f;
        myKeyFx.PlayOneShot(keyPickUp);
    }

    public void VictorySound()
    {
        myVictoryFx.volume = 0.6f;
        myVictoryFx.PlayOneShot(victory);
    }
    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.Find("Enemies");
        key = GameObject.Find("Key");
        closedDoor = GameObject.Find("ClosedDoor");
        door = GameObject.Find("Door");
        music = GameObject.Find("BackgroundMusic");
        key.SetActive(false);
    }
    
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
            StartCoroutine(LoadNextScene());
        }

        if (obtainedKey)
        {
            obtainedKey = false;
            closedDoor.SetActive(false);
            KeyPickUpSound();
            //Debug.Log("Loading next scene");
        }
    }

    IEnumerator LoadNextScene()
    {

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

}
