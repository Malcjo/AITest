using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    NavMeshAgent nav;
    public Vector3 newPos;
    public Vector3 TempPosView;
    public Vector3 origin;
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
    public Vector3 TempPos()
    {
        Vector3 tempNewPos = Random.insideUnitSphere * 1;
        Vector3 newPos = new Vector3(tempNewPos.x, 0, tempNewPos.z);
        return newPos;

    }
    IEnumerator SetDest()
    {
        while (true)
        {
            nav.SetDestination(origin += TempPos());
            Debug.Log("Move");
            yield return new WaitForSeconds(2);
            TempPos();
            origin += TempPos();
            TempPosView = TempPos();
        }


    }
}
