using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item
{
    public int ID;
    public Vector3 localTransform;
    public int priority;
    // Start is called before the first frame update

    public Item(Vector3 newLocalTransform, int newPriority)
    {
        
        localTransform = newLocalTransform;
        priority = newPriority;

    }
}
