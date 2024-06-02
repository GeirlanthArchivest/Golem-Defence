using System.Collections; // Import the System.Collections namespace
using System.Collections.Generic; // Import the System.Collections.Generic namespace
using UnityEngine; // Import the UnityEngine namespace

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] Vector2 parallaxEffectMultiplier; // Multiplier to control the parallax effect intensity

    private Transform cameraTransform; // Reference to the main camera's transform
    private Vector3 lastCameraPosition; // Stores the position of the camera in the previous frame
    private float textureUnitSizeX; // Width of the texture in world units

    void Start()
    {
        cameraTransform = Camera.main.transform; // Get the main camera's transform
        lastCameraPosition = cameraTransform.position; // Initialize the last camera position
        Sprite sprite = GetComponent<SpriteRenderer>().sprite; // Get the sprite attached to the GameObject
        Texture2D texture = sprite.texture; // Get the texture of the sprite
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit; // Calculate the width of the texture in world units
    }

    // LateUpdate is called once per frame, after all Update calls
    void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition; // Calculate the change in camera position
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.y); // Apply the parallax effect
        lastCameraPosition = cameraTransform.position; // Update the last camera position

        // Check if the camera has moved far enough to require wrapping the background
        if (cameraTransform.position.x - transform.position.x >= textureUnitSizeX)
        {
            float offsetPositionX = (cameraTransform.position.x - transform.position.x) % textureUnitSizeX; // Calculate the offset position for wrapping
            transform.position = new Vector3(cameraTransform.position.x + offsetPositionX, transform.position.y); // Wrap the background position
        }
    }
}
