using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.CoreUtils;

public class PlayerMover : MonoBehaviour
{
    public Animator playerAnimator;
    private LosingHandler ragdollComponent;
    private Touch touch;
    private float speedModifier;

    public float moveSpeed = 10;
    public float strafeSpeed = 3;  
    private void Start()
    {
        ragdollComponent = GetComponentInChildren<LosingHandler>();
        speedModifier = 0.01f;
    }
    private void Update()   
    { 
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.World); 
        if(Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Moved) 
            {
                transform.position = new Vector3(
                   Mathf.Clamp(transform.position.x, Boundary.leftSide, Boundary.rightSide) + touch.deltaPosition.x * speedModifier,
                   transform.position.y,
                   transform.position.z );
            }
        } 
        if (transform.GetChild(1).transform.childCount == 0)
        { 
            ragdollComponent.Losing(true); 
        }  
    } 
    public void Jump(int jumpHigh)
    {
        playerAnimator.SetTrigger("Jump");
        Vector3 stickmanPos = transform.GetChild(0).gameObject.transform.position;
        stickmanPos.y += jumpHigh;
        transform.GetChild(0).gameObject.transform.position = stickmanPos;
    }
    public void Stop()
    {
        moveSpeed = 0;
    }
}
