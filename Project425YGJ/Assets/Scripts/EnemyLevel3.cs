using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevel3 : MonoBehaviour
{
    [SerializeField]
    GameObject bombPrefab;

    public AnimationCurve myCurve;

    void Update()
    {
        transform.position = new Vector3(transform.position.x, myCurve.Evaluate((Time.time % myCurve.length)), transform.position.z);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBomb();
        }
    }



    void SpawnBomb()
    {
        GameObject bomb = Instantiate(bombPrefab, gameObject.transform.position + (Vector3.down * Random.Range(1,3)), gameObject.transform.rotation);
    }
}
