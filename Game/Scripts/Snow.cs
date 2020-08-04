/* Snowboarding1 - Logan Shehane
 * This goes on the Slope itself, it allows the slope to delete itself after some distance, and is spawned by the GameMaster.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snow : MonoBehaviour
{
    public GameObject Player;
    public GameObject GameMaster;
    private float snowLocalScale = 1;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        GameMaster = GameObject.Find("GameMaster");
        transform.localEulerAngles = new Vector3(0, 0, 0);
        snowLocalScale = GameMaster.GetComponent<GameMaster>().Scale;
        transform.localScale = new Vector3(transform.localScale.x * snowLocalScale, transform.localScale.y * snowLocalScale, transform.localScale.z * snowLocalScale);
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.transform.position.z > (transform.position.z + GameMaster.GetComponent<GameMaster>().renderDistanceBack))
        {
            Destroy(gameObject);
        }
    }
}
