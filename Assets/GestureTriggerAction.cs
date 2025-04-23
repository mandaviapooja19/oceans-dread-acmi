using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureTriggerAction : MonoBehaviour
{
    //public List<GameObject> objects;

    // public GameObject parentCameraOffset;
    public GameObject mainCameraOffset;
    public Transform xrRig;

    private float vrRoll;
     public float moveSpeed = 5.0f;
     public float moveDistance = 10.0f; // Distance to move

    private Vector3 initialPosition; // Initial position of the object
    private Vector3 targetPosition;  // Target position after movement

    public void gestureAction1(string actionName){
        // Vector3 vrCameraForward = Camera.main.transform.forward;
        // vrCameraForward.Normalize();
        Debug.Log("gestureAction1 called");
        Vector3 gazeDirection = xrRig.forward;
        gazeDirection.Normalize();

        Debug.Log("Execute Action for "+ actionName+ " Gesture");
        if(actionName == "Move left"){

        }
        else if(actionName == "Move right"){
            
            // camerOffset.RotateAround(new Vector3(HeadTransform.position.x, 0, HeadTransform.position.z), Vector3.up, 45.0f);
            // mainCameraOffset.RotateAround(new Vector3(HeadTransform.position.x, 0, HeadTransform.position.z), Vector3.up, 45.0f);
            
        }
        else if(actionName == "LookLeft"){

        }
        else if(actionName == "LookRight"){
            Debug.Log("LookRight");
            // parentCameraOffset.transform.rotation = Quaternion.Euler(45, 0, 0);
        }
        else if(actionName == "LookUp"){
            
        }
        else if(actionName == "LookDown"){
            
        }
        else if(actionName == "MoveForward"){
            Debug.Log("MovingForward");
            //parentCameraOffset.transform.Translate(vrCameraForward * moveSpeed * Time.deltaTime, Space.World);
            // Calculate the distance to move this frame
            float movementThisFrame = moveSpeed * Time.deltaTime;

            /* // Move the object towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementThisFrame);


            // Check if the object has reached the target position
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                // Optionally, you can add logic here for when the movement is completed
                Debug.Log("Movement completed!");
            } */


            //Working
            // Update XR rig position based on the gaze direction
            /* Vector3 newPosition = xrRig.position + (vrCameraForward * moveSpeed * Time.deltaTime);
            xrRig.position = newPosition; */



            // mainCameraOffset.transform.position = mainCameraOffset.transform.position + mainCameraOffset.transform.forward; //Moving left by 0.1 every frame
            // mainCameraOffset.transform.position = mainCameraOffset.transform.position * 3 + mainCameraOffset.transform.forward * moveSpeed;
            transform.position += gazeDirection * moveSpeed * Time.deltaTime;
            Debug.Log("new transform.position "+ transform.position);
            // Debug.Log("Leaning left");
        }
        else if(actionName == "MoveBackward"){
            
        }

        // Look left, look right, look up, look down, move forward, move backward
    }

    // Start is called before the first frame update
    void Start()
    {
        // Store the initial position of the object
        initialPosition = transform.position;

        // Calculate the target position based on the move distance
        targetPosition = initialPosition + Camera.main.transform.forward * moveDistance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
