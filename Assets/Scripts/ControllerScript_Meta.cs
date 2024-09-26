#if META
public partial class ControllerScript
{

    // Update is called once per frame
    void Update_Meta()
    {
        // While user holds the right index trigger, center the cube and turn it to face user
        if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger)) 
            CenterCube();

        // While thumbstick of right controller is currently pressed to the left
        // rotate cube to the left
        if (OVRInput.Get(OVRInput.RawButton.RThumbstickLeft)) 
            transform.Rotate(0, 5.0f * step, 0);

        // While thumbstick of right controller is currently pressed to the right
        // rotate cube to the right
        if (OVRInput.Get(OVRInput.RawButton.RThumbstickRight))
            transform.Rotate(0, -5.0f * step, 0);

        // If user has just released Button A of right controller in this frame
        if (OVRInput.GetUp(OVRInput.Button.One))
        {
            // Play short haptic on right controller
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
        }

        // While user holds the left hand trigger
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.0f)
        {
            // Assign left controller's position and rotation to cube
            transform.position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
            transform.rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);
        }
    }
}
#endif