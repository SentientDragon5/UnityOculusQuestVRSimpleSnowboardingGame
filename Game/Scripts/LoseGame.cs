/* Snowboarding1 - SentientDragon5
 * 
 * This is an old script, effects are now controlled by the player.
 * This script was used to mimic the pause, but only if you die.
 * 
 * This script contains the functions I used with Unity Events from other scripts, but the same
 * functions were added to the Player Script.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseGame : MonoBehaviour
{
    
    [Header("Effects")]
    public List<GameObject> loseEffect = new List<GameObject>();
    [Header("Output")]
    public bool GameIsFailed = false;

    void Update()
    {
        
        
        if (GameIsFailed)
        {
            Time.timeScale = 0f;
        }
        
        for (int i = 0; i < loseEffect.Count; i++)
        {
            loseEffect[i].SetActive(GameIsFailed);
        }

    }
    
    void Lose()
    {
        GameIsFailed = true;
        Time.timeScale = 0f;
    }
    public void Quit() //if the function is public, it can be called in Unity Events from another script.
    {
        SceneManager.LoadScene("start3");
    }
    public void Reload()
    {
        SceneManager.LoadScene("5");
    }
}
