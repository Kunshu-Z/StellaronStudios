using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientNoise : MonoBehaviour
{
    public AudioClip sound1; // Slot for the first sound
    public AudioClip sound2; // Slot for the second sound
    public AudioClip sound3; // Slot for the third sound

    private AudioSource audioSource; // Reference to the AudioSource component
    private List<AudioClip> sounds; // List to store the sounds

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false; // Ensure the AudioSource does not loop by default

        // Initialize the list of sounds
        sounds = new List<AudioClip> { sound1, sound2, sound3 };

        // Start the coroutine to play sounds
        StartCoroutine(PlayAmbientSounds());
    }

    IEnumerator PlayAmbientSounds()
    {
        while (true)
        {
            // Shuffle the list of sounds
            ShuffleSounds();

            // Play each sound in the list
            foreach (AudioClip sound in sounds)
            {
                audioSource.clip = sound;
                audioSource.Play();

                // Wait for the sound to finish playing
                yield return new WaitForSeconds(sound.length);
            }
        }
    }

    void ShuffleSounds()
    {
        for (int i = sounds.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            AudioClip temp = sounds[i];
            sounds[i] = sounds[randomIndex];
            sounds[randomIndex] = temp;
        }
    }
}
