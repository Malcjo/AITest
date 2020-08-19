using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PreyAI : MonoBehaviour
{
    NavMeshAgent nav;
    public Vector3 newPos;
    public Vector3 ItemPos;
    public Vector3 origin;

    public bool correctItem;

    public Vector3 center;
    public float radius;

    [SerializeField] private float moveTimer;
    [SerializeField] private float maxMoveTimer;

    int itemLayer;


    void Start()
    {
        itemLayer = 1 << 8;
        moveTimer = maxMoveTimer;

        nav = GetComponent<NavMeshAgent>();


    }


    void Update()
    {
        moveTimer -= Time.deltaTime;

        if (moveTimer <= 0)
        {
            MoveAI();
            moveTimer = maxMoveTimer;
        }
    }

    void MoveAI()
    {
        nav.SetDestination(NewPosition());

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
            items.Add(new Item(hitColliders[i].transform.position));

        }
        if (items.Count == 0)
        {
            Debug.Log("Nothing Found!");
            return new Vector3(Random.insideUnitSphere.x * radius, 0, Random.insideUnitSphere.z * radius);
        }
        else
        {
            Debug.Log("Item Found!");
            var Dest = Random.Range(0, items.Count);
            if (hitColliders[Dest].gameObject.tag == "Item")
            {
                correctItem = true;
            }
            else { correctItem = false; }
            return hitColliders[Dest].gameObject.transform.position;
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
            Destroy(other.gameObject);
            Debug.Log("Correct Item!");
        }
        else if(correctItem == false)
        {
            Destroy(other.gameObject);
            Debug.Log("Wrong Item!");
        }
    }

}
