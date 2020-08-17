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

    List<Objects> objects = new List<Object>();


    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position;
        nav = GetComponent<NavMeshAgent>();
        StartCoroutine(SetDest());

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SetDest()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            MoveAI();
        }
    }
    void MoveAI()
    {
        NewSearchPosition();
        NewMovePosition();

        nav.SetDestination(origin += NewMovePosition());
        Debug.Log("Move");
        origin += NewMovePosition();
    }

    //gaining position to move it
    public Vector3 NewMovePosition()
    {
        SearchForObject();

        Vector3 tempNewPosSphere = (NewSearchPosition() + Random.insideUnitSphere) * 1;
        Vector3 newPos = new Vector3(tempNewPosSphere.x, 0, tempNewPosSphere.z);
        return newPos;
    }

    //creating sphere area to search for new position
    public Vector3 NewSearchPosition()
    {
        Vector3 TempSphere = Random.insideUnitSphere * 2;
        Vector3 newSphere = new Vector3(TempSphere.x, 0, TempSphere.z);
        return newSphere += TempSphere;
    }

    //Searching for object
    void Vector3 SearchForObject()
    {
        center = NewSearchPosition();
        radius = 1;
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].tag == "Object")
            {
                objectCount++;
                Debug.Log("Object" + objectCount);
                objects.Add(new Objects(hitColliders[i].transform));
                //objectCollider.Add(hitColliders[i].transform);

            }
            else
            {
                Debug.Log("not Object");
            }
            foreach (Collider hitCollider in hitColliders)
            {

            }
            if (i == hitColliders.Length)
            {

            }
        }
        objectCount = 0;
        var Dest = Random.Range(1, objects.Count);
        return objects
    }
}
