using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject game;
    public float x;
    public float z;
    public float rotate;

    void Update()
    {
        x = NetworkManager.Instance.x;
        z = NetworkManager.Instance.z;
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(x,0,z);
    }
}
