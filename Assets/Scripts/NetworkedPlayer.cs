using System;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public partial class NetworkedPlayer : NetworkBehaviour
{
    public GameObject localPlayerInputGameObject;

    [SerializeField]private bool _isLocalPlayer = false;
    
    #region Sync Transform
    [SerializeField] private Transform headTransform;
    [SerializeField] private Transform leftHandTransform;
    [SerializeField] private Transform rightHandTransform;
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
            
            HideLocalIndicator();
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
    
    [Command]
    public void CmdPlayerBodyPartSync(
        Vector3 newHeadPos, Quaternion newHeadRot, 
        Vector3 newLeftPos, Quaternion newLeftRot, 
        Vector3 newRightPos,Quaternion newRightRot
        )
    {
        if (headTransform)
        {
            headTransform.position = newHeadPos;
            headTransform.rotation = newHeadRot;
        }

        if (leftHandTransform)
        {
            leftHandTransform.position = newLeftPos;
            leftHandTransform.rotation = newLeftRot;
        }
        
        if (rightHandTransform)
        {
            rightHandTransform.position = newRightPos;
            rightHandTransform.rotation = newRightRot;
        }
    }

    private void HideLocalIndicator()
    {
        var tr = headTransform.gameObject.GetComponentInChildren<GameObject>();
        if(tr)
            tr.SetActive(false);
    }
}
