using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    GameObject p = null;

    // Start is called before the first frame update
    void Start()
    {
        p = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(p == null)
        {
            Debug.Log("Player is dead");
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
