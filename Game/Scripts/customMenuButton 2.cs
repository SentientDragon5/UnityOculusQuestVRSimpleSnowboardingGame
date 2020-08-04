/* Snowboarding1 - Logan Shehane
 * This script can be attached to anything with a collider with Trigger enabled. this will then call the acociated event.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class customMenuButton : MonoBehaviour
{
    public bool invoked = false;
    public UnityEvent buttonPress;
    // Start is called before the first frame update
    void Start()
    {
        if (buttonPress == null)
        {
            buttonPress = new UnityEvent();
        }
        buttonPress.AddListener(OnTriggerEnter);
        invoked = false;
    }
    // Update is called once per frame
    void Update()
    {
    }
    void OnTriggerEnter()
    {
        if (!invoked)
        {
            invoked = true;
        }
        buttonPress.Invoke();
        Debug.Log(transform.name);
    }
}
