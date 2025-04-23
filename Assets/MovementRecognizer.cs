using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using PDollarGestureRecognizer;
using System.IO;
using UnityEngine.Events;

public class MovementRecognizer : MonoBehaviour
{

    public XRNode inputSource;
    public UnityEngine.XR.Interaction.Toolkit.InputHelpers.Button inputButton;
    public float inputThreshold = 0.1f;
    public Transform movementSource;

    public float newPositionThresholdDistance = 0.05f; // 5cm
    public GameObject debugCubePrefab;
    public bool creationMode = true;
    public string newGestureName; 

    public float recognitionThreshold = 0.9f;
    
    [System.Serializable]
    public class UnityStringEvent : UnityEvent<string> {}
    public UnityStringEvent OnRecognized;

    private List<Gesture> trainingSet = new List<Gesture>();
    private bool isMoving = false; 
    private List<Vector3> positionList = new List<Vector3>();

    // Start is called before the first frame update
    void Start() 
    {
        string[] gestureFiles = Directory.GetFiles(Application.persistentDataPath, "*.xml");
        foreach(var item in gestureFiles){
            trainingSet.Add(GestureIO.ReadGestureFromFile(item));
        }
    }

    // Update is called once per frame
    void Update()
    {
      UnityEngine.XR.Interaction.Toolkit.InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(inputSource), inputButton, out bool isPressed, inputThreshold);

      //Start the movement
      if(!isMoving & isPressed){
        StartMovement();
      } 
      // Ending the movement
      else if(isMoving & !isPressed){
        EndMovement();
      } 
      // Update the movement
      else if(isMoving & isPressed){
        UpdateMovement();
      } 
    }

    void StartMovement(){
        // Debug.Log("StartMovement"); 
        isMoving = true;
        positionList.Clear();
        positionList.Add(movementSource.position);
        
        if(debugCubePrefab)
            Destroy(Instantiate(debugCubePrefab, movementSource.position, Quaternion.identity), 3);
    }

    void EndMovement(){
        // Debug.Log("EndMovement");
        isMoving = false;

        //Create the gesture from the position list
        Point[] pointArray = new Point[positionList.Count];

        // Debug.Log("positionList.Count -> "+ positionList.Count);
        for(int i = 0; i < positionList.Count; i++){
            //pointArray[i] = positionList[i]; // PositionList is Vector3 but for pointArray we only need x and y values
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(positionList[i]); // position of screen with respect to position of movement with hand
            pointArray[i] = new Point(screenPoint.x, screenPoint.y, 0);
        }

        Gesture newGesture = new Gesture(pointArray);
        
        //To create new gesture
        if(creationMode){
            newGesture.Name = newGestureName;
            trainingSet.Add(newGesture);

            //To save the gestures in the system
            //Application.persistentDataPath is used when storing data in file even when the game is running 
            string fileName = Application.persistentDataPath + "/" + newGestureName + ".xml";
            GestureIO.WriteGesture(pointArray, newGestureName, fileName);
        }
        //To recognise the gesture
        else{
            Result result = PointCloudRecognizer.Classify(newGesture, trainingSet.ToArray());
            Debug.Log("result --> "+ result.GestureClass + result.Score);
            if(result.Score > recognitionThreshold){
                OnRecognized.Invoke(result.GestureClass);
            }
        }
    }

    void UpdateMovement(){
        // Debug.Log("UpdateMovement");
        Vector3 lastPosition = positionList[positionList.Count - 1];
        
        if(Vector3.Distance(movementSource.position, lastPosition) > newPositionThresholdDistance){
            positionList.Add(movementSource.position);
            if(debugCubePrefab){
                Destroy(Instantiate(debugCubePrefab, movementSource.position, Quaternion.identity), 3);
            }
        }
        
    }
}
