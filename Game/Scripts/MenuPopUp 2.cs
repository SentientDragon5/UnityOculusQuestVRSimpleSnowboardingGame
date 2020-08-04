/* Snowboarding1 - Logan Shehane
 * this script allows the world space UI (or whatever the target would be) to pop up when the trigger is enabled.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPopUp : MonoBehaviour
{
    public GameObject popUp;
    public bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        popUp.SetActive(isActive);
    }
    void OnTriggerEnter()
    {
        isActive = !isActive;
    }
    public void ResetData()
    {
        LoadManager.instance.data.Coins = 0;
    }
}
