using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public partial class NetworkedPlayer : NetworkBehaviour
{
    public GameObject localPlayerInputGameObject;
    
    #region
    // [SyncVar(hook = nameof(OnChangeLeftHandTransform))]
    // public Transform leftHand;
    // [SyncVar(hook = nameof(OnChangeRightHandTransform))]
    // public Transform rightHand;
    // void OnChangeLeftHandTransform(Transform oldHandTransform, Transform newHandTransform)
    // {
    // //     leftHandGameObject.transform.position = newHandTransform.position;
    // //     leftHandGameObject.transform.rotation = newHandTransform.rotation;
    // //     leftHandGameObject.transform.localScale = newHandTransform.localScale;
    // }
    //
    // void OnChangeRightHandTransform(Transform oldHandTransform, Transform newHandTransform)
    // {
    //     // rightHandGameObject.transform.position = newHandTransform.position;
    //     // rightHandGameObject.transform.rotation = newHandTransform.rotation;
    //     // rightHandGameObject.transform.localScale = newHandTransform.localScale;
    // }
    #endregion
    
    #region conditional sync
    [SyncVar] public Vector3 leftHandPos;
    [SyncVar] public Quaternion leftHandRot;
    [SyncVar] public Vector3 rightHandPos;
    [SyncVar] public Quaternion rightHandRot;
    #endregion
    
    void Start()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        Start_Standalone();
#endif
    }
    
    void Update()
    {
        if (!isLocalPlayer) return;
        
        //TODO: and other optional skipping strategy
        
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        Update_Standalone();
        //CmdUpdateHandsTransform(eLeft, eRight);
#endif
        
        // //Get left/ right hand Transform from ?
        // CmdUpdateHandsTransform();
    }

    void FixedUpdate()
    {
        if (!isLocalPlayer) return;
        
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        FixedUpdate_Standalone();
#endif
    }
    
    [Command]
    void CmdUpdateHandsTransform(Transform left, Transform right)
    {
        if (left)
        {
            leftHandPos = left.localPosition;
            leftHandRot = left.localRotation;
        }

        if (right)
        {
            rightHandPos = right.localPosition;
            rightHandRot = right.localRotation;
        }
    }
}
