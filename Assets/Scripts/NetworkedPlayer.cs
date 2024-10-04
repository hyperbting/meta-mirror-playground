using System;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public partial class NetworkedPlayer : NetworkBehaviour
{
    [SerializeField]private bool _isLocalPlayer = false;
    
    #region Player Movement Sync
    public enum PlayerMovementType
    {
        Transform,
        DirectionOnly
    }
    public static readonly PlayerMovementType TypePlayerMovement = PlayerMovementType.Transform;
    #endregion
        
    #region Sync Transform
    [SerializeField] private Transform headTransform;
    [SerializeField] private Transform leftHandTransform;
    [SerializeField] private Transform rightHandTransform;
    
    [SerializeField] private GameObject headMesh;
    #endregion
    
    // #region conditional sync
    // #endregion
    
    private void Awake()
    {        
        // NO MIRROR info here
        Debug.LogWarning($"NetworkedPlayer{netId} Awake \nisLocalPlayer:{isLocalPlayer}, isClient:{isClient}\nisServer:{isServer}, isOwned:{isOwned}");
    }

    private void OnEnable()
    {
        // NO MIRROR info here
        Debug.LogWarning($"NetworkedPlayer{netId} OnEnable \nisLocalPlayer:{isLocalPlayer}, isClient:{isClient}\nisServer:{isServer}, isOwned:{isOwned}");
    }

    private void OnDisable()
    {
        Debug.LogWarning($"NetworkedPlayer{netId} OnDisable \nisLocalPlayer:{isLocalPlayer}, isClient:{isClient}\nisServer:{isServer}, isOwned:{isOwned}");
        
        if(_isLocalPlayer)
            ConfigManager.Instance.UnregisterMyPlayer(this);
    }

    private void OnDestroy()
    {
        Debug.LogWarning($"NetworkedPlayer{netId} OnDestroy \nisLocalPlayer:{isLocalPlayer}, isClient:{isClient}\nisServer:{isServer}, isOwned:{isOwned}");
    }

    void Start()
    {
        Debug.LogWarning($"NetworkedPlayer{netId} Start \nisLocalPlayer:{isLocalPlayer}, isClient:{isClient}\nisServer:{isServer}, isOwned:{isOwned}");
        if (isOwned)
        {
            _isLocalPlayer = true;
            ConfigManager.Instance.RegisterMyPlayer(this);
            
            //HideLocalIndicator();
        }

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        Start_Standalone();
#endif
    }
    
    void Update()
    {
        if (!isOwned) return;
        
        //TODO: and other optional skipping strategy
        
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        Update_Standalone();
#endif
    }

    void FixedUpdate()
    {
        if (!isOwned) return;
        
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        FixedUpdate_Standalone();
#endif
    }
    
    //public void CmdPlayerMoveDirectionSync() => CmdPlayerMoveDirectionSync(moveValue, rotateValue.y);
    
    [Command]
    public void CmdPlayerMoveDirectionSync(Vector2 moveDir, float rotDir)
    {
        // using Vector2 moveDirectionUpdated and Quaternion rotationUpdated
        MoveByDirection(moveDir);
        RotateByDirection(rotDir);
    }
    
    [Command]
    public void CmdPlayerBodyPartSync(
        Vector3 newHeadPos, Quaternion newHeadRot, 
        Vector3 newLeftLPos, Quaternion newLeftLRot, 
        Vector3 newRightLPos,Quaternion newRightLRot
        )
    {
        if (headTransform)
        {
            headTransform.position = newHeadPos;
            headTransform.rotation = newHeadRot;
        }

        if (leftHandTransform)
        {
            leftHandTransform.localPosition = newLeftLPos;
            leftHandTransform.localRotation = newLeftLRot;
        }
        
        if (rightHandTransform)
        {
            rightHandTransform.localPosition = newRightLPos;
            rightHandTransform.localRotation = newRightLRot;
        }
    }

    private void HideLocalIndicator()
    {
        headMesh?.SetActive(false);
    }
}
