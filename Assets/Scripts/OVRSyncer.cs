using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRSyncer : MonoBehaviour
{
    [ContextMenu("Debug Print VRPartPosRot")]
    private void DebugVRPartPosRot()
    {
        Debug.LogWarning($"{headPosition},{ headRotation },\n" +
                         $"{leftHandLocPosition},{leftHandLocRotation},\n" +
                         $"{rightHandLocPosition},{rightHandLocRotation}"
                         );
    }

    public Vector3 headPosition => sceneCamera.transform.position;
    public Quaternion headRotation => sceneCamera.transform.rotation;
    public Vector3 leftHandLocPosition => OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
    public Quaternion leftHandLocRotation => OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);
    public Vector3 rightHandLocPosition => OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
    public Quaternion rightHandLocRotation => OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
    
    
    private NetworkedPlayer myPla;
    [SerializeField] private Camera sceneCamera;
    
    // Update is called once per frame
    private void Update()
    {
        // TODO: do I actually need this ?
        // https://developers.meta.com/horizon/documentation/unity/unity-ovrinput#how-do-i-set-up-controller-input-and-tracking
        OVRInput.Update();
        
        //DebugVRPartPosRot();
    }

    void FixedUpdate()
    {
        // TODO: do I actually need this ?
        // https://developers.meta.com/horizon/documentation/unity/unity-ovrinput#how-do-i-set-up-controller-input-and-tracking
        OVRInput.FixedUpdate();
            
        if (!myPla && (!ConfigManager.Instance || !ConfigManager.Instance.TryGetMyLocalPlayer(ref myPla)))
            return;
        
        // reposition vrRig, follows NetworkedPlayer
        var tra = myPla.transform;
        transform.position = tra.position;
        transform.rotation = tra.rotation;

        // // sync from VrRig Parts to NetworkedPlayer
        // myPla.CmdPlayerBodyPartSync(
        //     sceneCamera.transform.position, sceneCamera.transform.rotation,
        //     OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch), OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch),
        //     OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch), OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch)
        // );
    }
}
