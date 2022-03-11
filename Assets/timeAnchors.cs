using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class timeAnchors : MonoBehaviour
{
    private Vector3 anchorPosition;
    private Transform playerTransform;
    public Object timeAnchor;
    private Object currentAnchor;
    private bool anchorActive = false;
    private bool anchorTriggered = false;
    public float recallSpeed = 0.2f;
    public GameObject collisionCapsule;
    private CapsuleCollider playerCollision;
    private CharacterController playerController;
    public GameObject placeAnchorUI;
    public GameObject triggerAnchorUI;

    // Start is called before the first frame update
    void Start()
    {
        playerCollision = collisionCapsule.GetComponent<CapsuleCollider>();
        playerController = this.GetComponent<CharacterController>();
        playerTransform = this.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // reset level by pressing CTRL
        if (Input.GetKeyDown(KeyCode.L))
        {
            Scene scene = SceneManager.GetActiveScene(); 
            SceneManager.LoadScene(scene.name);
        }

        // place/activate time anchors by pressing R
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (anchorActive && !anchorTriggered)
            {
                anchorTriggered = true;
                playerCollision.enabled = false;
                playerController.enabled = false;
            }

            if (!anchorActive)
            {
                anchorPosition = new Vector3(playerTransform.position.x, playerTransform.position.y + 1.2f, playerTransform.position.z);
                currentAnchor = Instantiate(timeAnchor, anchorPosition, Quaternion.Euler(90, 0, 0));
                anchorActive = true;
                placeAnchorUI.SetActive(false);
                triggerAnchorUI.SetActive(true);
            }
        }

        // recall player to the time anchor when triggered
        if (anchorTriggered)
        {
            if (playerTransform.position.x < anchorPosition.x)
            {
                playerTransform.position += new Vector3(recallSpeed, 0f, 0f);
            }

            if (playerTransform.position.x > anchorPosition.x)
            {
                playerTransform.position -= new Vector3(recallSpeed, 0f, 0f);
            }

            if (playerTransform.position.y < anchorPosition.y)
            {
                playerTransform.position += new Vector3(0f, recallSpeed, 0f);
            }

            if (playerTransform.position.y > anchorPosition.y)
            {
                playerTransform.position -= new Vector3(0f, recallSpeed, 0f);
            }



            if (playerTransform.position.z < anchorPosition.z)
            {
                playerTransform.position += new Vector3(0f, 0f, recallSpeed);
            }

            if (playerTransform.position.z > anchorPosition.z)
            {
                playerTransform.position -= new Vector3(0f, 0f, recallSpeed);
            }

            // when player reaches anchor, delete it
            if (Vector3.Distance(playerTransform.position, anchorPosition) < 1.2f)
            {
                anchorTriggered = false;
                anchorActive = false;
                Destroy(currentAnchor);
                playerController.enabled = true;
                playerCollision.enabled = true;
                placeAnchorUI.SetActive(true);
                triggerAnchorUI.SetActive(false);
            }
        }

        
    }
}
