/* Snowboarding1 - SentientDragon5
 * 
 * This script is for the board, it changes whether the board should turn on a particle effect in the back of the board.
 * The side particle effects are controlled by the player.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardEmission : MonoBehaviour
{
    public GameObject snowEmission;
    public bool OnGround = false;
    private AudioSource audioSource; //This is so we can control whether the audio is playing.
    public float EmissionAudioInitialDelay = 4f;
    bool GamePaused()
    {
        GameObject Player;
        Player = GameObject.Find("Player");
        bool Paused = Player.GetComponent<PauseGameController>().GameIsPaused;
        return Paused;
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.enabled = false;
        snowEmission.SetActive(false);
    }
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision other)
    {
        Emit(other);
    }
    void OnCollisionExit(Collision other)
    {
        StopEmiting(other);
    }
    void OnCollisionStay(Collision other)
    {
        Emit(other);
    }
    void Emit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground") && !GamePaused())
        {
            snowEmission.SetActive(true);
            audioSource.enabled = true; // I had set the Audio source to start when enabled, then loop.
            OnGround = true;
        }
    }
    void StopEmiting(Collision other)
    {
        if (other.gameObject.CompareTag("Ground") || GamePaused())
        {
            snowEmission.SetActive(false);
            audioSource.enabled = false;
            OnGround = false;
        }
    }
}
