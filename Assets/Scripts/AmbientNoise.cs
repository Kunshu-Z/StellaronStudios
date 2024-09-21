using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientNoiseManager : MonoBehaviour
{
    public List<AudioSource> audioSources; // List of audio sources spread across the scene
    public float fadeDuration = 5.0f;      // Duration for fade in/out
    public float minVolume = 0.1f;         // Minimum volume (near mute)
    public float pauseBetweenFades = 2.0f; // Pause between fades

    void Start()
    {
        // Start the coroutine to control the volume cycling
        StartCoroutine(CycleAudioSources());
    }

    IEnumerator CycleAudioSources()
    {
        while (true)
        {
            foreach (AudioSource source in audioSources)
            {
                // Fade out the current audio source
                yield return StartCoroutine(FadeOut(source));

                // Pause for a while before fading back in
                yield return new WaitForSeconds(pauseBetweenFades);

                // Fade in the current audio source
                yield return StartCoroutine(FadeIn(source));
            }
        }
    }

    IEnumerator FadeOut(AudioSource source)
    {
        float startVolume = source.volume;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            // Gradually reduce volume over time
            source.volume = Mathf.Lerp(startVolume, minVolume, t / fadeDuration);
            yield return null; // Wait for the next frame
        }

        source.volume = minVolume; // Ensure it hits the minimum volume at the end
    }

    IEnumerator FadeIn(AudioSource source)
    {
        float startVolume = source.volume;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            // Gradually raise volume back to full over time
            source.volume = Mathf.Lerp(startVolume, 1.0f, t / fadeDuration);
            yield return null; // Wait for the next frame
        }

        source.volume = 1.0f; // Ensure it hits full volume at the end
    }
}
