using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour {

    private float movementSpeed = .2f;
    private float rotationSpeed = .02f;
    private float mouseSensitivity = 1f;
    private float forwardSpeed = 0f;
    private float sideSpeed = 0f;
    private float clockwiseSpeed = 0f;
    private float upDownSpeed = 0f;
    private float upDownRange = 20.0f;
    private float leftRightRange = 20.0f;
    private float verticalRotation = 0;
    private float horizontalRotation = 0;
    private float verticalVelocity = 0;
	
	CharacterController characterController;
    Vector3 oldSpeed;
	
	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();
        oldSpeed = new Vector3(0,0,0);
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;
	}

    

    // Update is called once per frame
    void Update () {
        // Rotation


        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
		verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
		
        horizontalRotation += Input.GetAxis("Mouse X") * mouseSensitivity;
        horizontalRotation = Mathf.Clamp(horizontalRotation, -leftRightRange, leftRightRange);

        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0);

        // Movement

        if (Input.GetKeyDown("return")){
            forwardSpeed = 0;
            sideSpeed = 0;
            upDownSpeed = 0;
            clockwiseSpeed = 0;
        }

        else {
            forwardSpeed += Input.GetAxis("Forward") * movementSpeed;
            sideSpeed += Input.GetAxis("Horizontal") * rotationSpeed;
            upDownSpeed -= Input.GetAxis("Vertical") * rotationSpeed;
            clockwiseSpeed += Input.GetAxis("Clockwise") * rotationSpeed;
        }


        transform.Rotate(upDownSpeed * Time.deltaTime * 45, sideSpeed * Time.deltaTime * 45, clockwiseSpeed * Time.deltaTime * 45);
        

        Vector3 speed = new Vector3( 0, verticalVelocity, forwardSpeed );
        
		

		speed = Camera.main.transform.rotation * speed;
        speed += oldSpeed;
        oldSpeed = speed;
        forwardSpeed = 0;
        
        

        characterController.Move( speed * Time.deltaTime );
	}
}
