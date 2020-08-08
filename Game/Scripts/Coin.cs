/* Snowboarding1 - SentientDragon5
 * This script is to be attached to the Coin, when collided with, it does an animation and then delets itself
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private GameObject Player;
    private Animator animator;
    private AudioSource ding;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Player = GameObject.Find("Player");
        ding = GetComponent<AudioSource>();
        ding.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            animator.SetBool("IsCollected", true);
            Player.GetComponent<Player>().Coins += 1;
            ding.enabled = true;
        }
    }
}
