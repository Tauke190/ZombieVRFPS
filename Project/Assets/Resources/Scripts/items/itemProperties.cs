using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemProperties : MonoBehaviour 
{
    public bool itemEquipped;
    public SphereCollider gripMount;
    public Rigidbody physicsBody;

    void Update()
    {
        if (itemEquipped)
        {
            physicsBody.isKinematic = true;
        }
        else
        {
            transform.parent = mapProperties.objectsPool;
            physicsBody.isKinematic = false;
        }
    }
}
