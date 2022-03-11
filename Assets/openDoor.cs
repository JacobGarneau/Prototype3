using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDoor : MonoBehaviour
{
    public GameObject door;
    public bool maintain = false;
    private Transform doorTransform;
    private BoxCollider pickupCollider;
    private MeshRenderer pickupRenderer;
    public float doorGrowSpeed = 0.1f;
    public float doorCloseFactor = 20f;
    private float doorTargetSize = 18f;

    // Start is called before the first frame update
    void Start()
    {
        doorTransform = door.GetComponent<Transform>();
        pickupCollider = this.GetComponent<BoxCollider>();
        pickupRenderer = this.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // adjust triggered door scale
        if (doorTransform.localScale.y < doorTargetSize)
        {
            doorTransform.localScale += new Vector3(0, doorGrowSpeed / doorCloseFactor, 0);
        }

        if (doorTransform.localScale.y > doorTargetSize)
        {
            doorTransform.localScale -= new Vector3(0, doorGrowSpeed, 0);
        }

        if (Input.GetKeyDown(KeyCode.R) && maintain)
        {
            pickupRenderer.enabled = true;
            doorTargetSize = 18f;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!maintain)
        {
            pickupCollider.enabled = false;
        }
        pickupRenderer.enabled = false;
        doorTargetSize = 6f;
    }

    private void OnTriggerExit(Collider collision)
    {
        if (maintain)
        {
            pickupRenderer.enabled = true;
            doorTargetSize = 18f;
        }
    }
}
