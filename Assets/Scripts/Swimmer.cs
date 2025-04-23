using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Swimmer : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] float swimForce = 2f;
    [SerializeField] float dragForce = 1f;
    [SerializeField] float minForce;
    [SerializeField] float minTimeBetweenStrokes;

    [Header("References")]
    [SerializeField] InputActionReference leftControllerSwimReference;
    [SerializeField] InputActionReference leftControllerVelocity;
    [SerializeField] InputActionReference rightControllerSwimReference;
    [SerializeField] InputActionReference rightControllerVelocity;
    [SerializeField] Transform trackingReference;


    Rigidbody _rigidBody;
    float _cooldownTimer;

    void Awake(){
        _rigidBody = GetComponent<Rigidbody>();
        _rigidBody.useGravity = false;
        _rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void FixedUpdate(){
        _cooldownTimer += Time.fixedDeltaTime;
        if(_cooldownTimer > minTimeBetweenStrokes && leftControllerSwimReference.action.IsPressed() && rightControllerSwimReference.action.IsPressed()){
            var leftHandVelocity = leftControllerVelocity.action.ReadValue<Vector3>();
            var rightHandVelocity = rightControllerVelocity.action.ReadValue<Vector3>();
            Vector3 localVelocity = leftHandVelocity + rightHandVelocity;
            localVelocity *= -1;

            if(localVelocity.sqrMagnitude > minForce * minForce){
                Vector3 worldVelocity = trackingReference.TransformDirection(localVelocity);
                _rigidBody.AddForce(worldVelocity * swimForce, ForceMode.Acceleration);
                _cooldownTimer = 0f;
            }
        }

        if(_rigidBody.velocity.sqrMagnitude > 0.01f){
            _rigidBody.AddForce(-_rigidBody.velocity * dragForce, ForceMode.Acceleration);
        }
    }


    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
