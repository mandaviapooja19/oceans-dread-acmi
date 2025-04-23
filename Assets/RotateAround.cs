using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{

    public GameObject cameraObj;
    private float cameraRotationAngle;

    // Start is called before the first frame update
    void Start()
    {
        //To get world mouse position
        // return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));

        //mousePositionOffset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);   

        // transform.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        cameraRotationAngle = Camera.main.transform.eulerAngles.z;

        /* if(cameraRotationAngle >= 20 && cameraRotationAngle <=80){
            // cameraObj.transform.position = transform.Translate(0.1f, 0f, 0f); //Moving left by 0.1 every frame
            cameraObj.transform.position = cameraObj.transform.position + cameraObj.transform.forward; //Moving left by 0.1 every frame
            Debug.Log("Leaning left");
        } */
    }
}
