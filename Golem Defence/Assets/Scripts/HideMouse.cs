using System.Collections; // Import the System.Collections namespace
using System.Collections.Generic; // Import the System.Collections.Generic namespace
using UnityEngine; // Import the UnityEngine namespace

public class HideMouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Make the cursor invisible
        Cursor.visible = false;

        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }
}
