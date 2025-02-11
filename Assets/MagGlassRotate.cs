using UnityEngine;

public class MagnifyingGlassCamera : MonoBehaviour
{
    public Transform magnifyingGlass; // Reference to the magnifying glass object
    public Transform vrCamera; // Reference to the VR camera (player's head)
    public Camera magnifyingCamera; // Reference to the camera attached to the magnifying glass

    void Update()
    {
        // Calculate the direction from the magnifying glass to the VR camera
        Vector3 directionToCamera = magnifyingGlass.position - vrCamera.position;

        // Calculate the rotation to look at the VR camera
        Quaternion targetRotation = Quaternion.LookRotation(directionToCamera.normalized, magnifyingGlass.up);

        // Apply the rotation to the magnifying camera
        magnifyingCamera.transform.rotation = targetRotation;
    }
}
