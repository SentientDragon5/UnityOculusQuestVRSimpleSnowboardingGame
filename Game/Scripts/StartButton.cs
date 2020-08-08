/* Snowboarding1 - SentientDragon5
 * Button to load the first sceene
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
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
            //SceneChanger();
            GameScene();
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
