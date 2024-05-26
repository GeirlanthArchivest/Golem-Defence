using UnityEngine.Audio;
using UnityEngine;

// Serializable attribute allows the class to be displayed in the Unity editor
[System.Serializable]
public class Sound
{
    // Name of the sound, used as an identifier
    public string name;

    // AudioClip representing the actual sound data
    public AudioClip clip;

    // Volume of the audio, clamped between 0 and 1
    [Range(0f, 1f)]
    public float volume;

    // Pitch of the audio, clamped between 0 and 1
    [Range(0f, 1f)]
    public float pitch;

    // Boolean indicating whether the audio should loop
    public bool loop;

    // Reference to the AudioSource component associated with this sound
    [HideInInspector]
    public AudioSource source;
}