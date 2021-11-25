using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    [SerializeField] private Rigidbody2D currentBallRigidbody;
    [SerializeField] private SpringJoint2D currentBallSpringJoint;

    [SerializeField] private float detachDelay = 0.5f;

    private Camera mainCamera;
    private bool isDragging;
    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if(currentBallRigidbody == null){ return; }

        if(!Touchscreen.current.primaryTouch.press.isPressed) 
        { 
            if(isDragging)
            {
                LaunchBall();
            }

            isDragging = false;

            return;
        }

        isDragging = true;

        currentBallRigidbody.isKinematic = true;

        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();

        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);

        currentBallRigidbody.position = worldPosition;
    }

    void LaunchBall()
    {
        currentBallRigidbody.isKinematic = false; 
        currentBallRigidbody = null;
        
        Invoke(nameof(DetachBall), detachDelay);
    }

    void DetachBall()
    {
        currentBallSpringJoint.enabled = false;
        currentBallSpringJoint = null;
    }
}
