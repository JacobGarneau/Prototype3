using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation : MonoBehaviour
{
    private Transform objectTransform;
    public float rotationSpeed = 0.04f;

    // Start is called before the first frame update
    void Start()
    {
        objectTransform = this.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        objectTransform.Rotate(0f, rotationSpeed, 0f);
    }
}
