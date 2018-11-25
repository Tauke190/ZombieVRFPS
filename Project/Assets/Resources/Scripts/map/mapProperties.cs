using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapProperties : MonoBehaviour {

    public Transform objectPool;

    public static Transform objectsPool;

    void Awake()
    {
        objectsPool = objectPool;
    }
}
