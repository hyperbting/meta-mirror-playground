using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;// 1. The Input System "using" statement

public partial class NetworkedPlayer
{
    // private Transform eLeft;
    // private Transform eRight;
    
    // 2. These variables are to hold the Action references
    InputAction moveAction;

    [SerializeField] private Transform myVrRig;
    private void Start_Standalone()
    {
        // 3. Find the references to the "Move" and "Jump" actions
        moveAction = InputSystem.actions.FindAction("Move");
    }
    private void Update_Standalone()
    {
        // 4. Read the "Move" action value, which is a 2D vector
        var moveValue = moveAction.ReadValue<Vector2>();
        moveValue.Normalize();
        moveValue *= 0.01f;
        //Debug.Log(moveValue);
        
        // your movement code here
        transform.position += transform.forward*moveValue.x + transform.right*moveValue.y;
        
        // if (!eLeft)
        // {
        //     var emptyGO = new GameObject();
        //     eLeft = emptyGO.transform;
        // }
        //
        // if (!eRight)
        // {
        //     var emptyGO = new GameObject();
        //     eRight = emptyGO.transform;
        // }
        //
        // var preLeftPos = eLeft.position;
        // preLeftPos.y = (Mathf.FloorToInt(Time.time) % 3) / 10f;
        // eLeft.position = preLeftPos;
        //
        // var preRightPos = eRight.position;
        // preRightPos.x = (Mathf.FloorToInt(Time.time) % 3) / 10f;
        // eRight.position = preRightPos;
    }

    private void FixedUpdate_Standalone()
    {
        if (!myVrRig)
            return;
        
        myVrRig.position = transform.position;
        myVrRig.rotation = transform.rotation;
        myVrRig.localScale = transform.localScale;
    }
}
