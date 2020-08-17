using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Objects
{
    public int count;
    public Transform localTransform;
    // Start is called before the first frame update

    public Objects(Transform newLocalTransform)
    {
        //count = newCount;
        localTransform = newLocalTransform;
    }
}
