using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Array of Sound objects to manage different audio clips
    public Sound[] Sounds;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        // Iterate through each Sound in the Sounds array
        foreach (Sound s in Sounds)
        {
            // Add an AudioSource component to the game object and assign it to the current Sound
            s.source = gameObject.AddComponent<AudioSource>();
            // Set the AudioClip of the AudioSource to the AudioClip of the current Sound
            s.source.clip = s.clip;

            // Set the volume, pitch, and loop properties of the AudioSource
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Play the audio with the specified name (in this case, "Music") when the script starts
        Play("Main Theme");
    }

    // Play the audio with the specified name and optional parameter to start again if it's already playing
    public void Play(string name, bool startAgain = false)
    {
        // Find the Sound object with the specified name in the Sounds array
        Sound s = Array.Find(Sounds, sound => sound.name == name);

        // Check if the audio should start again or if it's not already playing
        if (startAgain || !s.source.isPlaying)
            // Play the audio using the AudioSource
            s.source.Play();
    }

    // Stop the audio with the specified name
    public void Stop(string name)
    {
        // Find the Sound object with the specified name in the Sounds array
        Sound s = Array.Find(Sounds, sound => sound.name == name);

        // Check if the Sound object was not found
        if (s == null)
        {
            // Log a warning message if the Sound object is not found
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        // Stop the audio using the AudioSource
        s.source.Stop();
    }
}