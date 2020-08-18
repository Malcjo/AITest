using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item
{
    public int ID;
    public Vector3 localTransform;
    // Start is called before the first frame update

    public Item(Vector3 newLocalTransform)
    {
        //count = newCount;
        
        localTransform = newLocalTransform;
    }
}
