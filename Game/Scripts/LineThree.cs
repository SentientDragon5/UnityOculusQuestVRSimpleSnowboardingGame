/* Snowboarding1 - SentientDragon5
 * This takes the number of coins the player has directly from the player.
 */
using UnityEngine;
using UnityEngine.UI;

public class LineThree : MonoBehaviour
{
    private GameObject Player;
    public Text textComponentVar;
    public string output = "";
    void Start()
    {
        Player = GameObject.Find("Player");

    }
    void Update()
    {
        output = (Player.GetComponent<Player>().Coins).ToString();
        textComponentVar.text = output;
    }
}
