/* Snowboarding1 - Logan Shehane
 * This script would move the world space UI to a convinient location for the user
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHeight : MonoBehaviour
{
    private GameObject headset;
    public float OffsetY = 0;
    void Awake()
    {
        headset = GameObject.Find("CenterEyeAnchor");
        transform.position = new Vector3(transform.position.x, headset.transform.position.y + OffsetY, transform.position.z);
    }
}
