using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handProperties : MonoBehaviour {

    public float gripTilt = 62f;
    public SphereCollider gripMount;

    [HideInInspector]
    public Transform availableItem = null;

    void OnTriggerStay(Collider detectedItem)
    {
        if (detectedItem.tag == "Item")
        {
            availableItem = detectedItem.transform;
        }
        else
        {
            availableItem = null;
        }
    }

    void OnTriggerExit(Collider detectedItem)
    {
        availableItem = null;
    }
}
