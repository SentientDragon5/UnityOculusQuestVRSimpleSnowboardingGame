/*SnowBoarding 1
 * Logan Shehane
 * 
 * This script controls buttons that send user to the Start Screen
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitToStartButton : MonoBehaviour
{

    public void SceneChanger()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("VR Input"))
        {
            Debug.Log(other);
            SceneChanger();
        }
    }
    public void StartScene()
    {
        SceneManager.LoadScene("start3");
    }
    public void GameScene()
    {
        SceneManager.LoadScene("5");
    }
}

