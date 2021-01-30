using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotator : MonoBehaviour
{
    public float angularSpeed = 1.0f;   // degrees per second
    public Vector3 axis = new Vector3(0.0f, 0.0f, 1.0f);


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(axis, Time.deltaTime * angularSpeed);
    }
}
