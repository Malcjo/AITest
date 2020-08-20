using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PreyAI : MonoBehaviour
{
    NavMeshAgent nav;
    public Vector3 newPos;
    public Vector3 ItemPos;
    public Vector3 origin;
    public float hunger;
    public float maxHunger;
    public bool correctItem;

    public int randWait;

    public int dest;

    public GameObject Dest;
    public int numberOfDest;
    public bool move;

    public Vector3 center;
    public float radius;

    [SerializeField] private float moveTimer;
    [SerializeField] private float maxMoveTimer;

    int itemLayer;


    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        numberOfDest = 0;
        hunger = maxHunger;
        itemLayer = 1 << 8;
        move = true;

    }


    void Update()
    {
        hunger -= (1 * Time.deltaTime);
        if (move == true)
        {
            MoveAI();
        }

    }

    void MoveAI()
    {
        nav.SetDestination(NewPosition());
        move = false;
        newPos = NewPosition();
    }



    //Searching for object
    Vector3 NewPosition()
    {
        center = transform.position;
        Debug.Log("Looking For Items");
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, itemLayer);
        List<Item> items = new List<Item>();
        Debug.Log(hitColliders.Length);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if(hitColliders[i].tag == "Item")
            {
                items.Add(new Item(hitColliders[i].transform.position, 1));
            }
            else
            {
                items.Add(new Item(hitColliders[i].transform.position, 0));
            }
        }

        if (items.Count == 0)
        {
            Debug.Log("Nothing Found!");

            if (numberOfDest < 1)
            {
                Vector3 newDest = new Vector3(Random.insideUnitSphere.x * (radius * 1.25f), 0, Random.insideUnitSphere.z * (radius * 1.25f));
                Instantiate(Dest, newDest, Quaternion.identity);

                numberOfDest++;
                return newDest;


            }

            var tempDest = GameObject.FindGameObjectWithTag("Dest");
            return tempDest.gameObject.transform.position;
        }
        else
        {
            Debug.Log("Item Found!");
            

            dest = Random.Range(0, items.Count);
            if (hitColliders[dest].gameObject.tag == "Item")
            {
                correctItem = true;

            }
            else 
            { 
                correctItem = false;

            }
            return hitColliders[dest].gameObject.transform.position;
        }




    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void OnTriggerEnter(Collider other)
    {
        ItemPos = other.gameObject.transform.position;

        if (correctItem == true)
        {
            move = true;
            hunger += 10;
            Destroy(other.gameObject);
            Debug.Log("Correct Item!");
        }
        else if(correctItem == false)
        {
            move = true;
            Destroy(other.gameObject);
            Debug.Log("Wrong Item!");
        }
        if (other.tag == "Dest")
        {
            move = true;
            Destroy(other.gameObject);
            numberOfDest--;
            moveTimer = maxMoveTimer;

        }
    }

}
