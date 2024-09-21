using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeManager : MonoBehaviour
{
    public AudioClip sound; // Slot for the single sound
    public float volume = 1.0f; // Base volume of the ambient sound (1.0 is the default max volume)
    public float amplification = 1.0f; // Amplification factor for the volume

    private AudioSource audioSource; // Reference to the AudioSource component

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false; // Ensure the AudioSource does not loop by default

        // Start the coroutine to play the sound
        StartCoroutine(PlayAmbientSound());
    }

    IEnumerator PlayAmbientSound()
    {
        while (true)
        {
            // Play the amplified version of the sound
            AudioClip amplifiedSound = AmplifyAudioClip(sound, amplification);
            audioSource.clip = amplifiedSound;
            audioSource.Play();

            // Wait for the sound to finish playing
            yield return new WaitForSeconds(sound.length);
        }
    }

    AudioClip AmplifyAudioClip(AudioClip originalClip, float amplificationFactor)
    {
        // Get the audio data from the clip
        float[] data = new float[originalClip.samples * originalClip.channels];
        originalClip.GetData(data, 0);

        // Amplify the audio data
        for (int i = 0; i < data.Length; i++)
        {
            data[i] *= amplificationFactor;
        }

        // Create a new amplified audio clip
        AudioClip amplifiedClip = AudioClip.Create(originalClip.name + "_amplified", originalClip.samples, originalClip.channels, originalClip.frequency, false);
        amplifiedClip.SetData(data, 0);

        return amplifiedClip;
    }
}
