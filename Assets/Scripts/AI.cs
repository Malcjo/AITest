using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    NavMeshAgent nav;
    public Vector3 newPos;
    public Vector3 origin;




    public Vector3 center;
    public float radius;
    private float objectCount;
    [SerializeField] private float moveTimer;
    [SerializeField] private float maxMoveTimer;

    [SerializeField] private Vector3 hitColliderTransform;





    void Start()
    {
        moveTimer = maxMoveTimer;

        origin = transform.position;
        nav = GetComponent<NavMeshAgent>();


    }


    void Update()
    {
        moveTimer -= Time.deltaTime;

        if (moveTimer <= 0)
        {
            MoveAI();
        }
    }

    void MoveAI()
    {
        nav.SetDestination(NewPosition());

        objectCount = 0;
        moveTimer = maxMoveTimer;
        Debug.Log("Move");
    }



    //Searching for object
    Vector3 NewPosition()
    {
        center = transform.position;
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        List<Item> items = new List<Item>();

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject.tag == "Item")
            {
                items.Add(new Item(hitColliders[i].transform.position));
                hitColliderTransform = (hitColliders[i].transform.position);
                objectCount++;
            }

        }
        var Dest = Random.Range(0, items.Count);
        if (hitColliders[Dest].gameObject.tag == "Item")
        {
            Debug.Log(hitColliders[Dest].gameObject.tag);
            Debug.Log("Found Item");
            return hitColliders[Dest].gameObject.transform.position;
        }
        else
        {
            Debug.Log("No Item Found");
            return new Vector3(Random.insideUnitSphere.x * (radius / 2), 0, Random.insideUnitSphere.z * (radius / 2));
        }


    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
