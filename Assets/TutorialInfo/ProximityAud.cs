// in this script, the audio of the whales is control.
// This occurs by playing the audio at all times
// what makes it sound like it's not is the volume,
// which is what we adjust here. Inspo: https://discussions.unity.com/t/change-variable-value-based-on-perceived-audio-volume/2567

using UnityEngine;
using TMPro;


public class ProximityAudio : MonoBehaviour
{
    // swimmer position
    public Transform PlayerCapsule;  

    // maximum distance noise can be heard     
    public float maxDistance = 50f; 
    // tells you how should music fad3e
    public float fadeSpeed = 2f;  

    // the actual audio source 
    public AudioSource audioSource;

    void Start()
    {
        // start audio at 0, but do have it play at all times
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0f; 
        audioSource.Play();
    }

    void Update()
    {
        // calculate volume and make it volume valid number (b/w 0 and 1)
        float distance = Vector3.Distance(transform.position, PlayerCapsule.position);
        float targetVolume = Mathf.Clamp01(1f - (distance / maxDistance));

        // actually makes the change in volume
        audioSource.volume = Mathf.Lerp(audioSource.volume, targetVolume, Time.deltaTime * fadeSpeed);

    }
}