using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private GameObject playerCharacter;
    [SerializeField] private GameObject boss;

    private float cameraOffset = -3.0f;
    private float cameraHeightOffset = 7.0f;
    private float cameraRotationOffset = 25.0f;

    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        return;
        
        // Update the position of the camera after each frame to have the same offset as it had in the beginning

        // Find the current direction
        var direction = playerCharacter.transform.position - boss.transform.position;
        
        // Move the camera behind the player character in the given direction
        var newPosition = playerCharacter.transform.position - (direction.normalized * cameraOffset);
        
        // Move the camera above the player for better FoV
        transform.position = new Vector3(newPosition.x, newPosition.y + cameraHeightOffset, newPosition.z);
        
        // Rotate the camera to look at the boss
        transform.LookAt(Vector3.Lerp(boss.transform.position, playerCharacter.transform.position, 0.80f));
    }
    
}
