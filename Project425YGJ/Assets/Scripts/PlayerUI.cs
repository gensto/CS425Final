using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    GameObject heartPrefab;
    List<GameObject> hearts = new List<GameObject>();
    Health playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        GameObject _heart;
        playerHealth = GameObject.Find("Player").GetComponent<Health>();
        for (int i = 0; i < playerHealth.getMaxHealth(); i++)
        {
            Debug.Log("sdf");
            _heart = Instantiate(heartPrefab);
            _heart.transform.SetParent(transform);
            hearts.Add(_heart);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = playerHealth.getHealth(); i < playerHealth.getMaxHealth(); i++)
        {
            hearts[i].SetActive(false);
        }
        for (int i = 0; i < playerHealth.getHealth(); i++)
        {
            hearts[i].SetActive(true);
        }
    }
}
