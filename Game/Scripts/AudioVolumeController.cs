/* Snowboarding1 - SentientDragon5
 * 
 * This script is for anything with an AudioSource. It will get the Volume from the Loadmanager (saved to the device
 * across sessions, so that when you close the game the settings of the volume saves. This script reads the info.
 */
using UnityEngine;
public enum AudioType //Enums have many uses, they can be states, or drop down boxes in the inspector. 
{
    Music,
    SFX
}
[RequireComponent(typeof(AudioSource))]
public class AudioVolumeController : MonoBehaviour
{
    AudioSource audioSource;
    public AudioType Type;
    private float Volume = 0.5f;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Type == AudioType.Music) { Volume = LoadManager.instance.data.volumeMusic; }
        else if (Type == AudioType.SFX) { Volume = LoadManager.instance.data.volumeSFX; }
        audioSource.volume = Volume;
    }
}
