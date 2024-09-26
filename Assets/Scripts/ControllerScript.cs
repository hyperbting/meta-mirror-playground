using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ControllerScript
{
    public Transform RefTransform;
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private float step;

    // Start is called before the first frame update
    void Start()
    {
        // Set initial cube's position in front of user
        ResetCube();
    }

    // Update is called once per frame
    void Base_Update()
    {
        // Define step value for animation
        step = 5.0f * Time.deltaTime;
    }

    private void ResetCube()
    {
        if (!RefTransform)
            return;
        targetPosition = RefTransform.position + RefTransform.forward * 3.0f;
    }
    
    private void CenterCube() // Places cube smoothly at the center of the user's viewport and rotates it to face the camera
    {
        if (!RefTransform)
            return;
        
        ResetCube();
        targetRotation = Quaternion.LookRotation(transform.position - RefTransform.position);

        transform.position = Vector3.Lerp(transform.position, targetPosition, step);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, step);
    }
}