using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;// 1. The Input System "using" statement

public partial class NetworkedPlayer
{
    // 2. These variables are to hold the Action references
    InputAction moveAction;
    InputAction lookAction;
    
    [Header("Only used in DirectionSync")]
    [SerializeField] private Vector2 moveValue;
    [SerializeField] private Vector2 rotateValue;
    //[SerializeField] private Vector3 scaleValue;
    
    private void Start_Standalone()
    {
        // 3. Find the references to the "Move" and "Jump" actions
        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");
    }
    
    private void Update_Standalone()
    {
        // 4. Read the "Move" action value, which is a 2D vector
        moveValue = moveAction.ReadValue<Vector2>();
        moveValue.Normalize();
        
        // 4-2. Read the "Look" action value, which is a 2D vector
        rotateValue = lookAction.ReadValue<Vector2>();
        
        switch(TypePlayerMovement)
        {
            case PlayerMovementType.Transform:
                // move locally and directly
                MoveByDirection(moveValue);
                RotateByDirection(rotateValue.y);
                break;
            case PlayerMovementType.DirectionOnly:
                CmdPlayerMoveDirectionSync(moveValue, rotateValue.y);
                break;
            default:
                break;
        }

        //move certain body parts to VRrRig's values
        var ovs = ConfigManager.Instance.ovrSyncer;
        if (!ovs)
            return;
        
        MoveHead(ovs.headPosition, ovs.headRotation);
        //MoveTorso(ovs.torsoPosition, ovs.torsoRotation);
        MoveLeftHand(ovs.leftHandLocPosition, ovs.leftHandLocRotation);
        MoveRightHand(ovs.rightHandLocPosition, ovs.rightHandLocRotation);
    }

    private void FixedUpdate_Standalone()
    {
    } 

    #region
    private void MoveByDirection(Vector2 dir)
    {
        dir *= 0.01f;
        //Debug.Log(moveValue);
        
        // your movement code here
        transform.position += transform.forward * dir.x + transform.right * dir.y;
    }

    private void RotateByDirection(float dir)
    {
        dir *= 0.01f;
        transform.Rotate(Vector3.up, dir);
    }

    private void MoveHead(Vector3 newHeadPos, Quaternion newHeadRot)
    {
        if (!headTransform) return;
        headTransform.position = newHeadPos;
        headTransform.rotation = newHeadRot;
    }

    private void MoveTorso(Vector3 newPos, Quaternion newRot)
    {
        if (!torsoTransform) return;
        torsoTransform.position = newPos;
        torsoTransform.rotation = newRot;
    }
    
    private void MoveLeftHand(Vector3 newPos, Quaternion newRot)
    {
        if (!leftHandTransform) return;
        leftHandTransform.position = newPos;
        leftHandTransform.rotation = newRot;
    }
    
    private void MoveRightHand(Vector3 newPos, Quaternion newRot)
    {
        if (!rightHandTransform) return;
        rightHandTransform.position = newPos;
        rightHandTransform.rotation = newRot;
    }
    #endregion
}
