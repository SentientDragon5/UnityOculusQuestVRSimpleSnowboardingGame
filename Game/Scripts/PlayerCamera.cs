/* Snowboarding1 - SentientDragon5
 * This is a simple script that allows the object attachced to have it's location set to the Player. Originally only meant for the
 * Camera Rig, this script is now used for most objects that are consistant in the game, such as the "fog", score canvas, board, 
 * White plane below the player, the bounds for the player.
 * I use this script in other projects, I find it is very useful.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private GameObject Player;
    public Vector3 offset = new Vector3(0, 0, 0);
    public bool[] Axes = { true, true, true };
    private float newLocationx = 0f;
    private float newLocationy = 0f;
    private float newLocationz = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
                newLocationx + offset.x,
                newLocationy + offset.y,
                newLocationz + offset.z
                );
        if (Axes[0])
        {
            newLocationx = Player.transform.position.x + offset.x;
        }
        else
        {
            newLocationx =  transform.position.x;
        }
        if (Axes[1])
        {
            newLocationy = Player.transform.position.y + offset.y;
        }
        else
        {
            newLocationy = transform.position.y;
        }
        if (Axes[2])
        {
            newLocationz = Player.transform.position.z + offset.z;
        }
        else
        {
            newLocationz = transform.position.z;
        }
       

    }
}
