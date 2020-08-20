using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public float maxTimer;
    public float timer;

    public GameObject[] food;
    // Start is called before the first frame update
    void Start()
    {
        timer = maxTimer;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= (1 * Time.deltaTime);
        if(timer <= 0)
        {
            timer = maxTimer;
            Vector3 randomPos = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            Instantiate(food[Random.Range(0, 2)], randomPos, Quaternion.identity);
        }
    }
}
