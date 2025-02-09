using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomGrab : MonoBehaviour
{
    // This script should be attached to both controller objects in the scene
    // Make sure to define the input in the editor (LeftHand/Grip and RightHand/Grip recommended respectively)
    CustomGrab otherHand = null;
    public List<Transform> nearObjects = new List<Transform>();
    public Transform grabbedObject = null;
    public InputActionReference action;
    bool grabbing = false;
    private Vector3 previousPosition;
    private Quaternion previousRotation;

    private void Start()
    {
        action.action.Enable();

        // Find the other hand
        foreach(CustomGrab c in transform.parent.GetComponentsInChildren<CustomGrab>())
        {
            if (c != this)
                otherHand = c;
        }
        // Initialize previous values.
        previousPosition = transform.position;
        previousRotation = transform.rotation;
    }

    void Update()
    {
        grabbing = action.action.IsPressed();
        if (grabbing)
        {
            // Grab nearby object or the object in the other hand
            if (!grabbedObject)
                grabbedObject = nearObjects.Count > 0 ? nearObjects[0] : otherHand.grabbedObject;

            if (grabbedObject)
            {
                // Computing delta position and rotation
                Vector3 deltaPos = transform.position - previousPosition;
                Quaternion deltaRot = transform.rotation * Quaternion.Inverse(previousRotation);

                // Compute the relative position of the grabbed object to the controller.
                Vector3 relativePos = grabbedObject.position - transform.position;
                
                // Rotate the relative position by the delta rotation.
                Vector3 rotatedRelativePos = deltaRot * relativePos;

                // Compute the delta from the rotation.
                Vector3 deltaFromRotation = rotatedRelativePos - relativePos;

                // Apply the delta position to the object's position.
                Vector3 totalDelta = deltaPos + deltaFromRotation;
                grabbedObject.position += totalDelta;

                // Apply the delta rotation to the object's rotation.
                grabbedObject.rotation = deltaRot * grabbedObject.rotation;
            }
        }
        // If let go of button, release object
        else if (grabbedObject)
            grabbedObject = null;

        // saves the current position and rotation here
        previousPosition = transform.position;
        previousRotation = transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Make sure to tag grabbable objects with the "grabbable" tag
        // You also need to make sure to have colliders for the grabbable objects and the controllers
        // Make sure to set the controller colliders as triggers or they will get misplaced
        // You also need to add Rigidbody to the controllers for these functions to be triggered
        // Make sure gravity is disabled though, or your controllers will (virtually) fall to the ground

        Transform t = other.transform;
        if(t && t.tag.ToLower()=="grabbable")
            nearObjects.Add(t);
    }

    private void OnTriggerExit(Collider other)
    {
        Transform t = other.transform;
        if( t && t.tag.ToLower()=="grabbable")
            nearObjects.Remove(t);
    }
}
