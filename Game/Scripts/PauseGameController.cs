/* Snowboarding1 - Logan Shehane
 * 
 * This script is for pauseing the game. There are multiple ways to pause a game, but i chose to use the ScaleTime as a way to do this.
 * To Slow down a game, change the Time.scaleTime float to adjust the speed. This does not affect Time.unscaledTime, which should
 * ALWAYS be used in Input Gathering, such as getting a controller keypress.
 * 
 * I also use Unity.Events in this script. This is a useful feature, as in the inspector you can assign functions to call
 * on an Invoke in the script.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PauseGameController : MonoBehaviour
{
    [Header("Input")]
    public OVRInput.Button pauseButton = OVRInput.Button.Start;
    [Header("Effects")]
    public List<GameObject> pauseEffect = new List<GameObject>();
    [Header("Output")]
    public bool GameIsPaused = false;
    public UnityEvent ExtraResumeActivity;
    public UnityEvent ExtraPauseActivity;

    private bool pressedLastFrame = false;
    private bool pressedThisFrame = false;
    void Start()
    {
        if (ExtraPauseActivity == null)
        {
            ExtraPauseActivity = new UnityEvent();
        }
        if (ExtraResumeActivity == null)
        {
            ExtraResumeActivity = new UnityEvent();
        }
    }

    void Update()
    {
        // call these two if you want OVR controller Input
        OVRInput.Update();
        OVRInput.FixedUpdate();

        //pressedThisFrame will only be triggered like GetButtonDown, and pressedLastFrame will remain true until you stop pressing the specified button.
        pressedThisFrame = OVRInput.Get(pauseButton) && !pressedLastFrame;

        if (pressedThisFrame)
        {
            if (!GameIsPaused)
            {
                Pause();
            }
            else if (GameIsPaused)
            {
                Resume();
            }
        }
        /* PROBLEM - if this then you cannot pause physics for Player.GameState.Lose
        if (GameIsPaused)
        {
            Time.timeScale = 0f;
        }
        if (!GameIsPaused)
        {
            Time.timeScale = 1f;
        }
        */
        pressedLastFrame = OVRInput.Get(pauseButton);
        for (int i = 0; i < pauseEffect.Count; i++)
        {
             pauseEffect[i].SetActive(GameIsPaused);
        }
        
    }
    void Resume()
    {
        GameIsPaused = false;
        Time.timeScale = 1f;
        ExtraResumeActivity.Invoke();
    }
    void Pause()
    {
        GameIsPaused = true;
        Time.timeScale = 0f;
        ExtraPauseActivity.Invoke();
    }
    public void Quit()
    {
        SceneManager.LoadScene("start3");
    }
    public void Reload()
    {
        SceneManager.LoadScene("5");
    }
}
