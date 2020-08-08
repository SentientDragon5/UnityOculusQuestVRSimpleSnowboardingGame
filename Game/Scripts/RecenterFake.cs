/* Snowboarding1 - SentientDragon5
 * 
 * There is no good way to recenter an oculus Quest or Rift.
 * So far I have found that you need to have a parent gameobject to the OVRCameraRig, such that
 * you can offset the Oculus based on the parent location.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecenterFake : MonoBehaviour
{
    private GameObject CenterEye;
    void Awake()
    {
        CenterEye = GameObject.Find("CenterEyeAnchor");
    }
    public void FakeRecenterPlayer()
    {
        transform.position = new Vector3(
            transform.position.x - CenterEye.transform.position.x,
            transform.position.y,
            transform.position.z - CenterEye.transform.position.z);
    }
}
