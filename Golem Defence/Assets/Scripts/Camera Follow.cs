using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player1Transform;
    public Transform player2Transform;
    public float followSpeed = 5f;

    private Vector3 cameraOffset;

    void Start()
    {
        // Calculate the initial offset between the camera and the midpoint of the players
        cameraOffset = (player1Transform.position + player2Transform.position) / 2f - transform.position;
    }

    void FixedUpdate()
    {
        // Calculate the midpoint between the two players
        Vector3 midpoint = (player1Transform.position + player2Transform.position) / 2f;

        // Set the target position for the camera
        Vector3 targetPosition = midpoint - cameraOffset;

        // Smoothly move the camera towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}
