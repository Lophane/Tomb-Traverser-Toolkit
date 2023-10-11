using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;
    private bool startGame = false;

    float xRotation;
    float yRotation;

    private void Start()
    {        
        Invoke("StartGame", 8f);

    }

    private void Update()
    {
        if(startGame == true)
        {
            RotateCamera();
        }        
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    void StartGame()
    {
        startGame = true;
    }

}