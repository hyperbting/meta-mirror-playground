using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRSyncer : MonoBehaviour
{
    [ContextMenu("Debug Print VRPartPosRot")]
    private void DebugVRPartPosRot()
    {
        Debug.LogWarning($"{sceneCamera.transform.position},{ sceneCamera.transform.rotation },\n" +
                         $"{OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch)},{OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch)},\n" +
                         $"{OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch)},{OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch)}"
                         );
    }

    private NetworkedPlayer myPla;
    [SerializeField] private Camera sceneCamera;
    
    // Update is called once per frame
    private void Update()
    {
        // TODO: do I actually need this ?
        // https://developers.meta.com/horizon/documentation/unity/unity-ovrinput#how-do-i-set-up-controller-input-and-tracking
        OVRInput.Update();
        
        //DebugVRPartPosRot();        

        // // While thumbstick of right controller is currently pressed to the left
        // if (OVRInput.Get(OVRInput.RawButton.RThumbstickLeft))
        //     Debug.LogWarning("RThumbstickLeft");
        //
        // // While thumbstick of right controller is currently pressed to the right
        // if (OVRInput.Get(OVRInput.RawButton.RThumbstickRight)) 
        //     Debug.LogWarning("RThumbstickRight");
        //
        // // If user has just released Button A of right controller in this frame
        // if (OVRInput.GetUp(OVRInput.Button.One))
        // {
        //     // Play short haptic on right controller
        //     OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
        // }
        //
        // // While user holds the left hand trigger
        // if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.0f)
        // {
        //     // Assign left controller's position and rotation to cube
        //     Debug.LogWarning($"{OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch)} {OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch)}");
        // }
    }

    void FixedUpdate()
    {
        // TODO: do I actually need this ?
        // https://developers.meta.com/horizon/documentation/unity/unity-ovrinput#how-do-i-set-up-controller-input-and-tracking
        OVRInput.FixedUpdate();
            
        if (!myPla && (!ConfigManager.Instance || !ConfigManager.Instance.TryGetMyLocalPlayer(ref myPla)))
            return;
        
        // sync from NetworkedPlayer to vrRig
        transform.position = myPla.transform.position;
        transform.rotation = myPla.transform.rotation;
            
        // sync from VrRig to NetworkedPlayer
        myPla.CmdPlayerBodyPartSync(
            sceneCamera.transform.position,
            sceneCamera.transform.rotation,
            OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch),
            OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch),
            OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch),
            OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch)
            );
    }
}
